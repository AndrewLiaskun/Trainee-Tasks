// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Misc;

using TicTacToe;
using TicTacToe.Abstract;

namespace BattleShips.Models
{
    internal class AiPlayer : IPlayer
    {
        private IBoard _playerGameBoard;
        private IBoard _aiGameBoard;

        public AiPlayer()
        {
            _playerGameBoard = new GameBoard();
            _aiGameBoard = new GameBoard();
        }

        public void CreateAShip(Point point, bool isEmpty) => _aiGameBoard.SetCellValue(point.X, point.Y, DefaultCharContainer.Ship);

        public void MakeAShoot(Point point, bool isEmpty)
        {
            if (isEmpty)
            {
                _playerGameBoard.SetCellValue(point.X, point.Y, DefaultCharContainer.Miss);
            }
            else
            {
                _playerGameBoard.SetCellValue(point.X, point.Y, DefaultCharContainer.Got);
            }
        }
    }
}