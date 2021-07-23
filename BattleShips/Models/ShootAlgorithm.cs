// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Models
{

    public class ShootAlgorithm
    {
        private Random _generator = new Random();
        private List<BoardCell> _generatedCells;

        //TODO: Make it in while victim.Board.GetCellValue(indexX, indexY).Value == GameConstants.Ship
        public void EaseModShoot(IPlayer shooter, IPlayer victim)
        {
            _generatedCells = shooter.PolygonBoard.Cells.Select(x => x).ToList();

            var targetCell = GetRandomCell();
            var indexX = targetCell.Point.X;
            var indexY = targetCell.Point.Y;

            victim.Board.ProcessShot(targetCell.Point);
            var isDead = !isAlive(victim.Board.Ships, targetCell.Point);

            if (victim.Board.GetCellValue(indexX, indexY).Value == GameConstants.Ship)
            {
                shooter.MakeShot(targetCell.Point, false, isDead);
                victim.Board.SetCellValue(indexX, indexY, GameConstants.Got);
            }
            else
            {
                shooter.MakeShot(targetCell.Point, true, isDead);
                victim.Board.SetCellValue(indexX, indexY, GameConstants.Miss);
            }
        }

        private bool isAlive(IReadOnlyList<IShip> ships, Point point)
        {
            foreach (var item in ships)
            {
                if (item.Includes(point))
                    if (item.IsAlive)
                    {
                        return true;
                    }
            }
            return false;
        }

        private BoardCell GetRandomCell()
        {
            var newCells = new List<BoardCell>();
            foreach (var item in _generatedCells)
            {
                if (item.Value == GameConstants.Empty)
                {
                    newCells.Add(item);
                }
            }

            var index = _generator.Next(newCells.Count);

            return newCells[index];
        }
    }
}