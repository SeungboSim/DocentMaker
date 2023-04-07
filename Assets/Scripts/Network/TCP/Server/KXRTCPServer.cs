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
        private Socket _serverSocket = null;                    //소켓 통신을 위한 Socket 선언
        
        private AsyncCallback receiveHandler;                   // 비동기 수신 핸들러 
        private AsyncCallback sendHandler;                      // 비동기 송신 핸들러
        private AsyncCallback connectHandler;                   // 접속 핸들러
        private AsyncCallback disConnectHandler;                // 접속 종료 핸들러

        private Thread SyncThread; //Multi Threading을 위한 Thread 호출

        #region Event 등록
        public KXRTCPServer()
        {
            // 비동기 작업에 사용될 대리자를 초기화합니다.
            receiveHandler += new AsyncCallback(handleDataReceive);
            sendHandler += new AsyncCallback(handleDataSend);
            connectHandler += new AsyncCallback(handleClientConnectionRequest);
        }

     
        #endregion

        #region Server 시작

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
        /// Server 스타트
        /// </summary>
        /// <param name="PortNum"></param>
        public void StartServer(int PortNum)
        {
            try
            {
                // TCP 통신을 위한 소켓을 생성합니다.
                _serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                // 특정 포트에서 모든 주소로부터 들어오는 연결을 받기 위해 포트를 바인딩
                _serverSocket.Bind(new IPEndPoint(IPAddress.Any, PortNum));
                // 연결 요청을 받기 
                _serverSocket.Listen(100);

                _serverSocket.BeginAccept(connectHandler, _serverSocket);

            }
            catch (Exception e)
            {
#if UNITY_EDITOR //이렇게 구분을 하지 않으면 향후 소스가 많아졌을 때, 성능 이슈를 발생 시킴.
                Debug.LogWarning("Threading is Not Starting" + e.Message);
#endif
            }
        }
        #endregion


        #region Server 종료
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