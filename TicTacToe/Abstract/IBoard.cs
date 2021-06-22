// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Abstract
{
    public interface IBoard
    {
        //event EventHandler<SectionChangedArgs> SectionChanged;

        IReadOnlyList<BoardCell> Cells { get; }

        void SetCellValue(int x, int y, char newValue);

        BoardCell GetCell(int x, int y);

        char CheckWinner();

        void GameScore();

        bool IsEmptyCell(int x, int y);
    }
}