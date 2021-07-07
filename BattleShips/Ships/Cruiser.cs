// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;

namespace BattleShips.Ships
{
    public class Cruiser : IShip
    {
        private string _name = "Cruiser";
        private int _deckCount = 3;

        public string GetName => _name;

        public int GetDeck => _deckCount;
    }
}