using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess.Roles
{
    class Elephant : Role
    {
        public Elephant(BattleField battleField, Factions faction)
            : base(battleField, faction)
        {
            _battleField = battleField;
            if (faction == Factions.Black)
                DisplayName = "象";
            else
                DisplayName = "相";
            RoleName = RoleNames.Elephant;
        }

        private bool IsCoordinateCrossRiver(BattleFieldCoordinate coordinate)
        {
            if(Faction == Factions.Red && coordinate.X < 5)
            {
                return true;
            }
            if (Faction == Factions.Black && coordinate.X > 4)
            {
                return true;
            }
            return false;
        }

        private bool IsBlocked(BattleFieldCoordinate selfCoordinate, BattleFieldCoordinate targetCoordinate)
        {
            var middleX = (selfCoordinate.X + targetCoordinate.X) / 2;
            var middleY = (selfCoordinate.Y + targetCoordinate.Y) / 2;

            var middleLocation = _battleField.GetLocation(middleX, middleY);
            if (middleLocation.Token != null)
                return true;
            return false;
        }

        public override BattleFieldCoordinate[] GetAvailableMove()
        {
            List<BattleFieldCoordinate> available = new List<BattleFieldCoordinate>();
            var currentX = CurrentCoordinate.X;
            var currentY = CurrentCoordinate.Y;

            if (BattleFieldCoordinate.IsLegal(currentX + 2, currentY + 2))
                available.Add(new BattleFieldCoordinate(currentX + 2, currentY + 2));
            if (BattleFieldCoordinate.IsLegal(currentX + 2, currentY - 2))
                available.Add(new BattleFieldCoordinate(currentX + 2, currentY - 2));
            if (BattleFieldCoordinate.IsLegal(currentX - 2, currentY + 2))
                available.Add(new BattleFieldCoordinate(currentX - 2, currentY + 2));
            if (BattleFieldCoordinate.IsLegal(currentX - 2, currentY - 2))
                available.Add(new BattleFieldCoordinate(currentX - 2, currentY - 2));

            
            for (var i = available.Count - 1; i >= 0; i--)
            {
                var location = _battleField.GetLocation(available[i]);

                // if the location is cross the river, 
                // remove the location from the list
                if (IsCoordinateCrossRiver(available[i]))
                    available.RemoveAt(i);

                // if there is a token block in the middle, 
                // remove the location from the list
                else if (IsBlocked(CurrentCoordinate, available[i]))
                    available.RemoveAt(i);

                else
                {
                    // if the location is occupied by same faction token, 
                    // remove the location from the list
                    if (location.Token == null)
                        continue;
                    if (location.Token.Role.Faction == Faction)
                        available.RemoveAt(i);
                }
            }

            return available.ToArray();
        }
    }
}
