// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using TicTacToe;

namespace BattleShips.Ships
{
    public class Battleship : IShip
    {
        private int _deckCount = 4;
        private string _name = "Battleship";

        public Point Start { get; }

        public Point End { get; }

        public string Name => _name;

        public int Deck => _deckCount;

        public bool IsAlive { get; }

        public bool IsInsideShip(Point point) => throw new NotImplementedException();

        public bool TryDamageShip(Point shot) => throw new NotImplementedException();

        public void ApplyDamage(Point point, bool damaged) => throw new NotImplementedException();

        public bool Equals(IShip other) => throw new NotImplementedException();
    }
}