// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Ships;
using BattleShips.Ships.Generators;

using TicTacToe;

namespace BattleShips.Models.Players
{
    public abstract class AbstractPlayer : IPlayer
    {
        protected IBattleShipBoard _selfBoard;
        protected IBattleShipBoard _opponentBoard;
        private IShipGenerator _shipGenerator;
        private CreateOpponentShip _opponentShip;

        protected AbstractPlayer(PlayerType player, IShell shell, PlayerBoardConfig config)
        {
            Shell = shell;

            Name = player.ToString();
            Type = player;

            _selfBoard = new BattleShipBoard(Shell, config.SelfBoardStartPoint);
            _opponentBoard = new BattleShipBoard(Shell, config.OpponentBoardStartPoint);
            _opponentShip = new CreateOpponentShip(_opponentBoard, ShipGenerator);
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

        public void MakeShot(Point point, bool isEmpty, bool isAlive)
        {
            if (_opponentBoard.GetCellValue(point.X, point.Y).Value == GameConstants.Miss ||
                _opponentBoard.GetCellValue(point.X, point.Y).Value == GameConstants.Got)
            {
                return;
            }

            if (isEmpty)
            {
                _opponentBoard.SetCellValue(point.X, point.Y, GameConstants.Miss);
            }
            else
            {
                var ship = CreateValidShip(point);

                _opponentBoard.ChangeOrAddShip(ship.Start, ship);

                _opponentBoard.SetCellValue(point.X, point.Y, GameConstants.Got);
                if (!isAlive)
                {
                    for (int i = 0; i < ship.Deck; i++)
                    {
                        ship.ApplyDamage(true);
                    }
                }

                //_opponentBoard.ProcessShot(point);
            }
        }

        public void MakeMove(Point point)
        {
            _opponentBoard.Draw();
            _opponentBoard.DrawSelectedCell(point);
        }

        public void FillBoard()
        {
            var ships = new RandomShipGenerator(this);
            ships.PlaceShips();

            foreach (var item in _selfBoard.Ships)
            {
                for (int i = item.Start.X; i <= item.End.X; i++)
                {
                    for (int j = item.Start.Y; j <= item.End.Y; j++)
                    {
                        _selfBoard.SetCellValue(i, j, GameConstants.Ship);
                    }
                }
            }
        }

        protected abstract IShipGenerator CreateShipGenerator();

        private IShip CreateValidShip(Point point) => _opponentShip.Create(point);
    }
}