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
    public class ClientManager : MonoBehaviour
    {
        public Button connectButton;
        public Text messagePrefab;
        public RectTransform textArea;

        public InputField ip;
        public InputField port;

        public InputField messageInput;
        public Button sendButton;

        private bool isConnected = false;
        private Thread clientThread;

        public static Queue<string> log = new Queue<string>();

        private StreamReader reader;
        private StreamWriter writer;

        private void Awake()
        {
            connectButton.onClick.AddListener(ConnectButtonClick);
            //messageInput.OnSubmit.AddListener(MessageToServer);
        }

        public void ConnectButtonClick()
        {
            if(false == isConnected)
            {
                //������ ���� �õ�
                clientThread = new Thread(ClientTread);
                clientThread.IsBackground = true;
                clientThread.Start();
                isConnected = true;
            }
            else
            {
                //���� ����
                clientThread.Abort();
                isConnected = false;
            }
        }

        private void ClientTread()
        {
            TcpClient tcpClient = new TcpClient();

            IPAddress serverAdderss = IPAddress.Parse(ip.text);
            int portNum = int.Parse(port.text);

            IPEndPoint endPoint = new IPEndPoint(serverAdderss, portNum);
            tcpClient.Connect(endPoint);

            log.Enqueue($"������ ���ӵ�. IP : {endPoint.Address}");

            reader = new StreamReader(tcpClient.GetStream());
            writer = new StreamWriter(tcpClient.GetStream());
            writer.AutoFlush = true;

            while(tcpClient.Connected)
            {
                string readString = reader.ReadLine();
                log.Enqueue(readString);
            }

            log.Enqueue("���� ����");
        }

        public void MessageToServer(string message) //inputField�� OnSubmit���� ȣ��
        {
            writer.WriteLine(message);
            messageInput.text = "";
        }

        private void Update()
        {
            if(log.Count > 0)
            {
                Instantiate(messagePrefab, textArea).text = log.Dequeue();
            }
        }
    }
}