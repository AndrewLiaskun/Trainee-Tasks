// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class SectionChangedArgs : EventArgs
    {
        private Point _point;

        public SectionChangedArgs(Point evtPoint)
        {
            _point = evtPoint;
        }

        public Point Point
        {
            get { return _point; }
        }
    }
}