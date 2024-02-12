using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAppTest.NetWork
{
    public class TcpConnecter
    {
        private IPEndPoint ipe;
        private Socket scoket = null;

        //
        public TcpConnecter(IPEndPoint ipe)
        {
            this.ipe = ipe;

        }

        //初始化连接
        public void StartConnetion()
        {
            if (scoket == null)
            {
                this.scoket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                this.scoket.Connect(ipe);
            }
        }
        //发送信息给server；type: 方便以后判断不同消息类型，以调用对应方法
        public void SendMessage(String type, String message)
        {
            byte[] send = Encoding.UTF8.GetBytes(type + " " + message);
            scoket.Send(send);
            Console.WriteLine("Successfully Send");
        }
    }
}

