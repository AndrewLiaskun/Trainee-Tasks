// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using BattleShips.Abstract;

using TicTacToe;

namespace BattleShips.Models
{
    internal class BattleShipBoard : IBattleShipBoard
    {
        private static readonly string[] _boardTemplate = { "   1 2 3 4 5 6 7 8 9 10", "  ╔════════════════════╗", " ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║", "  ╚════════════════════╝" };

        private readonly IShell _shell;

        private BoardCell[] _boardCells;
        private List<IShip> _ships;

        private string[] _emptyBoard;

        public BattleShipBoard(IShell shell, Point position)
        {
            Position = position;

            _emptyBoard = GenerateBoard();
            _shell = shell;
            _ships = new List<IShip>();

            _boardCells = Enumerable.Range(0, 10).SelectMany(x =>
            {
                return new[] { new BoardCell(new Point(0, x), ' '), new BoardCell(new Point(1, x), ' '), new BoardCell(new Point(2, x), ' '),
                               new BoardCell(new Point(3, x), ' '), new BoardCell(new Point(4, x), ' '), new BoardCell(new Point(5, x), ' '),
                               new BoardCell(new Point(6, x), ' '), new BoardCell(new Point(7, x), ' '), new BoardCell(new Point(8, x), ' '),
                               new BoardCell(new Point(9, x), ' ')};
            }).ToArray();
        }

        public Point Position { get; }

        public IReadOnlyList<BoardCell> Cells => _boardCells;

        public IReadOnlyList<IShip> Ships => _ships;

        public int ShipsCount => _ships.Count;

        public char CheckWinner() => throw new NotImplementedException();

        public BoardCell GetCellValue(int x, int y) => Cells[(y * 10) + x];

        public bool IsEmptyCell(int x, int y) => GetCellValue(x, y).Value == BoardCell.DefaultCharValue;

        public void AddShip(IShip ship)
        {
            if (_ships.Contains(ship))
                return;

            _ships.Add(ship);

            // TODO: Update cells mark sells with ship values and so on
        }

        public void ProcessShot(Point point)
        {
            _ships.ForEach(z => z.TryDamageShip(point));
        }

        public void SetCellValue(int x, int y, char newValue)
        {
            var cell = GetCellValue(x, y);
            cell.Value = newValue;
        }

        public void Draw()
        {
            _shell.Fill(Position, _emptyBoard);

            // TODO: draw SHIPS !!!!!!!!!
        }

        private string[] GenerateBoard()
        {
            var lines = new List<string>();

            const int headerCount = 2;
            const int lineTemplateIndex = 2;

            lines.AddRange(_boardTemplate.Take(headerCount));

            int i = (int)(char)'A';
            int limit = (int)(char)'J';

            for (; i <= limit; i++)
            {
                lines.Add(((char)i) + _boardTemplate[lineTemplateIndex]);
            }

            lines.Add(_boardTemplate.Last());

            return lines.ToArray();
        }
    }
}