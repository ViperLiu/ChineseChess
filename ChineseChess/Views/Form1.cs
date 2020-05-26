using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using WebSocketSharp;

namespace ChineseChess
{
    public partial class Form1 : Form
    {
        //public static BattleField BattleField;
        //public static MessageQueue MessageQueue = new MessageQueue();

        private Presenters.FormPresenter _presenter;

        public Button UserClickedBtn { get; private set; }

        public Form1()
        {
            InitializeComponent();
            _presenter = new Presenters.FormPresenter(this);
            //BattleField = new BattleField(pictureBox1);
            //MessageQueue.OnMessage += MessageQueue_OnMessage;
            //_battleField.PlaceToken(this);
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {
            MouseEventArgs me = (MouseEventArgs)e;
            var clickX = me.Location.X;
            var clickY = me.Location.Y;
            _presenter.OnChessboardClicked(clickX, clickY);
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            _presenter.CloseView();

            
        }


        private void StartStopServer_Click(object sender, EventArgs e)
        {
            _presenter.StartOrStopServer();
        }

        private void StartOfflineGameBtn_Click(object sender, EventArgs e)
        {
            _presenter.StartOfflineGame();
        }

        private void ConnectToServerBtn_Click(object sender, EventArgs e)
        {
            _presenter.ConnectToServer();
        }

        private void SendChatBtn_Click(object sender, EventArgs e)
        {
            _presenter.SendChatText();
        }

        internal void Btn_Click(object sender, EventArgs e)
        {
            UserClickedBtn = sender as Button;
            _presenter.ClickBtn();
        }
    }
}
