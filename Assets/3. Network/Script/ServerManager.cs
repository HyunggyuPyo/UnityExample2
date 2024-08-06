using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

namespace MyProject
{
    public class ServerManager : MonoBehaviour
    {
        public Button connectButton;
        public Text messagePrefab;
        public RectTransform textArea;

        public string ipAddress = "127.0.0.1"; // IPv6와의 호환성을 위해 string을 주로 사용한다.
        
        //public byte[] ipAddressArray = { 127, 0, 0, 1 };
        public int port = 8987; // 80 이전의 포트는 사실상 겅의 선점이 되어있음
                                // ㄴ 0 ~ 65, 535 => ushort 사이즈의 숫자만 취급할 수 있으나.(port주소는 2바이트의 부호 없는 정수를 사용.) C#에서는 int 사용

        
        bool isConnected = false;
        private Thread serverMainThread;
        //private List<Thread> serverThreads = new List<Thread>();
        private List<ClientHandler> clients = new List<ClientHandler>();
        private List<Thread> threads = new List<Thread>();

        public static Queue<string> log = new Queue<string>(); //모든 스레드가 접근할 수 있는 Data영역의 Queue

        void Awake()
        {
            connectButton.onClick.AddListener(ServerConnectButtonClick);    
        }

        public void ServerConnectButtonClick()
        {
            if(false == isConnected)
            {
                //여기선 서버를 열고
                serverMainThread = new Thread(ServerThread);
                serverMainThread.IsBackground = true;
                serverMainThread.Start();
                isConnected = true;
            }
            else
            {
                //여기서는 서버를 닫는다.
                serverMainThread.Abort();
                isConnected = false;
            }
        }

        //통신에도 활용되지만, 데이터 입출력 등 데이터의 전송을 책임지는 Input, output 스트림이 필요함
        private StreamReader reader;
        private StreamWriter writer;
        private int clientId = 0;

        void ServerThread() //멀티스레드로 생성이 되어야 함 / 왜냐 tcpClient에 값이 들어오지 않으면 프로그램이 멈춰버리기 때문
        {

            /*
             오늘 과제 
            서버 스레드를 list로 관리하여
            다중 연결이 가능한 서버로 만들어보세요
             */


            //try/catch 문의 용도 : Exception 발생시 메세지를 수동으로 활용 할 수 있도록 함.
            //잘 제어된  if-else문과 비슷하다.
            try //if 블록 안의 구문에서 에러가 없을 경우 실행
            {
                TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
                tcpListener.Start(); //tcp 서버를 가동시킨다.
                log.Enqueue("서버 시작");

                while (true)
                {
                    TcpClient client = tcpListener.AcceptTcpClient();

                    ClientHandler handler = new ClientHandler();
                    handler.Connect(clientId++, this, client);
                    clients.Add(handler);

                    Thread clientThread = new Thread(handler.Run);
                    clientThread.IsBackground = true;
                    clientThread.Start();

                    threads.Add(clientThread);

                    log.Enqueue($"{handler.id}번 클라이언트가 접속됨.");
                }
                /*
                ////Text logText = Instantiate(messagePrefab, textArea);
                ////logText.text = "서버 시작";
                //log.Enqueue("서버 시작");

                //TcpClient tcpClient = tcpListener.AcceptTcpClient(); //대기가 걸린다.
                ////Text logText2 = Instantiate(messagePrefab, textArea);
                ////logText2.text = "클라이언트 연결됨";
                //log.Enqueue("믈라이언트 연결됨");

                //reader = new StreamReader(tcpClient.GetStream());
                //writer = new StreamWriter(tcpClient.GetStream());
                //writer.AutoFlush = true;

                //while (tcpClient.Connected)
                //{
                //    string readString = reader.ReadLine();
                //    if (string.IsNullOrEmpty(readString))
                //        continue;
                //    //Text messageText = Instantiate(messagePrefab, textArea);
                //    //messageText.text = readString;
                //    //받은 메세지를 그대로 wirter에 쓴다.
                //    writer.WriteLine($"당신의 메세지 : {readString}");

                //    log.Enqueue($"client message : {readString}");
                //}

                //log.Enqueue("클라이언트 연결 종료");
                */
            }
            catch (System.NullReferenceException e)
            {
                log.Enqueue("에러 발생");
                log.Enqueue(e.Message);
            }
            catch (System.Exception e) //try문 내의 구문중에 에러가 발생할 시 호출
            {
                log.Enqueue("에러 발생");
                log.Enqueue(e.Message);
            }
            finally //try문 내에세 에러가 발생 해도 실행되고, 안 해도 실행됨
            { // 주로, 중간에 흐름이 끊키지 않고 생성된 객체를 해제하는 등의 반드시 필요한 정차를 여기서 수행하게 됨.
                foreach (var thread in threads)
                {
                    thread?.Abort();
                }
            }
        }

        public void Disconnect(ClientHandler client)
        {
            clients.Remove(client);
        }

        public void BroadcastToClients(string message)
        {
            log.Enqueue(message);

            foreach (ClientHandler client in clients)
            {
                client.MessageToClient(message);
            }
        }

        private void Update()
        {
            if(log.Count > 0)
            {
                Text logText = Instantiate(messagePrefab, textArea);
                logText.text = log.Dequeue();
            }
        }
    }

    // 클라이언트가 TCP 접속 요청을 할 떄마다 해당 클라이언트를 붙들고 있는 객체를 생성한다.
    public class ClientHandler
    {
        public int id;
        public ServerManager server;
        public TcpClient tcpClient;
        public StreamReader reader;
        public StreamWriter writer;

        public void Connect(int id, ServerManager server, TcpClient tcpClient)
        {
            this.id = id;
            this.server = server;
            this.tcpClient = tcpClient;
            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;
        }  

        public void Disconnect()
        {
            writer.Close();
            reader.Close();
            tcpClient.Close();
            server.Disconnect(this);
        }

        public void MessageToClient(string message)
        {
            writer.WriteLine(message);
        }

        public void Run()
        {
            try
            {
                while(tcpClient.Connected)
                {
                    string readString = reader.ReadLine();
                    if(string.IsNullOrEmpty(readString))
                    {
                        continue;
                    }

                    //읽어온 메세지가 있으면 서버에게 전달
                    server.BroadcastToClients($"{id} 님의 말 : {readString}");
                }
            }
            catch (System.Exception e)
            {
                ServerManager.log.Enqueue($"{id}번 클라이언트 오류 발생 : {e.Message}");
            }
            finally
            {
                Disconnect();
            }
        }
    }
}


