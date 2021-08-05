// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;

using TicTacToe;

namespace BattleShips.Metadata
{
    [DataContract(Name = "battleship-board", Namespace = "http://schemas.datacontract.org/2004/07/BattleShips")]
    public class BoardDto
    {
        [DataMember(Name = "cells")]
        public BoardCellDto[] Board { get; set; }

        [DataMember(Name = "start-position")]
        public Point Position { get; set; }

        [DataMember(Name = "ships")]
        public List<ShipDto> Ships { get; set; }

        public static BoardDto FromBoard(IBattleShipBoard board)
        {
            BoardDto boardDto = new BoardDto();

            boardDto.Board = board.Cells.Select(BoardCellDto.FromCell).ToArray();
            boardDto.Position = board.Position;
            boardDto.Ships = CreateShips(board.Ships);

            return boardDto;
        }

        private static List<ShipDto> CreateShips(IEnumerable<IShip> ships) => ships.Select(ShipDto.FromShip).ToList();
    }
}