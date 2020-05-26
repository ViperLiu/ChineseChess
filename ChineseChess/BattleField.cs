using ChineseChess.Roles;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseChess
{
    public abstract class BattleField
    {
        protected readonly Location[,] _locations = new Location[10, 9];
        public readonly List<Token> Tokens = new List<Token>();
        public readonly List<BattleFieldCoordinate> RedCampArea = new List<BattleFieldCoordinate>();
        public readonly List<BattleFieldCoordinate> BlackCampArea = new List<BattleFieldCoordinate>();
        protected Presenters.FormPresenter _presenter;
        protected ChessboardDisplayer _chessboardDisplayer;
        public GamePhase GamePhase = new GamePhase();
        public Token CurrentActivatedToken;
        public string NickName;

        public BattleField(Presenters.FormPresenter presenter, string nickName)
        {
            _presenter = presenter;
            NickName = nickName;
            _chessboardDisplayer = _presenter.ChessboardDisplayer;
        }

        public void CreateBattleField()
        {
            InitializeLocations();
            GetCampAreas();
            CreateTokens();
            RegistTokenEvents();

            _chessboardDisplayer.CreateButtons();
            _chessboardDisplayer.DrawChessboard();
        }

        public abstract BattleFieldCoordinate ConvertPictureboxPointToCoordinate(int pbPointX, int pbPointY);

        protected abstract void InitializeLocations();

        public abstract void MoveToken(BattleFieldCoordinate from, BattleFieldCoordinate to);

        protected void GetCampAreas()
        {
            //RedCamp
            for (var x = 7; x < 10; x++)
            {
                for (var y = 3; y < 6; y++)
                {
                    RedCampArea.Add(new BattleFieldCoordinate(x, y));
                }
            }

            //BlackCamp
            for (var x = 0; x < 3; x++)
            {
                for (var y = 3; y < 6; y++)
                {
                    BlackCampArea.Add(new BattleFieldCoordinate(x, y));
                }
            }
        }

        protected void CreateTokens()
        {
            Token token1 = new Token(new BattleFieldCoordinate(9, 4), new General(this, Factions.Red));
            _locations[9, 4].PlaceToken(token1);
            Tokens.Add(token1);

            Token token2 = new Token(new BattleFieldCoordinate(0, 4), new General(this, Factions.Black));
            _locations[0, 4].PlaceToken(token2);
            Tokens.Add(token2);

            //士
            for (int i = 0, indexY = 3; i < 2; i++, indexY = 8 - indexY)
            {
                Token tokenBlack = new Token(new BattleFieldCoordinate(0, indexY), new Guard(this, Factions.Black));
                _locations[0, indexY].PlaceToken(tokenBlack);
                Tokens.Add(tokenBlack);

                Token tokenRed = new Token(new BattleFieldCoordinate(9, indexY), new Guard(this, Factions.Red));
                _locations[9, indexY].PlaceToken(tokenRed);
                Tokens.Add(tokenRed);
            }

            //象
            for (int i = 0, indexY = 2; i < 2; i++, indexY = 8 - indexY)
            {
                Token tokenBlack = new Token(new BattleFieldCoordinate(0, indexY), new Elephant(this, Factions.Black));
                _locations[0, indexY].PlaceToken(tokenBlack);
                Tokens.Add(tokenBlack);

                Token tokenRed = new Token(new BattleFieldCoordinate(9, indexY), new Elephant(this, Factions.Red));
                _locations[9, indexY].PlaceToken(tokenRed);
                Tokens.Add(tokenRed);
            }

            //車
            for (int i = 0, indexY = 0; i < 2; i++, indexY = 8 - indexY)
            {
                Token tokenBlack = new Token(new BattleFieldCoordinate(0, indexY), new Tank(this, Factions.Black));
                _locations[0, indexY].PlaceToken(tokenBlack);
                Tokens.Add(tokenBlack);

                Token tokenRed = new Token(new BattleFieldCoordinate(9, indexY), new Tank(this, Factions.Red));
                _locations[9, indexY].PlaceToken(tokenRed);
                Tokens.Add(tokenRed);
            }

            //馬
            for (int i = 0, indexY = 1; i < 2; i++, indexY = 8 - indexY)
            {
                Token tokenBlack = new Token(new BattleFieldCoordinate(0, indexY), new Horse(this, Factions.Black));
                _locations[0, indexY].PlaceToken(tokenBlack);
                Tokens.Add(tokenBlack);

                Token tokenRed = new Token(new BattleFieldCoordinate(9, indexY), new Horse(this, Factions.Red));
                _locations[9, indexY].PlaceToken(tokenRed);
                Tokens.Add(tokenRed);
            }

            //炮
            for (int i = 0, indexY = 1; i < 2; i++, indexY = 8 - indexY)
            {
                Token tokenBlack = new Token(new BattleFieldCoordinate(2, indexY), new Cannon(this, Factions.Black));
                _locations[2, indexY].PlaceToken(tokenBlack);
                Tokens.Add(tokenBlack);

                Token tokenRed = new Token(new BattleFieldCoordinate(7, indexY), new Cannon(this, Factions.Red));
                _locations[7, indexY].PlaceToken(tokenRed);
                Tokens.Add(tokenRed);
            }

            //卒、兵
            for (int i = 0, indexY = 0; i < 5; i++, indexY += 2)
            {
                Token tokenBlack = new Token(new BattleFieldCoordinate(3, indexY), new Infantry(this, Factions.Black));
                _locations[3, indexY].PlaceToken(tokenBlack);
                Tokens.Add(tokenBlack);

                Token tokenRed = new Token(new BattleFieldCoordinate(6, indexY), new Infantry(this, Factions.Red));
                _locations[6, indexY].PlaceToken(tokenRed);
                Tokens.Add(tokenRed);
            }

        }

        protected void RegistTokenEvents()
        {
            foreach (var token in Tokens)
            {
                token.OnActivated += OnTokenActivated;
                token.OnDeactivated += OnTokenDeactivated;
                token.OnBecomeTarget += OnBecomeTarget;
            }
        }

        public void Clear()
        {
            Tokens.Clear();
            _chessboardDisplayer.ClearChessboard();
        }

        public Location GetLocation(BattleFieldCoordinate coordinate)
        {
            var x = coordinate.X;
            var y = coordinate.Y;

            return _locations[x, y];
        }

        public Location GetLocation(int x, int y)
        {
            return _locations[x, y];
        }

        public bool IsLocationOccupied(BattleFieldCoordinate coordinate)
        {
            if (GetLocation(coordinate).Token != null)
                return true;
            return false;
        }

        protected void OnTokenActivated(object sender, EventArgs e)
        {
            foreach (var t in Tokens)
            {
                if (t.Equals(sender))
                    continue;
                t.Deactivate();
            }
            var token = sender as Token;
            var available = token.Role.GetAvailableMove();
            _chessboardDisplayer.HighlightLocation(available);
            CurrentActivatedToken = token;
            GamePhase.WaitForMove();
        }

        protected void OnTokenDeactivated(object sender, EventArgs e)
        {
            var token = sender as Token;
            var available = token.Role.GetAvailableMove();
            _chessboardDisplayer.EraseHighlight(available);
            CurrentActivatedToken = null;
            GamePhase.WaitForSelect();
        }

        protected void OnBecomeTarget(object sender, EventArgs e)
        {
            var tokenBeingAttacked = sender as Token;
            MoveToken(CurrentActivatedToken.Coordinate, tokenBeingAttacked.Coordinate);
        }
    }
}
