// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using BattleShips.Abstract;
using BattleShips.Abstract.Ships;
using BattleShips.Abstract.Visuals;
using BattleShips.Enums;
using BattleShips.Metadata;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Models
{
    internal class BattleShipBoard : IBattleShipBoard
    {
        private const int MaxIndex = GameConstants.BoardMeasures.MaxIndex;

        private BoardCell[] _boardCells;
        private List<IShip> _ships;
        private IVisualTable _gameTable;

        public BattleShipBoard(IVisualContext visualContext, Point position)
        {
            _ships = new List<IShip>();
            _boardCells = GenerateCells();

            Position = position;
            Shell = visualContext;

            _gameTable = visualContext.Create(Position);
        }

        public event EventHandler<BoardShipsChangedEventArgs> ShipsCollectionChanged = delegate { };

        public event EventHandler<ShipChangedEventArgs> ShipChanged = delegate { };

        public event EventHandler<BoardCell> CellChanged = delegate { };

        public Point Position { get; }

        public Point ZeroCellPosition => _gameTable.ZeroCell;

        public IReadOnlyList<BoardCell> Cells => _boardCells;

        public IReadOnlyList<IShip> Ships => _ships;

        public int AliveShips => Ships.Count(x => x.IsAlive);

        public Point CurrentPosition => _gameTable.CurrentPosition;

        protected IVisualContext Shell { get; }

        public char CheckWinner() => AliveShips == 0 ? GameConstants.Loser : GameConstants.Winner;

        public BoardCell GetCellValue(int x, int y) => Cells[(y * 10) + x];

        public BoardCell GetCellValue(Point point) => GetCellValue(point.X, point.Y);

        public void SetCellValue(Point point, char value)
            => SetCellValue(point.X, point.Y, value);

        public bool IsEmptyCell(Point point) => GetCellValue(point).Value == BoardCell.DefaultCharValue;

        public bool IsEmptyCell(int x, int y) => GetCellValue(x, y).Value == BoardCell.DefaultCharValue;

        public void AddShip(IShip ship)
        {
            if (ship is null)
                return;

            if (_ships.Contains(ship))
                return;

            _ships.Add(ship);

            ship.IsValid = ValidateShip(ship.Start, ship);

            ship.ShipChanged += OnShipChanged;

            RaiseShipsCollectionChanged(BoardShipsChangedEventArgs.CreateAdded(ship));
        }

        public void ProcessShot(Point point) => _ships.ForEach(z => z.TryDamageShip(point));

        public void SetCellValue(int x, int y, char newValue)
        {
            var cell = GetCellValue(x, y);

            cell.Value = newValue;

            RaiseCellChanged(cell);
        }

        public void Show()
        {
            _gameTable.Draw();

            _ships.ForEach(_gameTable.DrawShip);
            _gameTable.DrawBoardCells(this);
        }

        public void SelectCell(Point point) => _gameTable.DrawCursor(point);

        public void MoveShip(Point point, IShip ship, ShipDirection direction)
        {
            var current = Ships.LastOrDefault(x => x.Equals(ship));

            if (current == null)
                return;

            ship.IsValid = ValidateShip(point, ship);

            current.ChangeStartPoint(point);
            current.ChangeDirection(direction);

            Show();
        }

        public void SetCursor(Point position)
            => _gameTable.SetCursorPosition(position);

        public bool ValidateShip(Point point, IShip ship)
        {
            var rest = Ships.Except(new IShip[] { ship }).ToArray();

            if (rest.Length == 0)
                return true;

            return rest.All(x => x.IsValidDistance(point, ship));
        }

        public void ChangeOrAddShip(Point point, IShip ship)
        {
            var oldShips = _ships.Where(x => x.IsAtCriticalDistance(point)).ToArray();

            if (oldShips.Any())
            {
                foreach (var oldShip in oldShips)
                {
                    oldShip.ShipChanged -= OnShipChanged;
                    _ships.Remove(oldShip);
                }
            }

            _ships.Add(ship);
            ship.ShipChanged += OnShipChanged;

            if (!ship.IsAlive)
                PrintDeadShip(ship.Start, ship.End, ship.Direction);

            if (oldShips.Any())
                RaiseShipsCollectionChanged(BoardShipsChangedEventArgs.CreateReplaced(oldShips, ship));
            else
                RaiseShipsCollectionChanged(BoardShipsChangedEventArgs.CreateAdded(ship));
        }

        public IShip GetShipAtOrDefault(Point point) => Ships.FirstOrDefault(x => x.Includes(point));

        public void Load(BoardDto metadata, IShipFactory factory)
        {
            _ships.Clear();

            foreach (var cell in metadata.Board)
                SetCellValue(cell.Point.GetPoint(), cell.FirstChar);

            metadata.Ships.ForEach(x => AddShip(factory.CreateShip(x)));
        }

        public void Reset()
        {
            foreach (var item in Cells)
                item.Value = GameConstants.Empty;

            _ships.Clear();
            RaiseShipsCollectionChanged(BoardShipsChangedEventArgs.CreateResetArgs());
        }

        private static BoardCell[] GenerateCells()
        {
            return Enumerable.Range(0, 10).SelectMany(x =>
            {
                return new[] { new BoardCell(new Point(0, x), ' '), new BoardCell(new Point(1, x), ' '), new BoardCell(new Point(2, x), ' '),
                               new BoardCell(new Point(3, x), ' '), new BoardCell(new Point(4, x), ' '), new BoardCell(new Point(5, x), ' '),
                               new BoardCell(new Point(6, x), ' '), new BoardCell(new Point(7, x), ' '), new BoardCell(new Point(8, x), ' '),
                               new BoardCell(new Point(9, x), ' ')};
            }).ToArray();
        }

        private void RaiseShipsCollectionChanged(BoardShipsChangedEventArgs args) => ShipsCollectionChanged(this, args);

        private void RaiseCellChanged(BoardCell cell) => CellChanged(this, cell);

        private void RaiseShipChanged(ShipChangedEventArgs args) => ShipChanged(this, args);

        private void PrintDeadShip(Point start, Point end, ShipDirection direction)
        {
            var isHorizontal = direction == ShipDirection.Horizontal;
            int startIndex = isHorizontal ? start.X : start.Y;
            int endIndex = isHorizontal ? end.X : end.Y;

            for (int i = startIndex; i <= endIndex; i++)
            {
                var p = isHorizontal ? new Point(i, start.Y) : new Point(start.X, i);
                DrawKillZone(p);
            }
        }

        private void DrawKillZone(Point point)
        {
            for (int i = -1; i <= 1; ++i)
            {
                for (int j = -1; j <= 1; ++j)
                {
                    if (point.X + i > MaxIndex || point.X + i < 0)
                        continue;

                    var indexX = point.X + i;
                    var indexY = point.Y + j;

                    if (indexX < 0 || indexX > MaxIndex || indexY < 0 || indexY > MaxIndex)
                        continue;

                    if (GetCellValue(indexX, indexY).Value == GameConstants.Empty)
                        if (indexX != point.X || indexY != point.Y)
                            SetCellValue(indexX, indexY, GameConstants.Miss);
                }
            }
        }

        private void OnShipChanged(object sender, ShipChangedEventArgs e)
        {
            HandleShipChange(e);
            RaiseShipChanged(e);
            //var kk = 0;
            //if (e.NewValue.Health != e.OldValue.Health)
            //    kk = 1;
        }

        private void HandleShipChange(ShipChangedEventArgs e)
        {
            if (!e.NewValue.IsAlive.HasValue)
                return;

            var isAlive = e.NewValue.IsAlive.Value;

            if (isAlive)
            {
                UpdateShipCells(e.OldValue, false);
                UpdateShipCells(e.NewValue, true);
            }
            else
                PrintDeadShip(e.NewValue.Start, e.NewValue.End, e.NewValue.Direction);
        }

        private void UpdateShipCells(ShipState state, bool isNew)
        {
            var boundShip = _ships.LastOrDefault(x => x.ShipId == state.ShipId);
            var restShips = _ships.Except(new IShip[] { boundShip }).ToArray();

            for (int i = state.Start.X; i <= state.End.X; i++)
            {
                for (int j = state.Start.Y; j <= state.End.Y; j++)
                {
                    var point = new Point(i, j);

                    if (restShips.Any(x => x.Includes(point)))
                        break;

                    SetCellValue(point, isNew ? GameConstants.Ship : GameConstants.Empty);
                }
            }
        }
    }
}