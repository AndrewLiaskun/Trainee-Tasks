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

        public static BoardShipsChangedEventArgs CreateAdded(IReadOnlyList<IShip> ships)
            => new BoardShipsChangedEventArgs(BoardShipsChangeType.Add, null, ships);

        public static BoardShipsChangedEventArgs CreateRemoved(IReadOnlyList<IShip> ships)
            => new BoardShipsChangedEventArgs(BoardShipsChangeType.Remove, ships, null);

        public static BoardShipsChangedEventArgs CreateReplaced(IReadOnlyList<IShip> oldShips, IReadOnlyList<IShip> newShips)
            => new BoardShipsChangedEventArgs(BoardShipsChangeType.Replace, oldShips, newShips);
    }
}