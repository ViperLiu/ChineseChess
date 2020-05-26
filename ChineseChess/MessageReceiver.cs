using System;
using WebSocketSharp;
using WebSocketSharp.Server;

namespace ChineseChess
{
    public class MessageReceiver : WebSocketBehavior
    {
        
        protected override void OnMessage(MessageEventArgs e)
        {
            if (e.Data == null || e.Data == "")
                return;

            Message msg = Message.Deserialize(e.Data);
            Console.WriteLine("Server OnMessage : " + e.Data);
            //Form1.MessageQueue.Enqueue(msg);
        }

        protected override void OnError(ErrorEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

        protected override void OnOpen()
        {
            //var msg2 = string.Format("已加入 {0} 的遊戲", ServerBattleField.ServerNickName);
            //var message2 = new Message(Message.Type.WebSocket, msg2);
            //MessageSender.Send(Message.Serialize(message2));
        }

        protected override void OnClose(CloseEventArgs e)
        {
            Console.WriteLine(e.Reason);
        }
    }
}
