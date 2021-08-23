// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;

namespace BattleShips.Models
{
    public class GameHistory : IGameHistory
    {
        private List<HistoryRecord> _history;

        public GameHistory()
        {
            _history = new List<HistoryRecord>();
        }

        public IReadOnlyList<HistoryRecord> Histroy => _history;

        public int Count => _history.Count;

        public HistoryRecord this[int index] => _history[index];

        public void AddRecord(HistoryRecord record) => _history.Add(record);

        public IEnumerator<HistoryRecord> GetEnumerator() => _history.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}