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
        public NetEnum netEnum;                                 //Server/Client 선택

        public Socket _socket = null;                           //소켓 통신을 위한 Socket 선언
        public List<Socket> clientSockets = new List<Socket>(); // Client 소켓 리스트
        private AsyncCallback receiveHandler;                   // 비동기 수신 핸들러 
        private AsyncCallback sendHandler;                      // 비동기 송신 핸들러
        private AsyncCallback connectHandler;                   // 접속 핸들러
        private AsyncCallback disConnectHandler;                // 접속 종료 핸들러

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

            // 비동기적으로 들어오는 자료를 수신하기 위해 BeginReceive 메서드 사용!
            sockClient.BeginReceive(ao.buffer, 0, ao.buffer.Length, SocketFlags.None, receiveHandler, ao);
            // 클라인언트 접속완료후 새 클라이언트 접속을 위해 다시 대기
            _socket.BeginAccept(connectHandler, _socket);
        }

        private void handleConnectedSocketProvider(Socket socketClient)
        {
            Socket c = socketClient;
            IPEndPoint ip_endPoint = (IPEndPoint)c.RemoteEndPoint;
            string ip = ip_endPoint.Address.ToString();
            Debug.Log(ip);
            clientSockets.Add(c);
            Debug.Log("접속 IP : " + c.ToString());
        }
        #endregion



        #region 데이터 송수신
        /// <summary>
        /// 데이터 송신
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
        /// 데이터 수신
        /// </summary>
        /// <param name="ar"></param>
        public void handleDataReceive(IAsyncResult ar)
        {
            AsyncObject ao = (AsyncObject)ar.AsyncState;

            // 자료를 수신하고, 수신받은 바이트를 가져옴
            int recvBytes = ao.workingSocket.EndReceive(ar);

            // 수신받은 자료의 크기가 1 이상일 때에만 자료 처리
            if (recvBytes > 0)
            {

                try
                {
                    tempMsg = ao.buffer;
                    Debug.Log("메세지 : " + tempMsg);
                }
                catch (Exception e)
                {
                    Debug.Log(e.ToString());
                }

                // 버퍼처리 후 리셑
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