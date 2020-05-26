using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    public class Message
    {
        public readonly Type MessageType;
        public readonly string Data;
        public readonly MoveInfo MoveInfo;
        public string NickName = "";

        public Message(Type type, string data)
        {
            MessageType = type;
            Data = data;
            MoveInfo = null;
        }

        public Message(MoveInfo moveInfo)
        {
            MessageType = Type.TokenMoved;
            MoveInfo = moveInfo;
            Data = null;
        }

        public override string ToString()
        {
            if (MoveInfo != null)
                return string.Format("[Moved]#{0}#{1}", NickName, MoveInfo.ToString());

            return string.Format("[{0}]#{1}#{2}", MessageType.ToString(), NickName, Data);
        }

        public static string Serialize(Message message)
        {
            return message.ToString();
        }

        public static Message Deserialize(string message)
        {
            var type = message.Split('#')[0];
            var nickName = message.Split('#')[1];
            var content = message.Split('#')[2];
            if (type == "[Moved]")
            {
                var coor = content.Split('&');
                MoveInfo moveInfo = new MoveInfo(
                    int.Parse(coor[0]), 
                    int.Parse(coor[1]), 
                    int.Parse(coor[2]), 
                    int.Parse(coor[3])
                    );
                return new Message(moveInfo) { NickName = nickName };
            }
            else
            {
                var type2 = Type.Chat;
                switch(type)
                {
                    case "[Chat]":
                        type2 = Type.Chat;
                        break;
                    case "[WebSocket]":
                        type2 = Type.WebSocket;
                        break;
                    case "[Other]":
                        type2 = Type.Other;
                        break;
                }

                return new Message(type2, content) { NickName = nickName };
            }
        }

        public enum Type
        {
            TokenMoved,
            Chat,
            WebSocket,
            Other
        }
    }

    
}
