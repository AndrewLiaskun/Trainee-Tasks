// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.Commands;

using TicTacToe;

namespace BattleShips.UI.ViewModels
{
    public class GameHistoryViewModel : BaseViewModel, IModelProvider<IGameHistory>
    {
        public GameHistoryViewModel(IGameHistory gameHistory)
        {
            Model = gameHistory;
        }

        public IGameHistory Model { get; }
    }
}