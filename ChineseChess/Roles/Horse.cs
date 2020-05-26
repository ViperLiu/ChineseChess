using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess.Roles
{
    class Horse : Role
    {
        public Horse(BattleField battleField, Factions faction)
            : base(battleField, faction)
        {
            _battleField = battleField;
            if (faction == Factions.Black)
                DisplayName = "馬";
            else
                DisplayName = "傌";
            RoleName = RoleNames.Tank;
        }

        private List<BattleFieldCoordinate> GetAllPossibleMove()
        {
            List<BattleFieldCoordinate> available = new List<BattleFieldCoordinate>();
            var currentX = CurrentCoordinate.X;
            var currentY = CurrentCoordinate.Y;

            if (BattleFieldCoordinate.IsLegal(currentX + 2, currentY + 1))
                available.Add(new BattleFieldCoordinate(currentX + 2, currentY + 1));
            if (BattleFieldCoordinate.IsLegal(currentX + 2, currentY - 1))
                available.Add(new BattleFieldCoordinate(currentX + 2, currentY - 1));
            if (BattleFieldCoordinate.IsLegal(currentX - 2, currentY + 1))
                available.Add(new BattleFieldCoordinate(currentX - 2, currentY + 1));
            if (BattleFieldCoordinate.IsLegal(currentX - 2, currentY - 1))
                available.Add(new BattleFieldCoordinate(currentX - 2, currentY - 1));

            if (BattleFieldCoordinate.IsLegal(currentX + 1, currentY + 2))
                available.Add(new BattleFieldCoordinate(currentX + 1, currentY + 2));
            if (BattleFieldCoordinate.IsLegal(currentX + 1, currentY - 2))
                available.Add(new BattleFieldCoordinate(currentX + 1, currentY - 2));
            if (BattleFieldCoordinate.IsLegal(currentX - 1, currentY + 2))
                available.Add(new BattleFieldCoordinate(currentX - 1, currentY + 2));
            if (BattleFieldCoordinate.IsLegal(currentX - 1, currentY - 2))
                available.Add(new BattleFieldCoordinate(currentX - 1, currentY - 2));

            return available;
        }

        private bool IsBlocked(BattleFieldCoordinate destCoordinate)
        {
            var fromX = CurrentCoordinate.X;
            var fromY = CurrentCoordinate.Y;
            var destX = destCoordinate.X;
            var destY = destCoordinate.Y;

            if(destX - fromX == 2)
            {
                if (_battleField.GetLocation(fromX + 1, fromY).Token != null)
                    return true;
            }
            else if(destX - fromX == -2)
            {
                if (_battleField.GetLocation(fromX - 1, fromY).Token != null)
                    return true;
            }
            else if (destY - fromY == 2)
            {
                if (_battleField.GetLocation(fromX, fromY + 1).Token != null)
                    return true;
            }
            else if (destY - fromY == -2)
            {
                if (_battleField.GetLocation(fromX, fromY - 1).Token != null)
                    return true;
            }
            return false;
        }

        public override BattleFieldCoordinate[] GetAvailableMove()
        {
            List<BattleFieldCoordinate> available = GetAllPossibleMove();

            
            for (var i = available.Count - 1; i >= 0; i--)
            {
                var location = _battleField.GetLocation(available[i]);

                // if the destination is blocked by another token,
                // remove the location from the list
                if (IsBlocked(available[i]))
                {
                    available.RemoveAt(i);
                    continue;
                }

                // if the location is occupied by same faction token, 
                // remove the location from the list
                if (location.Token == null)
                    continue;
                if (location.Token.Role.Faction == Faction)
                    available.RemoveAt(i);

            }

            available.ForEach(i => Console.WriteLine(i));

            return available.ToArray();
        }
    }
}
