// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Enums;

namespace BattleShips.Abstract
{
    public class BoardShipsChangedEventArgs : EventArgs
    {
        private BoardShipsChangedEventArgs(BoardShipsChangeType type, IShip oldShip, IShip newShip)
        {
            ChangeType = type;
            OldShip = oldShip;
            NewShip = newShip;
        }

        public IShip OldShip { get; }

        public IShip NewShip { get; }

        public BoardShipsChangeType ChangeType { get; }

        public static BoardShipsChangedEventArgs CreateResetArgs() => new BoardShipsChangedEventArgs(BoardShipsChangeType.Reset, null, null);

        public static BoardShipsChangedEventArgs CreateAdded(IShip ship) => new BoardShipsChangedEventArgs(BoardShipsChangeType.Add, null, ship);

        public static BoardShipsChangedEventArgs CreateRemoved(IShip ship) => new BoardShipsChangedEventArgs(BoardShipsChangeType.Remove, ship, null);

        public static BoardShipsChangedEventArgs CreateReplaced(IShip oldShip, IShip newShip) => new BoardShipsChangedEventArgs(BoardShipsChangeType.Replace, oldShip, newShip);
    }
}