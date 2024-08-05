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

        void ServerThread() //��Ƽ������� ������ �Ǿ�� �� / �ֳ� tcpClient�� ���� ������ ������ ���α׷��� ��������� ����
        {

            /*
             ���� ���� 
            ���� �����带 list�� �����Ͽ�
            ���� ������ ������ ������ ��������
             */

            TcpListener tcpListener = new TcpListener(IPAddress.Parse(ipAddress), port);
            tcpListener.Start(); //tcp ������ ������Ų��.

            //Text logText = Instantiate(messagePrefab, textArea);
            //logText.text = "���� ����";
            log.Enqueue("���� ����");

            TcpClient tcpClient = tcpListener.AcceptTcpClient(); //��Ⱑ �ɸ���.
            //Text logText2 = Instantiate(messagePrefab, textArea);
            //logText2.text = "Ŭ���̾�Ʈ �����";
            log.Enqueue("�ɶ��̾�Ʈ �����");

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
                //���� �޼����� �״�� wirter�� ����.
                writer.WriteLine($"����� �޼��� : {readString}");

                log.Enqueue($"client message : {readString}");
            }

            log.Enqueue("Ŭ���̾�Ʈ ���� ����");
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
