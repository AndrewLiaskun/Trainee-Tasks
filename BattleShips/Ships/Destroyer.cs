// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;

namespace BattleShips.Ships
{
    public class Destroyer : IShip
    {
        private string _name = "Destroyer";
        private int _deckCount = 2;

        public string GetName => _name;

        public int GetDeck => _deckCount;
    }
}