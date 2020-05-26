using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    public abstract class Role
    {
        public string DisplayName;
        public RoleNames RoleName;
        public Factions Faction;
        public BattleFieldCoordinate CurrentCoordinate;

        protected BattleField _battleField;
        

        public Role(BattleField battleField, Factions faction)
        {
            _battleField = battleField;
            Faction = faction;
        }

        public abstract BattleFieldCoordinate[] GetAvailableMove();

    }
}
