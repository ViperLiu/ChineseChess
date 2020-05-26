using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChineseChess
{
    public class ChessboardDisplayer
    {
        private readonly Graphics G;
        private readonly Pen _blackPen = new Pen(Color.Black, 2);
        private readonly Pen _redPen = new Pen(Color.Red, 2);
        private readonly Pen _eraser = new Pen(SystemColors.Control, 2);
        private readonly PictureBox _pb;
        public Point _topLeftPoint;
        private Point _topRightPoint;
        private Point _bottomLeftPoint;
        private Point _bottomRightPoint;
        private int _unit = 60;
        private Form1 _form;
        private BattleField _battleField;
        private List<Button> _buttons = new List<Button>();
        private Chessboard[,] _chessboard = new Chessboard[10, 9];

        public ChessboardDisplayer(Form1 form)
        {
            _form = form;
            _pb = _form.pictureBox1;
            _topLeftPoint = new Point(25, 25);
            _topRightPoint = new Point(_pb.Width - 25, 25);
            _bottomLeftPoint = new Point(25, _pb.Height - 25);
            _bottomRightPoint = new Point(_pb.Width - 25, _pb.Height - 25);
            G = _pb.CreateGraphics();
        }

        public void LoadBattleField(BattleField battleField)
        {
            _battleField = battleField;
        }

        //public void DisplayChessboard()
        //{
        //    DrawChessboard();
        //    CreateButtons();
        //}

        public void HighlightLocation(BattleFieldCoordinate[] coordinates)
        {
            DrawChessboard();
            foreach (var co in coordinates)
            {
                if(_battleField.IsLocationOccupied(co))
                    continue;
                G.DrawEllipse(
                    _redPen,
                    GetChessboard(co).ButtonCoordinate.X,
                    GetChessboard(co).ButtonCoordinate.Y, 
                    40, 
                    40
                    );
            }
        }

        public void EraseHighlight(BattleFieldCoordinate[] coordinates)
        {
            foreach (var co in coordinates)
            {
                if (_battleField.IsLocationOccupied(co))
                    continue;
                G.DrawEllipse(
                    _eraser,
                    GetChessboard(co).ButtonCoordinate.X,
                    GetChessboard(co).ButtonCoordinate.Y,
                    40, 
                    40
                    );
            }
            DrawChessboard();
        }

        public void InitializeServerChessboard()
        {
            var Y = _topLeftPoint.Y - 20;
            for (var y = 0; y < 10; y++)
            {
                var X = _topLeftPoint.X - 20;
                for (var x = 0; x < 9; x++)
                {
                    _chessboard[y, x] = new Chessboard(new Point(X, Y));

                    X += _unit;
                }
                Y += _unit;
            }
        }

        public void InitializeClientChessboard()
        {
            var Y = _topLeftPoint.Y - 20;
            for (var y = 9; y >= 0; y--)
            {
                var X = _topLeftPoint.X - 20;
                for (var x = 8; x >= 0; x--)
                {
                    _chessboard[y, x] = new Chessboard(new Point(X, Y));

                    X += _unit;
                }
                Y += _unit;
            }
        }

        public void CreateButtons()
        {
            foreach(var token in _battleField.Tokens)
            {
                var targetChessboard = GetChessboard(token.Coordinate);
                Color color;
                if (token.Role.Faction == Factions.Black)
                    color = Color.Black;
                else
                    color = Color.Red;
                var btn = new Button
                {
                    Text = token.Role.DisplayName,
                    Location = targetChessboard.ButtonCoordinate,
                    ForeColor = color,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font(new FontFamily("標楷體"), 16),
                    Size = new Size(40, 40),
                    Tag = token
                };
                btn.Click += _form.Btn_Click;
                _buttons.Add(btn);
                targetChessboard.Button = btn;
                _form.Controls.Add(btn);
                btn.BringToFront();
            }
        }

        public Chessboard GetChessboard(BattleFieldCoordinate coordinate)
        {
            return _chessboard[coordinate.X, coordinate.Y];
        }

        public Chessboard GetChessboard(int x, int y)
        {
            return _chessboard[x, y];
        }

        public void DrawChessboard()
        {
            DrawFrame();
            DrawVerticalLine();
            DrawHorizontalLine();
            DrawCamp();
        }

        private void DrawFrame()
        {
            G.DrawLine(_blackPen, _topLeftPoint, _topRightPoint);
            G.DrawLine(_blackPen, _topRightPoint, _bottomRightPoint);
            G.DrawLine(_blackPen, _bottomRightPoint, _bottomLeftPoint);
            G.DrawLine(_blackPen, _bottomLeftPoint, _topLeftPoint);
        }

        private void DrawCamp()
        {
            G.DrawLine(
                _blackPen,
                GetChessboard(9, 3).ButtonCoordinate.X + 20,
                GetChessboard(9, 3).ButtonCoordinate.Y + 20,
                GetChessboard(7, 5).ButtonCoordinate.X + 20,
                GetChessboard(7, 5).ButtonCoordinate.Y + 20
                );
            G.DrawLine(
                _blackPen,
                GetChessboard(7, 3).ButtonCoordinate.X + 20,
                GetChessboard(7, 3).ButtonCoordinate.Y + 20,
                GetChessboard(9, 5).ButtonCoordinate.X + 20,
                GetChessboard(9, 5).ButtonCoordinate.Y + 20
                );
            G.DrawLine(
                _blackPen,
                GetChessboard(0, 3).ButtonCoordinate.X + 20,
                GetChessboard(0, 3).ButtonCoordinate.Y + 20,
                GetChessboard(2, 5).ButtonCoordinate.X + 20,
                GetChessboard(2, 5).ButtonCoordinate.Y + 20
                );
            G.DrawLine(
                _blackPen,
                GetChessboard(2, 3).ButtonCoordinate.X + 20,
                GetChessboard(2, 3).ButtonCoordinate.Y + 20,
                GetChessboard(0, 5).ButtonCoordinate.X + 20,
                GetChessboard(0, 5).ButtonCoordinate.Y + 20
                );
        }

        private void DrawVerticalLine()
        {
            for (var i = 1; i <= 7; i++)
            {
                //河上方的直線
                G.DrawLine(
                    _blackPen,
                    _topLeftPoint.X + _unit * i,
                    _topLeftPoint.Y,
                    _bottomLeftPoint.X + _unit * i,
                    _topLeftPoint.Y + _unit * 4
                );

                //河下方的直線
                G.DrawLine(
                    _blackPen,
                    _topLeftPoint.X + _unit * i,
                    _topLeftPoint.Y + _unit * 5,
                    _bottomLeftPoint.X + _unit * i,
                    _topLeftPoint.Y + _unit * 9
                );
            }
        }

        private void DrawHorizontalLine()
        {
            G.DrawLine(
                _blackPen,
                _topLeftPoint.X,
                _topLeftPoint.Y + 60,
                _topRightPoint.X,
                _topRightPoint.Y + 60
                );
            G.DrawLine(
                _blackPen,
                _topLeftPoint.X,
                _topLeftPoint.Y + 120,
                _topRightPoint.X,
                _topRightPoint.Y + 120
                );
            G.DrawLine(
                _blackPen,
                _topLeftPoint.X,
                _topLeftPoint.Y + 180,
                _topRightPoint.X,
                _topRightPoint.Y + 180
                );
            G.DrawLine(
                _blackPen,
                _topLeftPoint.X,
                _topLeftPoint.Y + 240,
                _topRightPoint.X,
                _topRightPoint.Y + 240
                );
            G.DrawLine(
                _blackPen,
                _topLeftPoint.X,
                _topLeftPoint.Y + 300,
                _topRightPoint.X,
                _topRightPoint.Y + 300
                );
            G.DrawLine(
                _blackPen,
                _topLeftPoint.X,
                _topLeftPoint.Y + 360,
                _topRightPoint.X,
                _topRightPoint.Y + 360
                );
            G.DrawLine(
                _blackPen,
                _topLeftPoint.X,
                _topLeftPoint.Y + 420,
                _topRightPoint.X,
                _topRightPoint.Y + 420
                );
            G.DrawLine(
                _blackPen,
                _topLeftPoint.X,
                _topLeftPoint.Y + 480,
                _topRightPoint.X,
                _topRightPoint.Y + 480
                );
        }

        public void ClearChessboard()
        {
            G.Clear(SystemColors.Control);
            foreach(var btn in _buttons)
            {
                if(btn != null)
                {
                    btn.Dispose();
                }
            }
        }

        //private void InitializeBtnPositions()
        //{
        //    var Y = _topLeftPoint.Y - 20;
        //    for (var y = 0; y < 10; y++)
        //    {
        //        var X = _topLeftPoint.Y - 20;
        //        for (var x = 0; x < 9; x++)
        //        {
        //            var position = new Point(X, Y);
        //            _btnPostitions[y, x] = position;
        //            X += _unit;
        //        }
        //        Y += _unit;
        //    }
        //}

    }
}
