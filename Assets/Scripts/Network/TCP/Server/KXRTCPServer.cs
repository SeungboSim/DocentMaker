using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;


namespace KaonMaker.TCP.Server
{

    public class KXRTCPServer : KXRNetwork
    {
        private Socket _serverSocket = null;                    //���� ����� ���� Socket ����
        
        private AsyncCallback receiveHandler;                   // �񵿱� ���� �ڵ鷯 
        private AsyncCallback sendHandler;                      // �񵿱� �۽� �ڵ鷯
        private AsyncCallback connectHandler;                   // ���� �ڵ鷯
        private AsyncCallback disConnectHandler;                // ���� ���� �ڵ鷯

        private Thread SyncThread; //Multi Threading�� ���� Thread ȣ��

        #region Event ���
        public KXRTCPServer()
        {
            // �񵿱� �۾��� ���� �븮�ڸ� �ʱ�ȭ�մϴ�.
            receiveHandler += new AsyncCallback(handleDataReceive);
            sendHandler += new AsyncCallback(handleDataSend);
            connectHandler += new AsyncCallback(handleClientConnectionRequest);
        }

     
        #endregion

        #region Server ����

        void Start()
        {
            SyncThread = new Thread(() => StartServer(9999));
            SyncThread.IsBackground = true;
            SyncThread.Start();
#if UNITY_EDITOR
            Debug.Log("Server Start");
#endif
        }

        /// <summary>
        /// Server ��ŸƮ
        /// </summary>
        /// <param name="PortNum"></param>
        public void StartServer(int PortNum)
        {
            try
            {
                // TCP ����� ���� ������ �����մϴ�.
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // Ư�� ��Ʈ���� ��� �ּҷκ��� ������ ������ �ޱ� ���� ��Ʈ�� ���ε�
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, PortNum));
                // ���� ��û�� �ޱ� 
                _serverSocket.Listen(100);

                _serverSocket.BeginAccept(connectHandler, _serverSocket);

            }
            catch (Exception e)
            {
#if UNITY_EDITOR //�̷��� ������ ���� ������ ���� �ҽ��� �������� ��, ���� �̽��� �߻� ��Ŵ.
                Debug.LogWarning("Threading is Not Starting" + e.Message);
#endif
            }
        }
        #endregion


        #region Server ����
        private void OnApplicationQuit()
        {
            SocketStop();
            if (SyncThread != null)
            {
                SyncThread.Abort();
            }
        }
        #endregion
    }
}