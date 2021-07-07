// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Misc;

using TicTacToe;
using TicTacToe.Abstract;

namespace BattleShips.Models
{
    internal class Player : IPlayer
    {
        private IBoard _playerGameBoard;
        private IBoard _aiGameBoard;

        public Player()
        {
            _playerGameBoard = new GameBoard();
            _aiGameBoard = new GameBoard();
        }

        public void CreateAShip(Point point, bool isEmpty) => _playerGameBoard.SetCellValue(point.X, point.Y, DefaultCharContainer.Ship);

        public void MakeAShoot(Point point, bool isEmpty)
        {
            if (isEmpty)
            {
                _aiGameBoard.SetCellValue(point.X, point.Y, DefaultCharContainer.Miss);
            }
            else
            {
                _aiGameBoard.SetCellValue(point.X, point.Y, DefaultCharContainer.Got);
            }
        }
    }
}