﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Enums
{
    public enum DrawCellType
    {
        // enum for ship placement

        Empty,
        Ship,
        HitShip,
        Miss,
        BadPut,
    };
}