// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class TicTacToe
    {
        private int[,] _score = new int[3, 3];
        private int? _winner;

        public void Draw()
        {
            string[] field = { "   |   |   ", "---+---+---", "   |   |   ", "---+---+---", "   |   |   " };

            for (int i = 0; i < 5; i++) Console.WriteLine(field[i]);
        }

        public void CheckWinner()
        {

            if (_winner == null)
            {
                for (int i = 0; i < 3; i++)
                {
                    if (EqualsRows(_score[i, 0], _score[i, 1], _score[i, 2]))    // Horizontal
                        _winner = _score[i, 0];
                }
                for (int i = 0; i < 3; i++)
                {
                    if (EqualsRows(_score[0, i], _score[1, i], _score[2, i]))    // Vertical
                        _winner = _score[0, i];
                }
                if (EqualsRows(_score[0, 0], _score[1, 1], _score[2, 2]))      // Diagonal
                    _winner = _score[0, 0];
                else if (EqualsRows(_score[2, 0], _score[1, 1], _score[0, 2]))
                    _winner = _score[2, 0];
            }
        }

        public void Move()
        {

            int x = 1, y = 0;
            int maxWidth = 8, maxHight = 4,
                minWidth = 1, minHight = 0;
            ConsoleKey controler;
            do
            {
                int hight = y / 2;
                int width = x / 4;
                Console.CursorLeft = x;
                Console.CursorTop = y;
                controler = Console.ReadKey(true).Key;
                if (controler == ConsoleKey.RightArrow && x < maxWidth) x += 4;
                if (controler == ConsoleKey.LeftArrow && x > minWidth) x -= 4;
                if (controler == ConsoleKey.DownArrow && y < maxHight) y += 2;
                if (controler == ConsoleKey.UpArrow && y > minHight) y -= 2;
                if (controler == ConsoleKey.Spacebar)
                    if (_score[hight, width] == 0)
                    {
                        _score[hight, width] = 1;
                        Console.Write("X");
                    }
            } while (controler != ConsoleKey.Escape);
        }

        private bool EqualsRows(int firstRow, int secondRows, int thirdRow)
        {
            return firstRow == secondRows && secondRows == thirdRow && firstRow != 0;
        }
    }
}