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

            _gameTable = new GameTable(position, shell);
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

        public void MoveShip(Point point, IShip ship, Direction direction)
        {
            var current = Ships.FirstOrDefault(x => x.Equals(ship));

            if (current == null)
                return;

            current.ChangeStartPoint(point);
            current.ChangeDirection(direction);

            Draw();
        }

        public void SetCursor(Point position)
            => _gameTable.SetCursorPosition(position);

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
        }
    }
}