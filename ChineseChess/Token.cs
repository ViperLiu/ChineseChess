using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChineseChess
{
    public class Token
    {
        //public Button Button;
        public Role Role;
        public BattleFieldCoordinate Coordinate;
        public EventHandler OnActivated;
        public EventHandler OnDeactivated;
        public EventHandler OnBecomeTarget;
        public bool IsActivated = false;

        public Token(BattleFieldCoordinate coordinate, Role role)
        {
            //var indexX = coordinate.X;
            //var indexY = coordinate.Y;
            //Color color;
            //if (role.Faction == Factions.Black)
            //    color = Color.Black;
            //else
            //    color = Color.Red;
            Coordinate = coordinate;
            Role = role;
            Role.CurrentCoordinate = coordinate;
            //Button = new Button
            //{
            //    Text = Role.DisplayName,
            //    Location = ServerBattleField._locations[indexX, indexY].CoordinateToPlaceBtn,
            //    ForeColor = color,
            //    FlatStyle = FlatStyle.Flat,
            //    Font = new Font(new FontFamily("標楷體"), 16),
            //    Size = new Size(40, 40),
            //};
            //Button.Click += new EventHandler(Button_OnClick);
        }

        //private void Button_OnClick(object sender, EventArgs e)
        //{
        //    var caseChangeActiveatedToken =
        //        ServerBattleField.GamePhase.CurrentPhase == GamePhase.Phase.WaitingForMove &&
        //        ServerBattleField.GamePhase.Turn == Role.Faction &&
        //        !IsActivated;

        //    var caseActivatedToken =
        //        ServerBattleField.GamePhase.CurrentPhase == GamePhase.Phase.WaitingForSelect &&
        //        ServerBattleField.GamePhase.Turn == Role.Faction &&
        //        !IsActivated;

        //    var caseDeactivatedToken =
        //        ServerBattleField.GamePhase.CurrentPhase == GamePhase.Phase.WaitingForMove &&
        //        ServerBattleField.GamePhase.Turn == Role.Faction &&
        //        IsActivated;

        //    var caseAttackEnemyToken =
        //        ServerBattleField.GamePhase.CurrentPhase == GamePhase.Phase.WaitingForMove &&
        //        ServerBattleField.GamePhase.Turn != Role.Faction;

        //    var caseNotYourTurn =
        //        ServerBattleField.GamePhase.CurrentPhase == GamePhase.Phase.WaitingForSelect &&
        //        ServerBattleField.GamePhase.Turn != Role.Faction;

        //    if (caseActivatedToken || caseChangeActiveatedToken)
        //    {
        //        Activate();
        //    }
        //    else if(caseDeactivatedToken)
        //    {
        //        Deactivate();
        //    }
        //    else if(caseAttackEnemyToken)
        //    {
        //        OnBecomeTarget(this, new EventArgs());
        //        Console.WriteLine("attack!");
        //    }
        //    else if(caseNotYourTurn)
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        throw new Exception("按鈕判定發生邏輯上的錯誤");
        //    }
        //}

        public void Activate()
        {
            if (IsActivated)
                return;

            IsActivated = true;
            OnActivated(this, new EventArgs());
        }

        public void Deactivate()
        {
            if (!IsActivated)
                return;

            IsActivated = false;
            OnDeactivated(this, new EventArgs());
        }

        public void BecomeTarget()
        {
            OnBecomeTarget(this, new EventArgs());
        }
    }
}
