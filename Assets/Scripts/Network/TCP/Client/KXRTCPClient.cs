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
        private AsyncCallback receiveHandler;                   // 비동기 수신 핸들러 
        private AsyncCallback sendHandler;                      // 비동기 송신 핸들러

        public string iPAddress = "";   //서버 IP 주소
        public int port = 9999;         //서버 포트

        private Thread SyncThread; //Multi Threading을 위한 Thread 호출

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
                //msg = "연결 실패 (연결 도중 오류가 발생함)";
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
                //msg = "연결 실패!";

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