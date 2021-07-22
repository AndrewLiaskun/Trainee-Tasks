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

        public void EaseModShoot(IPlayer shooter, IPlayer victim)
        {
            _generatedCells = shooter.PolygonBoard.Cells.Select(x => x).ToList();

            var targetCell = GetRandomCell();
            var indexX = targetCell.Point.X;
            var indexY = targetCell.Point.Y;

            if (victim.Board.GetCellValue(indexX, indexY).Value == GameConstants.Ship)
            {
                shooter.PolygonBoard.SetCellValue(indexX, indexY, GameConstants.Got);
                victim.Board.SetCellValue(indexX, indexY, GameConstants.Got);
            }
            else
            {
                shooter.PolygonBoard.SetCellValue(indexX, indexY, GameConstants.Miss);
                victim.Board.SetCellValue(indexX, indexY, GameConstants.Miss);
            }
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