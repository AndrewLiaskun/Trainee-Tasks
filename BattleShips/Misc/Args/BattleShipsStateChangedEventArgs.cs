// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using BattleShips.Enums;

namespace BattleShips.Misc.Args
{
    public class BattleShipsStateChangedEventArgs : EventArgs
    {
        public BattleShipsStateChangedEventArgs(BattleShipsState oldState, BattleShipsState newState)
        {
            OldState = oldState;
            NewState = newState;
        }

        public BattleShipsState OldState { get; }

        public BattleShipsState NewState { get; }
    }
}