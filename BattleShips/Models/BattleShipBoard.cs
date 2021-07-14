// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Models
{
    internal class BattleShipBoard : IBattleShipBoard
    {
        private static readonly string[] _boardTemplate = { "    1  2  3  4  5  6  7  8  9 10",
                                                            "   __ __ __ __ __ __ __ __ __ __",
                                                            " |  |  |  |  |  |  |  |  |  |  |",
                                                            "  |__|__|__|__|__|__|__|__|__|__|" };

        private readonly IShell _shell;

        private BoardCell[] _boardCells;
        private List<IShip> _ships;

        private string[] _emptyBoard;

        public BattleShipBoard(IShell shell, Point position)
        {
            Position = position;

            _emptyBoard = GenerateBoard();
            _shell = shell;
            _ships = new List<IShip>();

            _boardCells = Enumerable.Range(0, 10).SelectMany(x =>
            {
                return new[] { new BoardCell(new Point(0, x), ' '), new BoardCell(new Point(1, x), ' '), new BoardCell(new Point(2, x), ' '),
                               new BoardCell(new Point(3, x), ' '), new BoardCell(new Point(4, x), ' '), new BoardCell(new Point(5, x), ' '),
                               new BoardCell(new Point(6, x), ' '), new BoardCell(new Point(7, x), ' '), new BoardCell(new Point(8, x), ' '),
                               new BoardCell(new Point(9, x), ' ')};
            }).ToArray();
        }

        public Point Position { get; }

        public IReadOnlyList<BoardCell> Cells => _boardCells;

        public IReadOnlyList<IShip> Ships => _ships;

        public int ShipsCount => _ships.Count;

        public char CheckWinner() => throw new NotImplementedException();

        public BoardCell GetCellValue(int x, int y) => Cells[(y * 10) + x];

        public bool IsEmptyCell(int x, int y) => GetCellValue(x, y).Value == BoardCell.DefaultCharValue;

        public void AddShip(IShip ship)
        {
            if (_ships.Contains(ship))
                return;

            _ships.Add(ship);

            // TODO: Update cells mark sells with ship values and so on
        }

        public void ProcessShot(Point point)
        {
            _ships.ForEach(z => z.TryDamageShip(point));
        }

        public void SetCellValue(int x, int y, char newValue)
        {
            var position = (y * 10) + x;
            _boardCells[position].Value = newValue;
        }

        public void Draw()
        {
            _shell.SetForegroundColor(ShellColor.Yellow);
            _shell.Fill(Position, _emptyBoard);
            _shell.ResetColor();

            _ships.ForEach(DrawShip);
        }

        public void DrawSelectedCell(Point point) => DrawEmptyCell(point);

        public void MoveShip(Point point, IShip ship, Direction direction)
        {
            var current = Ships.FirstOrDefault(x => x.Equals(ship));

            if (current == null) return;

            current.ChangeStartPoint(point);
            current.ChangeDirection(direction);

            Draw();
        }

        public void FillBoardCell(IShip ship)
        {
            int xSize = (ship.Start.X - GameConstants.PlayerBoard.MinWidth) / GameConstants.Step.Right ==
                (ship.End.X - GameConstants.PlayerBoard.MinWidth) / GameConstants.Step.Right ?
                ((ship.Start.X - GameConstants.PlayerBoard.MinWidth) / GameConstants.Step.Right)
                : (ship.Start.X - GameConstants.PlayerBoard.MinWidth) / GameConstants.Step.Right;

            int ySize = (ship.Start.Y - GameConstants.PlayerBoard.MinHeight) / GameConstants.Step.Down
                == (ship.End.Y - GameConstants.PlayerBoard.MinHeight) / GameConstants.Step.Down ?
                ((ship.Start.Y - GameConstants.PlayerBoard.MinHeight) / GameConstants.Step.Down)
                : (ship.Start.Y - GameConstants.PlayerBoard.MinHeight) / GameConstants.Step.Down;

            var shipEndX = ship.End.X / GameConstants.Step.Right;
            var shipEndY = ship.End.Y / GameConstants.Step.Down;

            for (int i = xSize; i < shipEndX; i++)
            {
                for (int j = ySize; j < shipEndY; j++)
                {
                    if (!(Math.Abs(shipEndX - i) > ship.Deck || Math.Abs(shipEndY - j) > ship.Deck))
                        SetCellValue(i, j, GameConstants.Ship);
                }
            }
        }

        private bool IsOkayToPlaceShip(IShip ship)
        {
            int xSize = (ship.Start.X - GameConstants.PlayerBoard.MinWidth) / GameConstants.Step.Right ==
            (ship.End.X - GameConstants.PlayerBoard.MinWidth) / GameConstants.Step.Right ?
            ((ship.Start.X - GameConstants.PlayerBoard.MinWidth) / GameConstants.Step.Right)
            : (ship.Start.X - GameConstants.PlayerBoard.MinWidth) / GameConstants.Step.Right;

            int ySize = (ship.Start.Y - GameConstants.PlayerBoard.MinHeight) / GameConstants.Step.Down
                == (ship.End.Y - GameConstants.PlayerBoard.MinHeight) / GameConstants.Step.Down ?
                ((ship.Start.Y - GameConstants.PlayerBoard.MinHeight) / GameConstants.Step.Down)
                : (ship.Start.Y - GameConstants.PlayerBoard.MinHeight) / GameConstants.Step.Down;

            var shipEndX = ship.End.X / GameConstants.Step.Right;
            var shipEndY = ship.End.Y / GameConstants.Step.Down;

            for (int i = xSize; i < shipEndX; i++)
            {
                for (int j = ySize; j < shipEndY; j++)
                {
                    if (!(Math.Abs(shipEndX - i) > ship.Deck || Math.Abs(shipEndY - j) > ship.Deck))
                    {

                        if (GetCellValue(i, j).Value == GameConstants.Ship)
                            return false;
                        //var position = (j * 10) + i;

                        //var kekYPlus = position + 10;
                        //var kekYMinus = position - 10;

                        //var kekXMinus = position - 1;
                        //var kekXPlus = position + 1;

                        //var kekDiagUpLeft = position - 11;
                        //var kekDiagDownLeft = position + 9;

                        //var kekDiagDownRight = position + 11;
                        //var kekDiagUpRight = position - 9;

                        //if (_boardCells[position].Value == GameConstants.Ship || _boardCells[kekYPlus].Value == GameConstants.Ship ||
                        //    _boardCells[kekYMinus].Value == GameConstants.Ship || _boardCells[kekXMinus].Value == GameConstants.Ship ||
                        //    _boardCells[kekXPlus].Value == GameConstants.Ship || _boardCells[kekDiagUpLeft].Value == GameConstants.Ship ||
                        //    _boardCells[kekDiagDownLeft].Value == GameConstants.Ship || _boardCells[kekDiagDownRight].Value == GameConstants.Ship ||
                        //    _boardCells[kekDiagUpRight].Value == GameConstants.Ship)
                        //{
                        //    return false;
                        //}
                    }
                }
            }

            return true;
        }

        private void DrawEmptyCell(Point point)
        {
            _shell.SetBackgroundColor(ShellColor.DarkYellow);

            for (int i = 0; i < 2; ++i)
            {
                if (i == 0)
                    _shell.PrintText("  ", new Point(point.X, point.Y));
                else
                {
                    _shell.PrintText("__", new Point(point.X, point.Y + 1));
                }
            }
            _shell.ResetColor();
        }

        private void DrawShip(IShip ship)
        {
            if (!IsOkayToPlaceShip(ship))
                _shell.SetForegroundColor(ShellColor.Red);
            else
                _shell.SetForegroundColor(ShellColor.Blue);

            Point p = ship.Start;
            for (int i = 0; i < ship.Deck; i++)
            {
                _shell.PrintText("__", new Point(p.X, p.Y - 1));
                for (int k = 0; k < 2; ++k)
                {
                    if (k == 0)
                        _shell.PrintText("|  |", new Point(p.X - 1, p.Y));
                    else
                        _shell.PrintText("|__|", new Point(p.X - 1, p.Y + 1));
                }
                if (ship.Direction == Direction.Horizontal)
                    p.X += GameConstants.Step.Right;
                else
                    p.Y += GameConstants.Step.Down;
            }
            _shell.ResetColor();
        }

        private string[] GenerateBoard()
        {
            var lines = new List<string>();

            const int headerCount = 2;
            const int lineTemplateIndex = 2;

            lines.AddRange(_boardTemplate.Take(headerCount));

            int i = (int)(char)'A';
            int limit = (int)(char)'J';

            for (; i <= limit; i++)
            {
                lines.Add(((char)i) + _boardTemplate[lineTemplateIndex]);
                lines.Add(_boardTemplate[3]);
            }

            return lines.ToArray();
        }
    }
}