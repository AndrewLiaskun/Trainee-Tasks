// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace BattleShips.Misc
{
    public class ShipChangedEventArgs : EventArgs
    {
        public ShipChangedEventArgs(ShipState oldValue, ShipState newValue)
        {
            OldValue = oldValue;
            NewValue = newValue;
        }

        public ShipState OldValue { get; }

        public ShipState NewValue { get; }
    }
}