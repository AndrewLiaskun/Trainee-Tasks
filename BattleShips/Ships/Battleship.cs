// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;

namespace BattleShips.Ships
{
    public class Battleship : IShip
    {
        private int _deckCount = 4;
        private string _name = "Battleship";

        public string GetName => _name;

        public int GetDeck => _deckCount;
    }
}