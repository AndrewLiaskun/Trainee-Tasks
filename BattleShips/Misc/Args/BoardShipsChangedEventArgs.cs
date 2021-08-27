// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Enums;

namespace BattleShips.Abstract
{
    public class BoardShipsChangedEventArgs : EventArgs
    {
        private BoardShipsChangedEventArgs(BoardShipsChangeType type, IReadOnlyList<IShip> oldShip, IShip newShip)
        {
            ChangeType = type;
            OldShip = oldShip;
            NewShip = newShip;
        }

        public IReadOnlyList<IShip> OldShip { get; }

        public IShip NewShip { get; }

        public BoardShipsChangeType ChangeType { get; }

        public static BoardShipsChangedEventArgs CreateResetArgs() => new BoardShipsChangedEventArgs(BoardShipsChangeType.Reset, null, null);

        public static BoardShipsChangedEventArgs CreateAdded(IShip ship) => new BoardShipsChangedEventArgs(BoardShipsChangeType.Add, null, ship);

        public static BoardShipsChangedEventArgs CreateRemoved(IReadOnlyList<IShip> ship) => new BoardShipsChangedEventArgs(BoardShipsChangeType.Remove, ship, null);

        public static BoardShipsChangedEventArgs CreateReplaced(IReadOnlyList<IShip> oldShip, IShip newShip) => new BoardShipsChangedEventArgs(BoardShipsChangeType.Replace, oldShip, newShip);
    }
}