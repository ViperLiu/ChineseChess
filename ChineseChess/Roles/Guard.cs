using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess.Roles
{
    class Guard : Role
    {
        public Guard(BattleField battleField, Factions faction)
            : base(battleField, faction)
        {
            _battleField = battleField;
            if (faction == Factions.Black)
                DisplayName = "士";
            else
                DisplayName = "仕";
            RoleName = RoleNames.Guard;
        }

        public override BattleFieldCoordinate[] GetAvailableMove()
        {
            List<BattleFieldCoordinate> available = new List<BattleFieldCoordinate>();
            var currentX = CurrentCoordinate.X;
            var currentY = CurrentCoordinate.Y;
            var targetCamp = Faction == Factions.Black ? _battleField.BlackCampArea : _battleField.RedCampArea;
            foreach (var camp in targetCamp)
            {
                if (Math.Abs(camp.X - currentX) == 1 && Math.Abs(camp.Y - currentY) == 1)
                {
                    available.Add(camp);
                }
            }

            // if the location is occupied by same faction token, 
            // remove the location from the list
            for (var i = available.Count - 1; i >= 0; i--)
            {
                var location = _battleField.GetLocation(available[i]);
                if (location.Token == null)
                    continue;
                if (location.Token.Role.Faction == Faction)
                    available.RemoveAt(i);
            }

            return available.ToArray();
        }
    }
}
