// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Abstract.Visuals;
using BattleShips.Enums;
using BattleShips.Metadata;
using BattleShips.Misc;
using BattleShips.Misc.Args;
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

        private OpponentShipGenerator _opponentShip;

        protected AbstractPlayer(PlayerType player, IVisualContext shell, PlayerBoardConfig config)
        {
            Shell = shell;

            Name = player.ToString();
            Type = player;

            _selfBoard = new BattleShipBoard(Shell, config.SelfBoardStartPoint);

            _opponentBoard = new BattleShipBoard(Shell, config.OpponentBoardStartPoint);
            _opponentShip = new OpponentShipGenerator(_opponentBoard, ShipFactory);

            _shipGenerator = new RandomShipGenerator(this);
        }

        public event EventHandler ResetOcurred;

        public event EventHandler<CellChangedEventArgs> CellCollectionChanged = delegate { };

        public PlayerType Type { get; }

        public string Name { get; }

        public IBattleShipBoard Board => _selfBoard;

        public IBattleShipBoard PolygonBoard => _opponentBoard;

        public IShipFactory ShipFactory
        {
            get => _shipFactory ?? (_shipFactory = CreateShipFactory());
        }

        protected IVisualContext Shell { get; }

        public IShip CreateShip(Point point)
        {
            var ship = ShipFactory.CreateShip(point);

            if (ship != null)
                Board.AddShip(ship);

            return ship;
        }

        public void ShowBoards()
        {
            Board.Show();
            PolygonBoard.Show();
        }

        public void MakeShot(Point point, bool isEmpty, bool isAlive)
        {
            var cell = _opponentBoard.GetCellValue(point);
            var OldCell = new BoardCell(cell.Point, cell.Value);

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

            RaiseCellCollectionChanged(new CellChangedEventArgs(OldCell, cell, Type));
        }

        public void Reset()
        {
            Board.Reset();
            PolygonBoard.Reset();
            RaiseResetOccured();
        }

        public void MakeMove(Point point)
        {
            _opponentBoard.Show();
            _opponentBoard.SelectCell(point);
        }

        public void FillShips() => _shipGenerator.PlaceShips();

        public void Load(PlayerDto player)
        {
            Board.Load(player.Board, ShipFactory);
            PolygonBoard.Load(player.Polygon, ShipFactory);
        }

        protected abstract IShipFactory CreateShipFactory();

        private void RaiseCellCollectionChanged(CellChangedEventArgs args) => CellCollectionChanged(this, args);

        private void RaiseResetOccured() => ResetOcurred?.Invoke(this, null);

        private IShip CreateValidShip(Point point, bool isAlive) => _opponentShip.Create(point, isAlive);
    }
}