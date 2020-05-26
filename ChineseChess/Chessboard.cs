using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChineseChess
{
    public class Chessboard
    {
        public readonly Point ButtonCoordinate;
        public Button Button;

        public Chessboard(Point point)
        {
            ButtonCoordinate = point;
            Button = null;
        }

        public void PlaceButton(Button btn)
        {
            Button = btn;
            if (!Button.InvokeRequired)
            {
                Button.Location = ButtonCoordinate;
            }
            else
            {
                Button.Invoke((MethodInvoker)delegate
                {
                    Button.Location = ButtonCoordinate;
                });
            }
        }

        public void RemoveButton()
        {
            Button = null;
        }

        public void RemoveButtonFromGame()
        {
            if (Button == null)
                return;
            if (!Button.InvokeRequired)
            {
                Button.Dispose();
            }
            else
            {
                Button.Invoke((MethodInvoker)delegate
                {
                    Button.Dispose();
                });
            }
            
            Button = null;
        }
    }
}
