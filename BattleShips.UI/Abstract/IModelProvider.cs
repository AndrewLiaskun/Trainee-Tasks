﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.UI.Abstract
{
    public interface IModelProvider<T>
    {
        T Model { get; }
    }
}