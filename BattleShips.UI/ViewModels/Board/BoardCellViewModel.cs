// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;

using TicTacToe;

namespace BattleShips.UI.ViewModels.Board
{
    public class BoardCellViewModel : BaseViewModel, IModelProvider<BoardCell>
    {
        public BoardCellViewModel(BoardCell boardCell)
        {
            Model = boardCell;
        }

        public BoardCell Model { get; }

        public char Value => Model.Value;
    }
}