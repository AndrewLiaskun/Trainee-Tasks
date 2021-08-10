// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.UI.Views.Controls;

using TicTacToe;

namespace BattleShips.UI.ViewModels
{
    public class CellContainer
    {
        private RelayCommand _addCommand;
        private BoardCell _newCell;

        public CellContainer()
        {
            ItemsSource = new ObservableCollection<BoardCell>();
        }

        public ObservableCollection<BoardCell> ItemsSource { get; }
    }
}