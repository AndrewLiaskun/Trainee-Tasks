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
    [DataContract(Name = "board-cell", Namespace = "http://schemas.datacontract.org/2004/07/BattleShips")]
    public class BoardCellDto
    {
        public char FirstChar => Value[0];

        [DataMember(Name = "point")]
        public PointDto Point { get; set; }

        [DataMember(Name = "value")]
        public string Value { get; set; }

        public static BoardCellDto FromCell(BoardCell cell)
        {
            var metedata = new BoardCellDto();

            metedata.Point = PointDto.FromPoint(cell.Point);
            metedata.Value = cell.Value.ToString();

            return metedata;
        }

        public BoardCell GetCell() => new BoardCell(Point.GetPoint(), FirstChar);
    }
}