// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Ships.Generators;

using TicTacToe;

namespace BattleShips.Models.Players
{
    public abstract class AbstractPlayer : IPlayer
    {
        protected IBattleShipBoard _selfBoard;
        protected IBattleShipBoard _opponentBoard;

        private IShipFactory _shipFactory;
        private RandomShipGenerator _shipGenerator;

        private OpponentShip _opponentShip;

        protected AbstractPlayer(PlayerType player, IShell shell, PlayerBoardConfig config)
        {
            Shell = shell;

            Name = player.ToString();
            Type = player;

            _selfBoard = new BattleShipBoard(Shell, config.SelfBoardStartPoint);

            _opponentBoard = new BattleShipBoard(Shell, config.OpponentBoardStartPoint);
            _opponentShip = new OpponentShip(_opponentBoard, ShipFactory);

            _shipGenerator = new RandomShipGenerator(this);
        }

        public PlayerType Type { get; }

        public string Name { get; }

        public IBattleShipBoard Board => _selfBoard;

        public IBattleShipBoard PolygonBoard => _opponentBoard;

        public IShipFactory ShipFactory
        {
            get => _shipFactory ?? (_shipFactory = CreateShipFactory());
        }

        protected IShell Shell { get; }

        public IShip CreateShip(Point point)
        {
            var ship = ShipFactory.CreateShip(point);

            if (ship != null)
                Board.AddShip(ship);

            return ship;
        }

        public void ShowBoards()
        {
            Board.Draw();
            PolygonBoard.Draw();
        }

        public void MakeShot(Point point, bool isEmpty, bool isAlive)
        {
            var cell = PolygonBoard.GetCellValue(point);

            if (cell.Value == GameConstants.Miss || cell.Value == GameConstants.Got)
                return;

            if (!isEmpty)
            {
                var ship = CreateValidShip(point, isAlive);

                PolygonBoard.ChangeOrAddShip(ship.Start, ship);
                PolygonBoard.SetCellValue(point, GameConstants.Got);
            }
            else
                PolygonBoard.SetCellValue(point, GameConstants.Miss);
        }

        public void MakeMove(Point point)
        {
            _opponentBoard.Draw();
            _opponentBoard.DrawSelectedCell(point);
        }

        public void FillShips() => _shipGenerator.PlaceShips();

        protected abstract IShipFactory CreateShipFactory();

        private IShip CreateValidShip(Point point, bool isAlive) => _opponentShip.Create(point, isAlive);
    }
}