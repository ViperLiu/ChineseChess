using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    public class GamePhase
    {
        public Phase CurrentPhase;
        public Factions Turn;

        public GamePhase()
        {
            CurrentPhase = Phase.WaitingForSelect;
            Turn = Factions.Red;
        }

        public void WaitForSelect()
        {
            CurrentPhase = Phase.WaitingForSelect;
            Console.WriteLine(CurrentPhase);
        }

        public void WaitForMove()
        {
            CurrentPhase = Phase.WaitingForMove;
            Console.WriteLine(CurrentPhase);
        }

        public void SwitchTurn()
        {
            if (Turn == Factions.Red)
                Turn = Factions.Black;
            else
                Turn = Factions.Red;
            WaitForSelect();
        }

        public enum Phase
        {
            WaitingForSelect = 0,
            WaitingForMove = 1
        }
    }
}
