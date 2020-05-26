using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    public class MessageQueue
    {
        private readonly Queue<Message> _messages;

        public event EventHandler OnMessage;

        public int Count { get { return _messages.Count; } }

        public MessageQueue()
        {
            _messages = new Queue<Message>();
        }

        public void Enqueue(Message m)
        {
            _messages.Enqueue(m);
            OnMessage(this, new EventArgs());
        }

        public Message Dequeue()
        {
            return _messages.Dequeue();
        }
    }
}
