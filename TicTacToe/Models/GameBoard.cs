// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using TicTacToe.Abstract;

namespace TicTacToe
{
    public class GameBoard : IBoard
    {
        private BoardCell[] _boardCells;

        public GameBoard()
        {
            _boardCells = Enumerable.Range(0, 3).SelectMany(x =>
            {
                return new BoardCell[] { new BoardCell(new Point(0, x), ' '), new BoardCell(new Point(1, x), ' '), new BoardCell(new Point(2, x), ' ') };
            }).ToArray();
        }

        public IReadOnlyList<BoardCell> Cells => _boardCells;

        public void SetCellValue(int x, int y, char newValue)
        {
            var cell = GetCellValue(x, y);
            cell.Value = newValue;
        }

        public BoardCell GetCellValue(int x, int y) => Cells[(y * 3) + x];

        public bool IsEmptyCell(int x, int y) => GetCellValue(x, y).Value == BoardCell.DefaultCharValue;

        public char CheckWinner()
        {

            for (int i = 0; i < 3; i++)
                if (EqualsRows(_boardCells[0 + i].Value, _boardCells[3 + i].Value, _boardCells[6 + i].Value))     // Vertical
                    return _boardCells[0 + i].Value;

            for (int i = 0; i < 3; i++)
                if (EqualsRows(_boardCells[i * 3].Value, _boardCells[(i * 3) + 1].Value, _boardCells[(i * 3) + 2].Value))    // Horizontal
                    return _boardCells[i * 3].Value;

            if (EqualsRows(_boardCells[0].Value, _boardCells[4].Value, _boardCells[8].Value) ||
                EqualsRows(_boardCells[2].Value, _boardCells[4].Value, _boardCells[6].Value))    // Diagonal
                return _boardCells[4].Value;

            if (_boardCells.Count(x => x.Value == BoardCell.DefaultCharValue) == 0)
                return 'T';

            return BoardCell.DefaultCharValue;
        }

        public string GetGameScore(IPlayer firstPlayer, IPlayer secondPlayer) => $"{firstPlayer.Name}: {firstPlayer.Score}\n{secondPlayer.Name}: {secondPlayer.Score}";

        private bool EqualsRows(char first, char second, char third) => first == second && second == third && first != BoardCell.DefaultCharValue;
    }
}