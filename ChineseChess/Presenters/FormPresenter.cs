using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp.Server;

namespace ChineseChess.Presenters
{
    public class FormPresenter
    {
        private Form1 _form;
        private BattleField _battleField;
        private WebSocketClient _webSocketClient;
        private WebSocketServer _webSocketServer = new WebSocketServer(30678);
        public readonly ChessboardDisplayer ChessboardDisplayer;
        private IMessageSender _messageSender { get; set; }
        public MessageQueue MessageQueue { get; private set; }
        public string ServerNickName = "Player1";
        public string ClientNickName = "Player2";

        public FormPresenter(Form1 form)
        {
            _form = form;
            ChessboardDisplayer = new ChessboardDisplayer(form);
            MessageQueue = new MessageQueue();
            MessageQueue.OnMessage += MessageQueueOnMessage;
        }

        private void MessageQueueOnMessage(object sender, EventArgs e)
        {
            var msg = MessageQueue.Dequeue();
            //Console.WriteLine(msg);

            if (msg.MessageType == Message.Type.TokenMoved)
            {
                var from = new BattleFieldCoordinate(msg.MoveInfo.FromX, msg.MoveInfo.FromY);
                var to = new BattleFieldCoordinate(msg.MoveInfo.ToX, msg.MoveInfo.ToY);
                _battleField.MoveToken(from, to);
            }
            else if (msg.MessageType == Message.Type.Chat)
            {
                var text = string.Format("[Chat] {0}：{1}", msg.NickName, msg.Data);
                DisplayMsg(text);
            }
            else
            {
                var text = string.Format("[{0}] {1}", msg.MessageType, msg.Data);
                DisplayMsg(text);
            }
        }

        internal void CloseView()
        {
            if (_webSocketServer == null)
                return;
            else if (_webSocketServer.IsListening)
                _webSocketServer.Stop();

            if (_webSocketClient == null)
                return;
            else if (_webSocketClient.WSClient.IsAlive)
                _webSocketClient.WSClient.Close();
        }

        internal void ClickBtn()
        {
            var token = _form.UserClickedBtn.Tag as Token;

            var caseChangeActiveatedToken =
                _battleField.GamePhase.CurrentPhase == GamePhase.Phase.WaitingForMove &&
                _battleField.GamePhase.Turn == token.Role.Faction &&
                !token.IsActivated;

            var caseActivatedToken =
                _battleField.GamePhase.CurrentPhase == GamePhase.Phase.WaitingForSelect &&
                _battleField.GamePhase.Turn == token.Role.Faction &&
                !token.IsActivated;

            var caseDeactivatedToken =
                _battleField.GamePhase.CurrentPhase == GamePhase.Phase.WaitingForMove &&
                _battleField.GamePhase.Turn == token.Role.Faction &&
                token.IsActivated;

            var caseAttackEnemyToken =
                _battleField.GamePhase.CurrentPhase == GamePhase.Phase.WaitingForMove &&
                _battleField.GamePhase.Turn != token.Role.Faction;

            var caseNotYourTurn =
                _battleField.GamePhase.CurrentPhase == GamePhase.Phase.WaitingForSelect &&
                _battleField.GamePhase.Turn != token.Role.Faction;

            if (caseActivatedToken || caseChangeActiveatedToken)
            {
                token.Activate();
            }
            else if (caseDeactivatedToken)
            {
                token.Deactivate();
            }
            else if (caseAttackEnemyToken)
            {
                _battleField.MoveToken(_battleField.CurrentActivatedToken.Coordinate, token.Coordinate);
                //token.BecomeTarget();
                Console.WriteLine("attack!");
            }
            else if (caseNotYourTurn)
            {
                return;
            }
            else
            {
                throw new Exception("按鈕判定發生邏輯上的錯誤");
            }
        }

        internal void OnChessboardClicked(int x, int y)
        {
            if (_battleField == null)
                return;

            if (_battleField.GamePhase.CurrentPhase != GamePhase.Phase.WaitingForMove)
                return;
            
            if (x % 60 < 5 || x % 60 > 45 || y % 60 < 5 || y % 60 > 45)
                return;

            var from = _battleField.CurrentActivatedToken.Coordinate;
            var to = _battleField.ConvertPictureboxPointToCoordinate(x, y);

            _battleField.MoveToken(from, to);
        }

        public void StartOrStopServer()
        {
            if (_webSocketServer.IsListening)
                StopServer();
            else
                StartServer();
        }

        private void StopServer()
        {
            _webSocketServer.Stop();
            _battleField.Clear();

            //UI Update
            _form.StartStopServerBtn.Text = "Start Server";
            _form.ConnectToServerBtn.Enabled = true;
            _form.URL.Enabled = true;
            _form.NickName.Enabled = true;
            DisplayMsg("[WebSocket] 伺服器已關閉");
        }

        private void StartServer()
        {
            try
            {
                var nickName = _form.NickName.Text == "" ? "Player1" : _form.NickName.Text;

                if (_webSocketServer.WebSocketServices["/MessageReciever"] == null)
                    _webSocketServer.AddWebSocketService<MessageReceiver>("/MessageReciever");

                _messageSender = new ServerMessageSender(_webSocketServer);

                _webSocketServer.Start();

                if (_battleField == null)
                {
                    _battleField = new ServerBattleField(this, nickName);
                    ChessboardDisplayer.LoadBattleField(_battleField);
                }
                

                _battleField.CreateBattleField();
                //PlaceButtons();
            }
            catch (Exception e)
            {
                DisplayMsg("[Error] " + e.Message);
                DisplayMsg("[Error] " + e.StackTrace);
                return;
            }

            //UI Update
            _form.StartStopServerBtn.Text = "Stop Server";
            _form.ConnectToServerBtn.Enabled = false;
            _form.URL.Enabled = false;
            _form.NickName.Enabled = false;
            _form.SendChatBtn.Enabled = true;
            DisplayMsg("[WebSocket] 已啟動伺服器");
            DisplayMsg("[WebSocket] 等待玩家加入");
        }

        internal void SendChatText()
        {
            var msg = _form.ChatInput.Text;
            var message = new Message(Message.Type.Chat, msg);
            message.NickName = _battleField.NickName;
            _messageSender.Send(Message.Serialize(message));

            var text = string.Format("[Chat] {0}：{1}", message.NickName, message.Data);
            DisplayMsg(text);
        }

        //private void PlaceButtons()
        //{
        //    ChessboardDisplayer.CreateButtons();
        //    //foreach (var token in _battleField.Tokens)
        //    //{
        //    //    _form.Controls.Add(token.Button);
        //    //    token.Button.BringToFront();
        //    //}
        //}

        internal void ConnectToServer()
        {
            var url = string.Format("ws://{0}:30678/MessageReciever", _form.URL.Text);
            try
            {
                var nickName = _form.NickName.Text == "" ? "Player2" : _form.NickName.Text;
                _webSocketClient = new WebSocketClient(this, url);
                _messageSender = new ClientMessageSender(_webSocketClient);
                _webSocketClient.WSClient.Connect();
                _battleField = new ClientBattleField(this, nickName);
                ChessboardDisplayer.LoadBattleField(_battleField);
                _battleField.CreateBattleField();
                Task t = new Task(_webSocketClient.KeepAlive);
                t.Start();
            }
            catch (Exception e)
            {
                DisplayMsg(string.Format("[Error] {0}", e.Message));
                return;
            }
            _form.StartStopServerBtn.Enabled = false;
            _form.URL.Enabled = false;
            //PlaceButtons();
        }

        public void DisplayMsg(string msg)
        {
            msg = msg.Replace("]#", "] ").Replace("#", "");
            if (!_form.MessagesTextBox.InvokeRequired)
            {
                _form.MessagesTextBox.AppendText(msg + "\r\n");
            }
            else
            {
                _form.MessagesTextBox.Invoke((MethodInvoker)delegate
                {
                    _form.MessagesTextBox.AppendText(msg + "\r\n");
                });
            }
        }

        internal void StartOfflineGame()
        {
            //_battleField = new ServerBattleField(_form.pictureBox1);
            //PlaceButtons();
        }

        internal void SendMessage(string msg)
        {
            _messageSender.Send(msg);
        }
    }
}
