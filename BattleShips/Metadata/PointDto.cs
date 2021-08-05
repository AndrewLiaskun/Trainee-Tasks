// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using TicTacToe;

namespace BattleShips.Metadata
{
    [DataContract(Name = "point", Namespace = "http://schemas.datacontract.org/2004/07/BattleShips")]
    public class PointDto
    {
        [DataMember(Name = "x")]
        public int X { get; set; }

        [DataMember(Name = "y")]
        public int Y { get; set; }

        public static PointDto FromPoint(Point point)
        {
            var metadate = new PointDto();

            metadate.X = point.X;
            metadate.Y = point.Y;

            return metadate;
        }

        public Point GetPoint() => new Point(X, Y);
    }
}