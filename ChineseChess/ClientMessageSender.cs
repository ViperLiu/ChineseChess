using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    class ClientMessageSender : IMessageSender
    {
        private WebSocketClient _webSocketClient;

        public ClientMessageSender(WebSocketClient webSocketClient)
        {
            _webSocketClient = webSocketClient;
        }

        public void Send(string message)
        {
            _webSocketClient.WSClient.Send(message);
        }
        
    }
}
