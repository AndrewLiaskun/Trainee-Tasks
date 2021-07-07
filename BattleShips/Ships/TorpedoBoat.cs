// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Ships
{
    public class TorpedoBoat
    {
        private string _name = "Torpedo Boat";
        private int _deckCount = 1;

        public string GetName => _name;

        public int GetDeck => _deckCount;
    }
}