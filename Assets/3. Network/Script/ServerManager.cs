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

        void ServerThread() //멀티스레드로 생성이 되어야 함 / 왜냐 tcpClient에 값이 들어오지 않으면 프로그램이 멈춰버리기 때문
        {

            /*
             오늘 과제 
            서버 스레드를 list로 관리하여
            다중 연결이 가능한 서버로 만들어보세요
             */

            TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
            tcpListener.Start(); //tcp 서버를 가동시킨다.

            //Text logText = Instantiate(messagePrefab, textArea);
            //logText.text = "서버 시작";
            log.Enqueue("서버 시작");

            TcpClient tcpClient = tcpListener.AcceptTcpClient(); //대기가 걸린다.
            //Text logText2 = Instantiate(messagePrefab, textArea);
            //logText2.text = "클라이언트 연결됨";
            log.Enqueue("믈라이언트 연결됨");

            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;

            while(tcpClient.Connected)
            {
                string readString = reader.ReadLine();
                if (string.IsNullOrEmpty(readString))
                    continue;
                //Text messageText = Instantiate(messagePrefab, textArea);
                //messageText.text = readString;
                //받은 메세지를 그대로 wirter에 쓴다.
                writer.WriteLine($"당신의 메세지 : {readString}");

                log.Enqueue($"client message : {readString}");
            }

            log.Enqueue("클라이언트 연결 종료");
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
}
