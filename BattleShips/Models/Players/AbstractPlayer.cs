// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Enums;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Models.Players
{
    public abstract class AbstractPlayer : IPlayer
    {
        protected IBattleShipBoard _selfBoard;
        protected IBattleShipBoard _opponentBoard;
        private IShipGenerator _shipGenerator;

        protected AbstractPlayer(PlayerType player, IShell shell, PlayerBoardConfig config)
        {
            Shell = shell;

            Name = player.ToString();
            Type = player;

            _selfBoard = new BattleShipBoard(Shell, config.SelfBoardStartPoint);
            _opponentBoard = new BattleShipBoard(Shell, config.OpponentBoardStartPoint);
        }

        public PlayerType Type { get; }

        public string Name { get; }

        public IBattleShipBoard Board => _selfBoard;

        public IBattleShipBoard PolygonBoard => _opponentBoard;

        public IShipGenerator ShipGenerator
        {
            get => _shipGenerator ?? (_shipGenerator = CreateShipGenerator());
        }

        protected IShell Shell { get; }

        public IShip CreateShip(Point point)
        {
            var ship = ShipGenerator.CreateShip(point);

            if (ship != null)
                Board.AddShip(ship);

            return ship;
        }

        public void ShowBoards()
        {
            Board.Draw();
            _opponentBoard.Draw();
        }

        public void MakeShot(Point point, bool isEmpty)
        {
            if (isEmpty)
            {
                _opponentBoard.SetCellValue(point.X, point.Y, GameConstants.Miss);
            }
            else
            {
                _opponentBoard.SetCellValue(point.X, point.Y, GameConstants.Got);
            }
        }

        public void MakeMove(Point point)
        {
            _opponentBoard.Draw();
            _opponentBoard.DrawSelectedCell(point);
        }

        protected abstract IShipGenerator CreateShipGenerator();
    }
}