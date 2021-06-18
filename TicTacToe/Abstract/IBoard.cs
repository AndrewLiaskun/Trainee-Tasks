// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe.Abstract
{
    public interface IBoard
    {
        event EventHandler SectionChanged;

        void SetSectionValue(byte positionX, byte positionY, char newValue);
    }
}