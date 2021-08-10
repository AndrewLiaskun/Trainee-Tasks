// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShipsWPF.Basic;

using TicTacToe;

namespace BattleShips.UI.ViewModels
{
    internal class PlayerBoardViewModel : BaseViewModel
    {
        private ObservableCollection<BoardCell> _playerBoard;

        public ObservableCollection<BoardCell> PlayerBoard
        {
            get => _playerBoard;
            set
            {
                if (_playerBoard == value)
                    return;

                GetPlayerBoard();
            }
        }

        private void GetPlayerBoard()
        {
        }
    }
}