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
        private List<IHistoryRecord> _history;

        public GameHistory()
        {
            _history = new List<IHistoryRecord>();
        }

        public IReadOnlyList<IHistoryRecord> Histroy => _history;

        public int Count => _history.Count;

        public IHistoryRecord this[int index] => _history[index];

        public void AddRecord(IHistoryRecord record) => _history.Add(record);

        public void Clear() => _history.Clear();

        public void RefreshHistory(IGameHistory history)
        {
            if (history == null)
                return;

            Clear();

            foreach (var item in history)
                AddRecord(item);
        }

        public IEnumerator<IHistoryRecord> GetEnumerator() => _history.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}