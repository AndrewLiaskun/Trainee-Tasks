// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Enums;

namespace BattleShips.Abstract
{
    public class BoardShipsChangedEventArgs : EventArgs
    {
        private BoardShipsChangedEventArgs(BoardShipsChangeType type, IReadOnlyList<IShip> oldShips, IReadOnlyList<IShip> newShips)
        {
            ChangeType = type;
            OldShips = oldShips;
            NewShips = newShips;
        }

        public IReadOnlyList<IShip> OldShips { get; }

        public IReadOnlyList<IShip> NewShips { get; }

        public BoardShipsChangeType ChangeType { get; }

        public static BoardShipsChangedEventArgs CreateResetArgs() => new BoardShipsChangedEventArgs(BoardShipsChangeType.Reset, null, null);

        public static BoardShipsChangedEventArgs CreateAdded(IReadOnlyList<IShip> ship)
            => new BoardShipsChangedEventArgs(BoardShipsChangeType.Add, null, ship);

        public static BoardShipsChangedEventArgs CreateRemoved(IReadOnlyList<IShip> ship)
            => new BoardShipsChangedEventArgs(BoardShipsChangeType.Remove, ship, null);

        public static BoardShipsChangedEventArgs CreateReplaced(IReadOnlyList<IShip> oldShip, IReadOnlyList<IShip> newShip)
            => new BoardShipsChangedEventArgs(BoardShipsChangeType.Replace, oldShip, newShip);
    }
}