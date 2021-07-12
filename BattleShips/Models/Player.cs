// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Ships;

using TicTacToe;

namespace BattleShips.Models
{
    internal class Player : IPlayer
    {
        private IBattleShipBoard _playerGameBoard;
        private IBattleShipBoard _aiGameBoard;

        public Player(IShell shell)
        {
            _playerGameBoard = new BattleShipBoard(shell, new Point());
            _aiGameBoard = new BattleShipBoard(shell, new Point(44, 0));
        }

        public IBattleShipBoard Board => _playerGameBoard;

        public IShip CreateShip(Point point)
        {
            var ship = new Battleship(point);
            _playerGameBoard.AddShip(ship);

            return ship;
        }

        public void ShowBoards()
        {
            _playerGameBoard.Draw();
            _aiGameBoard.Draw();
        }

        public void MakeShot(Point point, bool isEmpty)
        {
            if (isEmpty)
            {
                _aiGameBoard.SetCellValue(point.X, point.Y, GameConstants.Miss);
            }
            else
            {
                _aiGameBoard.SetCellValue(point.X, point.Y, GameConstants.Got);
            }
        }

        public void MakeMove(Point point) => _aiGameBoard.DrawSelectedCell(point);
    }
}