using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;


namespace KaonMaker.TCP.Client
{

    public class KXRTCPClient : KXRNetwork
    {
        private bool g_Connected;
        private AsyncCallback receiveHandler;                   // �񵿱� ���� �ڵ鷯 
        private AsyncCallback sendHandler;                      // �񵿱� �۽� �ڵ鷯

        public string iPAddress = "";   //���� IP �ּ�
        public int port = 9999;         //���� ��Ʈ

        private Thread SyncThread; //Multi Threading�� ���� Thread ȣ��

        public string msgTest = "";


        public KXRTCPClient()
        {
            receiveHandler = new AsyncCallback(handleDataReceive);
            sendHandler = new AsyncCallback(handleDataSend);
        }

        private void Start()
        {
            ConnectToServer(iPAddress, port);
        }

        public void ConnectToServer(string hostName, int hostPort)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            bool isConnected = false;
            try
            {
                _socket.Connect(hostName, hostPort);
                isConnected = true;
            }
            catch
            {
                //msg = "���� ���� (���� ���� ������ �߻���)";
                isConnected = false;
            }
            g_Connected = isConnected;

            if (isConnected)
            {
                AsyncObject ao = new AsyncObject(4096);

                ao.workingSocket = _socket;

                SyncThread = new Thread(() => _socket.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, receiveHandler, ao));
                SyncThread.IsBackground = true;
                SyncThread.Start();
                Debug.Log("SyncClientThread Start");
            }
            else
            {
                //msg = "���� ����!";

            }
        }

        
        public void SendMeesageTest()
        {
            SendMessage(msgTest);
        }
        


        private void OnApplicationQuit()
        {
            if (SyncThread != null)
            {
                SyncThread.Abort();
            }
            SocketStop();
        }

    }
}