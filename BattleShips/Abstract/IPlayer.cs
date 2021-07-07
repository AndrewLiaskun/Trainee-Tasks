// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TicTacToe;

namespace BattleShips.Abstract
{
    public interface IPlayer
    {
        void MakeAShoot(Point point, bool isEmpty);

        void CreateAShip(Point point, bool isEmpty);
    }
}