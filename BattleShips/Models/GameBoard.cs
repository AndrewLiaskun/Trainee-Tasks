// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using TicTacToe;
using TicTacToe.Abstract;

namespace BattleShips.Models
{
    internal class GameBoard : IBoard
    {
        private BoardCell[] _boardCells;

        public GameBoard()
        {
            _boardCells = Enumerable.Range(0, 10).SelectMany(x =>
            {
                return new BoardCell[] { new BoardCell(new Point(0, x), ' '), new BoardCell(new Point(1, x), ' '), new BoardCell(new Point(2, x), ' '),
                new BoardCell(new Point(3, x), ' '), new BoardCell(new Point(4, x), ' '), new BoardCell(new Point(5, x), ' '),
                new BoardCell(new Point(6, x), ' '), new BoardCell(new Point(7, x), ' '), new BoardCell(new Point(8, x), ' '),new BoardCell(new Point(9, x), ' ')};
            }).ToArray();
        }

        public IReadOnlyList<BoardCell> Cells => _boardCells;

        public char CheckWinner() => throw new NotImplementedException();

        public BoardCell GetCellValue(int x, int y) => Cells[(y * 10) + x];

        public bool IsEmptyCell(int x, int y) => GetCellValue(x, y).Value == BoardCell.DefaultCharValue;

        public void SetCellValue(int x, int y, char newValue)
        {
            var cell = GetCellValue(x, y);
            cell.Value = newValue;
        }
    }
}