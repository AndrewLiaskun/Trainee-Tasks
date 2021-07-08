// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Models
{
    internal class AiPlayer : IPlayer
    {
        private IBattleShipBoard _playerGameBoard;
        private IBattleShipBoard _aiGameBoard;

        public AiPlayer(IShell shell)
        {
            _playerGameBoard = new BattleShipBoard(shell, new Point());
            _aiGameBoard = new BattleShipBoard(shell, new Point(34, 0));
        }

        public IBattleShipBoard Board => _playerGameBoard;

        public void CreateShip(Point point, bool isEmpty)
            => _aiGameBoard.SetCellValue(point.X, point.Y, GameConstants.Ship);

        public void ShowBoards()
        {
            _playerGameBoard.Draw();
            _aiGameBoard.Draw();
        }

        public void MakeShot(Point point, bool isEmpty)
        {
            if (isEmpty)
                _playerGameBoard.SetCellValue(point.X, point.Y, GameConstants.Miss);
            else
                _playerGameBoard.SetCellValue(point.X, point.Y, GameConstants.Got);
        }
    }
}