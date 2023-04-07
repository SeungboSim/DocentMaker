using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;


namespace KaonMaker.TCP
{

    public class AsyncObject
    {
        public byte[] buffer;
        public Socket workingSocket;
        public AsyncObject(int bufferSize)
        {
            this.buffer = new byte[bufferSize];
        }

    }
}
