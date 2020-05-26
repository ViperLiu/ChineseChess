using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    public class ClientBattleField : BattleField
    {
        public ClientBattleField(Presenters.FormPresenter presenter, string nickName)
            : base(presenter, nickName)
        {
            _presenter = presenter;
            NickName = nickName;
        }

        public override BattleFieldCoordinate ConvertPictureboxPointToCoordinate(int pbPointX, int pbPointY)
        {
            return new BattleFieldCoordinate(9 - (pbPointY / 60), 8 - (pbPointX / 60));
        }

        public override void MoveToken(BattleFieldCoordinate from, BattleFieldCoordinate to)
        {
            var fromLocation = GetLocation(from);
            var destLocation = GetLocation(to);
            var fromChessboard = _chessboardDisplayer.GetChessboard(from);
            var toChessboard = _chessboardDisplayer.GetChessboard(to);

            var token = fromLocation.Token;
            var btn = fromChessboard.Button;

            var availables = token.Role.GetAvailableMove();
            if (!availables.Contains(to))
                return;

            _chessboardDisplayer.EraseHighlight(availables);

            fromLocation.RemoveToken();
            destLocation.PlaceToken(token);

            fromChessboard.RemoveButton();
            toChessboard.RemoveButtonFromGame();
            toChessboard.PlaceButton(btn);


            //destLocation.RemoveTokenFromGame();
            

            token.Deactivate();

            GamePhase.SwitchTurn();


            //if (!shouldNotify)
            //    return;
            //var message = new Message(new MoveInfo(from, to));
            //MessageSender.Send(Message.Serialize(message), IsClient);
            _chessboardDisplayer.DrawChessboard();
        }

        protected override void InitializeLocations()
        {
            _chessboardDisplayer.InitializeClientChessboard();
            for (var x = 0; x < 10; x++)
            {
                for (var y = 0; y < 9; y++)
                {
                    _locations[x, y] = new Location(x, y);
                }
            }
        }
    }
}
