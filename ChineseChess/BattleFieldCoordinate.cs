using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    public struct BattleFieldCoordinate
    {
        public int X;
        public int Y;
        public BattleFieldCoordinate(int x, int y)
        {
            if (x < 0 || x > 9)
                throw new Exception("X 超出範圍");
            if (y < 0 || y > 8)
                throw new Exception("Y 超出範圍");
            X = x;
            Y = y;
        }

        public static bool IsLegal(int x, int y)
        {
            if (x < 0 || x > 9)
                return false;
            if (y < 0 || y > 8)
                return false;
            return true;
        }

        public override string ToString()
        {
            return string.Format("X = {0}, Y = {1}", X, Y);
        }
    }
}
