// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Models
{
    internal class BattleShipBoard : IBattleShipBoard
    {
        private readonly IShell _shell;

        private BoardCell[] _boardCells;
        private List<IShip> _ships;
        private GameTable _gameTable;

        public BattleShipBoard(IShell shell, Point position)
        {
            Position = position;
            _shell = shell;
            _ships = new List<IShip>();

            _boardCells = GenerateCells();

            _gameTable = new GameTable(position, shell, this);
        }

        public Point Position { get; }

        public Point ZeroCellPosition => _gameTable.ZeroCell;

        public IReadOnlyList<BoardCell> Cells => _boardCells;

        public IReadOnlyList<IShip> Ships => _ships;

        public int ShipsCount => _ships.Count;

        public char CheckWinner() => throw new NotImplementedException();

        public BoardCell GetCellValue(int x, int y) => Cells[(y * 10) + x];

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
            // TODO: Update cells mark sells with ship values and so on
        }

        public void ProcessShot(Point point) => _ships.ForEach(z => z.TryDamageShip(point));

        public void SetCellValue(int x, int y, char newValue)
        {
            var position = (y * 10) + x;
            _boardCells[position].Value = newValue;
        }

        public void Draw()
        {
            _shell.SetForegroundColor(ShellColor.Yellow);
            _gameTable.Draw();
            _shell.ResetColor();

            _ships.ForEach(_gameTable.DrawShip);
        }

        public void DrawSelectedCell(Point point) => _gameTable.DrawCursor(point);

        public void MoveShip(Point point, IShip ship, ShipDirection direction)
        {
            var current = Ships.FirstOrDefault(x => x.Equals(ship));

            if (current == null)
                return;

            ship.IsValid = ValidateShip(point, ship);

            current.ChangeStartPoint(point);
            current.ChangeDirection(direction);

            Draw();
        }

        public void SetCursor(Point position)
            => _gameTable.SetCursorPosition(position);

        public bool ValidateShip(Point point, IShip ship)
        {
            var rest = Ships.Except(new IShip[] { ship }).ToArray();

            if (rest.Length == 0)
                return true;

            return !rest.Any(x => x.IntersectsWith(point, ship));
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

        private void OnShipChanged(object sender, ShipChangedEventArgs e)
        {
            if (!e.NewValue.IsFrozen)
                return;

            for (int i = e.OldValue.Start.X; i <= e.OldValue.End.X; i++)
            {
                for (int j = e.OldValue.Start.Y; j <= e.OldValue.End.Y; j++)
                {
                    SetCellValue(i, j, GameConstants.Empty);
                }
            }
            for (int i = e.NewValue.Start.X; i <= e.NewValue.End.X; i++)
            {
                for (int j = e.NewValue.Start.Y; j <= e.NewValue.End.Y; j++)
                {
                    SetCellValue(i, j, GameConstants.Ship);
                }
            }
        }
    }
}