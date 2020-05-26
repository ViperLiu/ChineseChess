using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    public class MoveInfo
    {
        public readonly int FromX;
        public readonly int FromY;
        public readonly int ToX;
        public readonly int ToY;

        public MoveInfo(int fromX, int fromY, int toX, int toY)
        {
            FromX = fromX;
            FromY = fromY;
            ToX = toX;
            ToY = toY;
        }

        public MoveInfo(BattleFieldCoordinate fromCoor, BattleFieldCoordinate toCoor)
        {
            FromX = fromCoor.X;
            FromY = fromCoor.Y;
            ToX = toCoor.X;
            ToY = toCoor.Y;
        }

        public override string ToString()
        {
            return string.Format("{0}&{1}&{2}&{3}", FromX, FromY, ToX, ToY);
        }
    }
}
