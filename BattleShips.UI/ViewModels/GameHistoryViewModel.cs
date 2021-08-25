// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.Models;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.Commands;

using TicTacToe;

namespace BattleShips.UI.ViewModels
{
    public class GameHistoryViewModel : BaseViewModel, IModelProvider<IGameHistory>
    {

        private ObservableCollection<HistoryRecordViewModel> _history;

        public GameHistoryViewModel(IGameHistory gameHistory)
        {
            Model = gameHistory;

            _history = new ObservableCollection<HistoryRecordViewModel>(GetRecords(Model));
        }

        public IGameHistory Model { get; }

        public IEnumerable<HistoryRecordViewModel> History => _history;

        public void Add(HistoryRecordViewModel record)
        {
            _history.Add(record);
        }

        private static IEnumerable<HistoryRecordViewModel> GetRecords(IGameHistory records)
                            => records.Select(c => new HistoryRecordViewModel(c));
    }
}