// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Abstract;

namespace TicTacToe
{
    [Serializable]
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

        public event EventHandler<SectionChangedArgs> SectionChanged = delegate { };

        public IReadOnlyList<BoardCell> Cells => _boardCells;

        public void GameScore()
        {
        }

        public void SetCellValue(int x, int y, char newValue)
        {
            var cell = GetCell(x, y);
            cell.Value = newValue;
        }

        public BoardCell GetCell(int x, int y) => Cells[(y * 3) + x];

        public bool IsEmptyCell(int x, int y)
        {
            if (GetCell(x, y).Value == BoardCell.DefaultCharValue) return true;
            return false;
        }

        public char CheckWinner()
        {

            for (int i = 0; i < 3; i++)
            {

                if (EqualsRows(_boardCells[0 + i].Value, _boardCells[3 + i].Value, _boardCells[6 + i].Value))     // Vertical
                    return _boardCells[0 + i].Value;
            }
            for (int i = 0; i < 3; i++)
            {
                if (EqualsRows(_boardCells[i * 3].Value, _boardCells[(i * 3) + 1].Value, _boardCells[(i * 3) + 2].Value))    // Horizontal
                    return _boardCells[i * 3].Value;
            }

            if (EqualsRows(_boardCells[0].Value, _boardCells[4].Value, _boardCells[8].Value) || EqualsRows(_boardCells[2].Value, _boardCells[4].Value, _boardCells[6].Value))    // Diagonal
                return _boardCells[4].Value;

            return BoardCell.DefaultCharValue;
        }

        public bool EqualsRows(char first, char second, char third)
        {
            return first == second && second == third && first != BoardCell.DefaultCharValue;
        }
    }
}