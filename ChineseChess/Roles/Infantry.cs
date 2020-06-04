using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess.Roles
{
    class Infantry : Role
    {
        private bool _isCrossedRiver = false;
        public Infantry(BattleField battleField, Factions faction)
            : base(battleField, faction)
        {
            _battleField = battleField;
            if (faction == Factions.Black)
                DisplayName = "卒";
            else
                DisplayName = "兵";
            RoleName = RoleNames.Infantry;
        }

        private bool CanMoveAnyForward()
        {
            var currentX = CurrentCoordinate.X;
            if(Faction == Factions.Red && currentX - 1 < 0)
            {
                return false;
            }
            else if (Faction == Factions.Black && currentX + 1 > 9)
            {
                return false;
            }

            return true;
        }

        public override BattleFieldCoordinate[] GetAvailableMove()
        {
            List<BattleFieldCoordinate> available = new List<BattleFieldCoordinate>();
            var currentX = CurrentCoordinate.X;
            var currentY = CurrentCoordinate.Y;
            if(Faction == Factions.Red)
            {
                _isCrossedRiver = currentX < 5 ? true : false;
                if (!_isCrossedRiver)
                    available.Add(new BattleFieldCoordinate(currentX - 1, currentY));
                else
                {
                    if(CanMoveAnyForward())
                        available.Add(new BattleFieldCoordinate(currentX - 1, currentY));
                    if(BattleFieldCoordinate.IsLegal(currentX, currentY - 1))
                        available.Add(new BattleFieldCoordinate(currentX, currentY - 1));
                    if (BattleFieldCoordinate.IsLegal(currentX, currentY + 1))
                        available.Add(new BattleFieldCoordinate(currentX, currentY + 1));
                }
            }
            else
            {
                _isCrossedRiver = currentX > 4 ? true : false;
                if (!_isCrossedRiver)
                    available.Add(new BattleFieldCoordinate(currentX + 1, currentY));
                else
                {
                    if (CanMoveAnyForward())
                        available.Add(new BattleFieldCoordinate(currentX + 1, currentY));
                    if (BattleFieldCoordinate.IsLegal(currentX, currentY - 1))
                        available.Add(new BattleFieldCoordinate(currentX, currentY - 1));
                    if (BattleFieldCoordinate.IsLegal(currentX, currentY + 1))
                        available.Add(new BattleFieldCoordinate(currentX, currentY + 1));
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
