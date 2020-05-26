using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChineseChess
{
    public class Location
    {
        public int X;
        public int Y;
        //public readonly Point CoordinateToPlaceBtn;
        public Token Token;
        //public Button Button;

        public Location(int x, int y)
        {
            //CoordinateToPlaceBtn = coordinate;
            X = x;
            Y = y;
            Token = null;
        }

        public void PlaceToken(Token token)
        {
            Token = token;
            /*if (!Token.Button.InvokeRequired)
            {
                Token.Button.Location = CoordinateToPlaceBtn;
            }
            else
            {
                Token.Button.Invoke((MethodInvoker)delegate
                {
                    Token.Button.Location = CoordinateToPlaceBtn;
                });
            }*/
            
            Token.Coordinate = new BattleFieldCoordinate(X, Y);
            Token.Role.CurrentCoordinate = Token.Coordinate;
        }
        //public void RemoveTokenFromGame()
        //{
        //    if (Token == null)
        //        return;
        //    /*if (!Token.Button.InvokeRequired)
        //    {
        //        Token.Button.Dispose();
        //    }
        //    else
        //    {
        //        Token.Button.Invoke((MethodInvoker)delegate
        //        {
        //            Token.Button.Dispose();
        //        });
        //    }*/
        //    Token = null;
        //}

        public void RemoveToken()
        {
            Token = null;
        }

        public override string ToString()
        {
            return string.Format("X = {0}, Y = {1}", X, Y);
        }
    }
}
