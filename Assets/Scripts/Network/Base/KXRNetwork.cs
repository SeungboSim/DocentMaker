using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

namespace KaonMaker.TCP
{
    public class KXRNetwork : MonoBehaviour
    {
        public NetEnum netEnum;                                 //Server/Client ����

        public Socket _socket = null;                           //���� ����� ���� Socket ����
        public List<Socket> clientSockets = new List<Socket>(); // Client ���� ����Ʈ
        private AsyncCallback receiveHandler;                   // �񵿱� ���� �ڵ鷯 
        private AsyncCallback sendHandler;                      // �񵿱� �۽� �ڵ鷯
        private AsyncCallback connectHandler;                   // ���� �ڵ鷯
        private AsyncCallback disConnectHandler;                // ���� ���� �ڵ鷯

        public byte[] tempMsg;
        public byte[] get_msg
        {
            get
            {
                return this.tempMsg;
            }
            set
            {
                this.tempMsg = value;
            }

        }

        #region Server Handler
        public void handleClientConnectionRequest(IAsyncResult ar)
        {
            Socket sockClient = _socket.EndAccept(ar);

            handleConnectedSocketProvider(sockClient);

            AsyncObject ao = new AsyncObject(4096);
            ao.workingSocket = sockClient;

            // �񵿱������� ������ �ڷḦ �����ϱ� ���� BeginReceive �޼��� ���!
            sockClient.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, receiveHandler, ao);
            // Ŭ���ξ�Ʈ ���ӿϷ��� �� Ŭ���̾�Ʈ ������ ���� �ٽ� ���
            _socket.BeginAccept(connectHandler, _socket);
        }

        private void handleConnectedSocketProvider(Socket socketClient)
        {
            Socket c = socketClient;
            IPEndPoint ip_endPoint = (IPEndPoint)c.RemoteEndPoint;
            string ip = ip_endPoint.Address.ToString();
            Debug.Log(ip);
            clientSockets.Add(c);
            Debug.Log("���� IP : " + c.ToString());
        }
        #endregion



        #region ������ �ۼ���
        /// <summary>
        /// ������ �۽�
        /// </summary>
        /// <param name="ar"></param>
        public void SyncSendMessage(byte[] message)
        {
            AsyncObject ao = new AsyncObject(4096);

            //ao.Buffer = Encoding.UTF8.GetBytes(message);
            ao.buffer = message;

            ao.workingSocket = _socket;

            try
            {
                _socket.BeginSend(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, sendHandler, ao);

            }
            catch (SocketException e)
            {
#if UNITY_EDITOR
                Debug.Log(e.Message);
#endif
            }
        }
        public void handleDataSend(IAsyncResult ar)
        {
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            int sentBytes = ao.workingSocket.EndSend(ar);

            if (sentBytes > 0)
            {
                byte[] tmpmsg = ao.buffer;
            }
        }

        /// <summary>
        /// ������ ����
        /// </summary>
        /// <param name="ar"></param>
        public void handleDataReceive(IAsyncResult ar)
        {
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // �ڷḦ �����ϰ�, ���Ź��� ����Ʈ�� ������
            int recvBytes = ao.workingSocket.EndReceive(ar);

            // ���Ź��� �ڷ��� ũ�Ⱑ 1 �̻��� ������ �ڷ� ó��
            if (recvBytes > 0)
            {

                try
                {
                    tempMsg = ao.buffer;
                    Debug.Log("�޼��� : " + tempMsg);
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }

                // ����ó�� �� ���V
                ao.buffer = new byte[4096];
            }
        }
        #endregion


        public void SocketStop()
        {
            _socket.Shutdown(SocketShutdown.Both);
            _socket.Close();
        }
        
    }
}