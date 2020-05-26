using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess.Roles
{
    class Tank : Role
    {
        public Tank(BattleField battleField, Factions faction)
            : base(battleField, faction)
        {
            _battleField = battleField;
            if (faction == Factions.Black)
                DisplayName = "車";
            else
                DisplayName = "俥";
            RoleName = RoleNames.Tank;
        }

        public override BattleFieldCoordinate[] GetAvailableMove()
        {
            List<BattleFieldCoordinate> available = new List<BattleFieldCoordinate>();
            var currentX = CurrentCoordinate.X;
            var currentY = CurrentCoordinate.Y;

            for(var i = currentX + 1; i < 10; i++)
            {
                available.Add(new BattleFieldCoordinate(i, currentY));
                if (_battleField.GetLocation(i, currentY).Token != null)
                    break;
            }
            for (var i = currentX - 1; i >= 0; i--)
            {
                available.Add(new BattleFieldCoordinate(i, currentY));
                if (_battleField.GetLocation(i, currentY).Token != null)
                    break;
            }
            for (var i = currentY + 1; i < 9; i++)
            {
                available.Add(new BattleFieldCoordinate(currentX, i));
                if (_battleField.GetLocation(currentX, i).Token != null)
                    break;
            }
            for (var i = currentY - 1; i >= 0; i--)
            {
                available.Add(new BattleFieldCoordinate(currentX, i));
                if (_battleField.GetLocation(currentX, i).Token != null)
                    break;
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
