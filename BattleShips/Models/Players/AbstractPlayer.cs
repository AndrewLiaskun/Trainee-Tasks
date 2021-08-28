// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

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
        private List<IHistoryRecord> _historyRecords;
        private OpponentShipGenerator _opponentShip;

        protected AbstractPlayer(PlayerType player, IVisualContext shell, PlayerBoardConfig config)
        {
            Shell = shell;

            Name = player.ToString();
            Type = player;

            _historyRecords = new List<IHistoryRecord>();
            _selfBoard = new BattleShipBoard(Shell, config.SelfBoardStartPoint);

            _opponentBoard = new BattleShipBoard(Shell, config.OpponentBoardStartPoint);
            _opponentShip = new OpponentShipGenerator(_opponentBoard, ShipFactory);

            _shipGenerator = new RandomShipGenerator(this);
        }

        protected AbstractPlayer(string name, PlayerType player, IVisualContext shell,
            PlayerBoardConfig config, List<IHistoryRecord> playerHistory = null) : this(player, shell, config)
        {
            Name = name;
            _historyRecords = new List<IHistoryRecord>();
            _historyRecords = playerHistory;
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

        public IReadOnlyList<IHistoryRecord> PlayerHistory => _historyRecords;

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
            var cell = new BoardCell(point, _opponentBoard.GetCellValue(point).Value);
            var OldCell = new BoardCell(cell.Point, cell.Value);

            if (cell.Value == GameConstants.Miss || cell.Value == GameConstants.Got)
                return;

            if (!isEmpty)
            {
                var ship = CreateValidShip(point, isAlive);

                PolygonBoard.ChangeOrAddShip(point, ship);
                PolygonBoard.SetCellValue(point, GameConstants.Got);
            }
            else
                PolygonBoard.SetCellValue(point, GameConstants.Miss);

            RaiseCellCollectionChanged(new CellChangedEventArgs(OldCell, cell, Name));
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

        public void RefreshHistory(IReadOnlyList<IHistoryRecord> history)
        {
            if (history == null) return;
            _historyRecords.Clear();

            foreach (var item in history)
                _historyRecords.Add(item);
        }

        protected abstract IShipFactory CreateShipFactory();

        private void RaiseCellCollectionChanged(CellChangedEventArgs args) => CellCollectionChanged(this, args);

        private void RaiseResetOccured() => ResetOcurred?.Invoke(this, null);

        private IShip CreateValidShip(Point point, bool isAlive) => _opponentShip.Create(point, isAlive);
    }
}