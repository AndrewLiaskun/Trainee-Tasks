// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Models;

namespace BattleShips.Misc.Args
{
    public class HistoryRecordsChangedEventArgs : EventArgs
    {
        public IHistoryRecord HistoryRecord;

        private HistoryRecordsChangedEventArgs(IHistoryRecord historyRecord) => HistoryRecord = historyRecord;

        public static HistoryRecordsChangedEventArgs CreateAdded(IHistoryRecord historyRecord)
            => new HistoryRecordsChangedEventArgs(historyRecord);
    }
}