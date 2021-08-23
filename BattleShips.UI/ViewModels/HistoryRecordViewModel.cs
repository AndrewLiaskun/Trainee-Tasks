// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Enums;
using BattleShips.Models;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;

using TicTacToe;

namespace BattleShips.UI.ViewModels
{
    public class HistoryRecordViewModel : BaseViewModel, IModelProvider<IHistoryRecord>
    {
        public HistoryRecordViewModel(IHistoryRecord record)
        {
            Model = record;
        }

        public IHistoryRecord Model { get; }

        public Point Point => Model.Point;

        public bool IsShipCell => Model.IsShipCell;

        public PlayerType Shooter => Model.Shooter;
    }
}