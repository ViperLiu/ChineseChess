using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WebSocketSharp;

namespace ChineseChess
{
    public class WebSocketClient
    {
        public readonly WebSocket WSClient;
        private Presenters.FormPresenter _presenter;

        public WebSocketClient(Presenters.FormPresenter presenter, string url)
        {
            _presenter = presenter;
            WSClient = new WebSocket(url);
            WSClient.EmitOnPing = true;
            WSClient.OnMessage += OnMessage;
            WSClient.OnError += OnError;
            WSClient.OnClose += OnClose;
            WSClient.OnOpen += OnOpen;
            //WSClient.Connect();
        }


        private void OnOpen(object sender, EventArgs e)
        {
            var msg2 = string.Format("{0} 已加入遊戲", _presenter.ClientNickName);
            var message2 = new Message(Message.Type.WebSocket, msg2);
            message2.NickName = _presenter.ClientNickName;
            _presenter.SendMessage(Message.Serialize(message2));
        }

        private void OnClose(object sender, CloseEventArgs e)
        {
            var msg2 = string.Format("已退出 {0} 的遊戲", _presenter.ServerNickName);
            var message2 = new Message(Message.Type.WebSocket, msg2);
            _presenter.MessageQueue.Enqueue(message2);
        }

        private void OnError(object sender, ErrorEventArgs e)
        {
            Console.WriteLine("OnError : " + e.Message);
        }

        private void OnMessage(object sender, MessageEventArgs e)
        {
            if (e.Data == null || e.Data == "")
                return;
            Console.WriteLine("OnMessage : " + e.Data);
            _presenter.MessageQueue.Enqueue(Message.Deserialize(e.Data));
        }

        public void KeepAlive()
        {
            while(WSClient.IsAlive)
            {
                WSClient.Ping("ping");
                Console.WriteLine("Ping");
                Thread.Sleep(10 * 1000);
            }
            Console.WriteLine("Connection to server is closed");
        }

        public void SendTrash()
        {
            while (true)
            {
                WSClient.Send("Trash");
                Console.WriteLine("Trash");
                Thread.Sleep(3 * 1000);
            }
        }

    }
}
