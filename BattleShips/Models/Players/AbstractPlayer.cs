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
using BattleShips.Utils;

using TicTacToe;

using static BattleShips.Resources.Serialization;

namespace BattleShips.Models.Players
{
    public abstract class AbstractPlayer : IPlayer
    {
        protected IBattleShipBoard _selfBoard;
        protected IBattleShipBoard _opponentBoard;

        private IShipFactory _shipFactory;
        private RandomShipGenerator _shipGenerator;
        private IGameHistory _historyRecords;
        private OpponentShipGenerator _opponentShip;
        private string _name;

        protected AbstractPlayer(PlayerType player, IVisualContext shell, PlayerBoardConfig config)
        {
            Shell = shell;

            _name = player.ToString();
            Type = player;

            _historyRecords = new GameHistory();
            _selfBoard = new BattleShipBoard(Shell, config.SelfBoardStartPoint);

            _opponentBoard = new BattleShipBoard(Shell, config.OpponentBoardStartPoint);
            _opponentShip = new OpponentShipGenerator(_opponentBoard, ShipFactory);

            _shipGenerator = new RandomShipGenerator(this);
        }

        protected AbstractPlayer(string name, PlayerType player, IVisualContext shell,
            PlayerBoardConfig config, IGameHistory playerHistory = null) : this(player, shell, config)
        {
            _name = name;
            _historyRecords = new GameHistory();
            _historyRecords = playerHistory;
        }

        public event EventHandler ResetOcurred;

        public event EventHandler<CellChangedEventArgs> CellCollectionChanged = delegate { };

        public PlayerType Type { get; }

        public string Name => _name;

        public IBattleShipBoard Board => _selfBoard;

        public IBattleShipBoard PolygonBoard => _opponentBoard;

        public IShipFactory ShipFactory
        {
            get => _shipFactory ?? (_shipFactory = CreateShipFactory());
        }

        public IGameHistory PlayerHistory => _historyRecords;

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
            _name = player.Name;
            Board.Load(player.Board, ShipFactory);
            PolygonBoard.Load(player.Polygon, ShipFactory);
            foreach (var item in player.History.History)
            {
                _historyRecords.AddRecord(item.GetRecord());
            }
        }

        public void RefreshUser()
        {
            GameSerializer.TrySave(PlayerMetadate.FromPlayer(this), UsersFolderPath + Name + XmlExtention);
        }

        protected abstract IShipFactory CreateShipFactory();

        private void RaiseCellCollectionChanged(CellChangedEventArgs args) => CellCollectionChanged(this, args);

        private void RaiseResetOccured() => ResetOcurred?.Invoke(this, null);

        private IShip CreateValidShip(Point point, bool isAlive) => _opponentShip.Create(point, isAlive);
    }
}