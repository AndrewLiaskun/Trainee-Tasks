// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using TicTacToe.Abstract;

namespace TicTacToe
{
    public class Minimax
    {
        private IBoard _board;

        public Minimax(IBoard board)
        {
            _board = board;
        }

        public BoardCell BestMove()
        {
            var bestScore = float.NegativeInfinity;
            int moveX = 0;
            int moveY = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var cell = (j * 3) + i;
                    if (_board.Cells[cell].Value == BoardCell.DefaultCharValue)
                    {

                        _board.SetCellValue(i, j, BoardCell.ZeroChar);
                        var score = Minmax(0, false);
                        _board.SetCellValue(i, j, BoardCell.DefaultCharValue);
                        if (score > bestScore)
                        {
                            bestScore = score;
                            moveX = i;
                            moveY = j;
                        }
                    }
                }
            }

            _board.SetCellValue(moveX, moveY, BoardCell.ZeroChar);
            return _board.GetCellValue(moveX, moveY);
        }

        public float Minmax(int depth, bool isMaximizing)
        {
            var res = _board.CheckWinner();
            if (res != BoardCell.DefaultCharValue)
            {
                if (res == BoardCell.CrossChar)
                    return -10;
                if (res == BoardCell.ZeroChar)
                    return 10;
                if (res == BoardCell.TieChar)
                    return 0;
            }
            var playerChar = isMaximizing ? BoardCell.ZeroChar : BoardCell.CrossChar;
            Func<float, float, float> miniMax = isMaximizing ? (Func<float, float, float>)((x, y) => Math.Max(x, y)) : (x, y) => Math.Min(x, y);

            var bestScore = isMaximizing ? float.NegativeInfinity : float.PositiveInfinity;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {

                    if (_board.GetCellValue(i, j).Value == BoardCell.DefaultCharValue)
                    {

                        _board.SetCellValue(i, j, playerChar);

                        var score = Minmax(depth + 1, !isMaximizing);
                        _board.SetCellValue(i, j, BoardCell.DefaultCharValue);
                        bestScore = miniMax(score, bestScore);
                    }
                }
            }
            return bestScore;
        }
    }
}