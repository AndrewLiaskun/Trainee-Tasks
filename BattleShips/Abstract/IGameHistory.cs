﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Models;

namespace BattleShips.Abstract
{
    public interface IGameHistory : IReadOnlyList<HistoryRecord>
    {
        void AddRecord(HistoryRecord record);
    }
}