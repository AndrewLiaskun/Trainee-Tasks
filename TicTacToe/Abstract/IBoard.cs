// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Collections.Generic;

namespace TicTacToe.Abstract
{
    public interface IBoard
    {
        IReadOnlyList<BoardCell> Cells { get; }

        void SetCellValue(int x, int y, char newValue);

        BoardCell GetCellValue(int x, int y);

        char CheckWinner();

        bool IsEmptyCell(int x, int y);
    }
}