// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using BattleShips.Abstract;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Models
{
    public class ShootAlgorithm
    {
        private Random _generator = new Random();

        private List<BoardCell> _availableCells;

        public void MakeShoot(IPlayer shooter, IPlayer victim)
        {
            _availableCells = shooter.PolygonBoard.Cells.Where(x => x.Value == GameConstants.Empty).Select(x => x).ToList();

            Shoot(shooter, victim);
        }

        private void Shoot(IPlayer shooter, IPlayer victim)
        {
            if (_availableCells.Count == 0)
                return;

            BoardCell targetCell;

            do
            {
                targetCell = GetRandomCell();
                victim.Board.ProcessShot(targetCell.Point);

                var damagedShip = victim.Board.GetShipAtOrDefault(targetCell.Point);
                var isAlive = damagedShip?.IsAlive ?? false;

                if (victim.Board.GetCellValue(targetCell.Point).Value == GameConstants.Ship)
                {
                    shooter.MakeShot(targetCell.Point, false, isAlive);
                    victim.Board.SetCellValue(targetCell.Point, GameConstants.Got);
                }
                else
                {
                    shooter.MakeShot(targetCell.Point, true, isAlive);
                    victim.Board.SetCellValue(targetCell.Point, GameConstants.Miss);
                }

                _availableCells.Remove(targetCell);
            }
            while (_availableCells.Count > 0 && victim.Board.GetCellValue(targetCell.Point).Value == GameConstants.Ship);
        }

        private BoardCell GetRandomCell()
        {
            var index = _generator.Next(_availableCells.Count);
            return _availableCells[index];
        }
    }
}