using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess.Roles
{
    class Cannon : Role
    {
        public Cannon(BattleField battleField, Factions faction)
            : base(battleField, faction)
        {
            _battleField = battleField;
            if (faction == Factions.Black)
                DisplayName = "砲";
            else
                DisplayName = "炮";
            RoleName = RoleNames.Cannon;
        }

        public override BattleFieldCoordinate[] GetAvailableMove()
        {
            List<BattleFieldCoordinate> available = new List<BattleFieldCoordinate>();
            var currentX = CurrentCoordinate.X;
            var currentY = CurrentCoordinate.Y;

            int x = 0;
            for (var i = currentX + 1; i < 10; i++)
            {
                if (_battleField.GetLocation(i, currentY).Token != null)
                    x++;
                if (x == 1)
                    continue;
                else if(x == 2)
                {
                    available.Add(new BattleFieldCoordinate(i, currentY));
                    break;
                }
                available.Add(new BattleFieldCoordinate(i, currentY));
            }

            x = 0;
            for (var i = currentX - 1; i >= 0; i--)
            {
                if (_battleField.GetLocation(i, currentY).Token != null)
                    x++;
                if (x == 1)
                    continue;
                else if (x == 2)
                {
                    available.Add(new BattleFieldCoordinate(i, currentY));
                    break;
                }
                available.Add(new BattleFieldCoordinate(i, currentY));
            }

            x = 0;
            for (var i = currentY + 1; i < 9; i++)
            {
                if (_battleField.GetLocation(currentX, i).Token != null)
                    x++;
                if (x == 1)
                    continue;
                else if (x == 2)
                {
                    available.Add(new BattleFieldCoordinate(currentX, i));
                    break;
                }
                available.Add(new BattleFieldCoordinate(currentX, i));
            }

            x = 0;
            for (var i = currentY - 1; i >= 0; i--)
            {
                if (_battleField.GetLocation(currentX, i).Token != null)
                    x++;
                if (x == 1)
                    continue;
                else if (x == 2)
                {
                    available.Add(new BattleFieldCoordinate(currentX, i));
                    break;
                }
                available.Add(new BattleFieldCoordinate(currentX, i));
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
