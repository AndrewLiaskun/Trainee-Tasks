// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Windows.Input;

using BattleShips.Misc;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.Commands;

using TicTacToe;

namespace BattleShips.UI.ViewModels.Board
{
    public class BoardCellViewModel : BaseViewModel, IModelProvider<BoardCell>
    {
        public BoardCellViewModel(BoardCell boardCell)
        {
            Model = boardCell;
            ClickCommand = new RelayCommand(ClickExecute, () => Value == GameConstants.Empty);
        }

        public event EventHandler<Point> Clicked;

        public BoardCell Model { get; }

        public ICommand ClickCommand { get; }

        public char Value => Model.Value;

        private void ClickExecute()
        {
            Clicked?.Invoke(this, Model.Point);
        }
    }
}