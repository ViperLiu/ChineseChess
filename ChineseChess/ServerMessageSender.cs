using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebSocketSharp.Server;

namespace ChineseChess
{
    class ServerMessageSender : IMessageSender
    {
        private WebSocketServer _webSocketServer;

        public ServerMessageSender(WebSocketServer webSocketServer)
        {
            _webSocketServer = webSocketServer;
        }

        public void Send(string message)
        {
            var sessions = _webSocketServer.WebSocketServices["/MessageReciever"].Sessions;
            var id = sessions.IDs.ToList();
            sessions.SendTo(message, id.First());
        }
    }
}
