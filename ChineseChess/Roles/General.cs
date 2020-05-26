using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess.Roles
{
    public class General : Role
    {
        public General(BattleField battleField, Factions faction)
            : base(battleField, faction)
        {
            _battleField = battleField;
            RoleName = RoleNames.General;
            if (faction == Factions.Red)
                DisplayName = "帥";
            else
                DisplayName = "將";
        }
        public override BattleFieldCoordinate[] GetAvailableMove()
        {
            List<BattleFieldCoordinate> available = new List<BattleFieldCoordinate>();
            var currentX = CurrentCoordinate.X;
            var currentY = CurrentCoordinate.Y;
            var targetCamp = Faction == Factions.Black ? _battleField.BlackCampArea : _battleField.RedCampArea;
            foreach(var camp in targetCamp)
            {
                if(Math.Abs(camp.X - currentX) == 1 && Math.Abs(camp.Y - currentY) == 0)
                {
                    available.Add(camp);
                }
                if (Math.Abs(camp.X - currentX) == 0 && Math.Abs(camp.Y - currentY) == 1)
                {
                    available.Add(camp);
                }
            }

            // if the location is occupied by same faction token, 
            // remove the location from the list
            for(var i = available.Count - 1; i >= 0; i--)
            {
                var location = _battleField.GetLocation(available[i]);
                if (location.Token == null)
                    continue;
                if (location.Token.Role.Faction == Faction)
                    available.RemoveAt(i);
            }

            //available.ForEach(i => Console.WriteLine(i));

            return available.ToArray();
        }
    }
}
