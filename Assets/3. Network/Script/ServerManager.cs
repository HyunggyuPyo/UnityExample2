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

        public string ipAddress = "127.0.0.1"; // IPv6���� ȣȯ���� ���� string�� �ַ� ����Ѵ�.
        
        //public byte[] ipAddressArray = { 127, 0, 0, 1 };
        public int port = 8987; // 80 ������ ��Ʈ�� ��ǻ� ���� ������ �Ǿ�����
                                // �� 0 ~ 65, 535 => ushort �������� ���ڸ� ����� �� ������.(port�ּҴ� 2����Ʈ�� ��ȣ ���� ������ ���.) C#������ int ���

        
        bool isConnected = false;
        private Thread serverMainThread;
        //private List<Thread> serverThreads = new List<Thread>();
        private List<ClientHandler> clients = new List<ClientHandler>();
        private List<Thread> threads = new List<Thread>();

        public static Queue<string> log = new Queue<string>(); //��� �����尡 ������ �� �ִ� Data������ Queue

        void Awake()
        {
            connectButton.onClick.AddListener(ServerConnectButtonClick);    
        }

        public void ServerConnectButtonClick()
        {
            if(false == isConnected)
            {
                //���⼱ ������ ����
                serverMainThread = new Thread(ServerThread);
                serverMainThread.IsBackground = true;
                serverMainThread.Start();
                isConnected = true;
            }
            else
            {
                //���⼭�� ������ �ݴ´�.
                serverMainThread.Abort();
                isConnected = false;
            }
        }

        //��ſ��� Ȱ�������, ������ ����� �� �������� ������ å������ Input, output ��Ʈ���� �ʿ���
        private StreamReader reader;
        private StreamWriter writer;
        private int clientId = 0;

        void ServerThread() //��Ƽ������� ������ �Ǿ�� �� / �ֳ� tcpClient�� ���� ������ ������ ���α׷��� ��������� ����
        {

            /*
             ���� ���� 
            ���� �����带 list�� �����Ͽ�
            ���� ������ ������ ������ ��������
             */


            //try/catch ���� �뵵 : Exception �߻��� �޼����� �������� Ȱ�� �� �� �ֵ��� ��.
            //�� �����  if-else���� ����ϴ�.
            try //if ��� ���� �������� ������ ���� ��� ����
            {
                TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
                tcpListener.Start(); //tcp ������ ������Ų��.
                log.Enqueue("���� ����");

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

                    log.Enqueue($"{handler.id}�� Ŭ���̾�Ʈ�� ���ӵ�.");
                }
                /*
                ////Text logText = Instantiate(messagePrefab, textArea);
                ////logText.text = "���� ����";
                //log.Enqueue("���� ����");

                //TcpClient tcpClient = tcpListener.AcceptTcpClient(); //��Ⱑ �ɸ���.
                ////Text logText2 = Instantiate(messagePrefab, textArea);
                ////logText2.text = "Ŭ���̾�Ʈ �����";
                //log.Enqueue("�ɶ��̾�Ʈ �����");

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
                //    //���� �޼����� �״�� wirter�� ����.
                //    writer.WriteLine($"����� �޼��� : {readString}");

                //    log.Enqueue($"client message : {readString}");
                //}

                //log.Enqueue("Ŭ���̾�Ʈ ���� ����");
                */
            }
            catch (System.NullReferenceException e)
            {
                log.Enqueue("���� �߻�");
                log.Enqueue(e.Message);
            }
            catch (System.Exception e) //try�� ���� �����߿� ������ �߻��� �� ȣ��
            {
                log.Enqueue("���� �߻�");
                log.Enqueue(e.Message);
            }
            finally //try�� ������ ������ �߻� �ص� ����ǰ�, �� �ص� �����
            { // �ַ�, �߰��� �帧�� ��Ű�� �ʰ� ������ ��ü�� �����ϴ� ���� �ݵ�� �ʿ��� ������ ���⼭ �����ϰ� ��.
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

    // Ŭ���̾�Ʈ�� TCP ���� ��û�� �� ������ �ش� Ŭ���̾�Ʈ�� �ٵ�� �ִ� ��ü�� �����Ѵ�.
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

                    //�о�� �޼����� ������ �������� ����
                    server.BroadcastToClients($"{id} ���� �� : {readString}");
                }
            }
            catch (System.Exception e)
            {
                ServerManager.log.Enqueue($"{id}�� Ŭ���̾�Ʈ ���� �߻� : {e.Message}");
            }
            finally
            {
                Disconnect();
            }
        }
    }
}


