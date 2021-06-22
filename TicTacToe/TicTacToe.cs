// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using TicTacToe.Abstract;
using TicTacToe.Enums;
using TicTacToe.Utils;

namespace TicTacToe
{
    public class TicTacToe
    {
        private static string[] _field = { "   |   |   ", "---+---+---", "   |   |   ", "---+---+---", "   |   |   " };

        private GameState _currentState = GameState.Menu;
        private SaveFormat _currFormat = SaveFormat.XML;

        private GameMenu _gameMenu;

        private IGraphicalInterface _graphicalInterface = new ConsoleGraphicalInterface();

        private GameBoard _gameBoard = new GameBoard();

        private Minimax _minimax;

        private Point _startPoint = new Point(54, 0);

        public TicTacToe()
        {
            _gameMenu = new GameMenu(_graphicalInterface, this);
        }

        public void SwitchState(GameState state)
        {
            _currentState = state;
        }

        public SaveFormat GetFileFormat()
        {
            return _currFormat;
        }

        public void Resume()
        {
            _graphicalInterface.Fill(_field);
            for (int i = 0; i < _gameBoard.Cells.Count; i++)
            {
                _graphicalInterface.SetCursorPosition(new Point((_gameBoard.Cells[i].Point.X * 4) + 50, _gameBoard.Cells[i].Point.Y * 2));
                Console.WriteLine(_gameBoard.Cells[i].Value);
            }
        }

        public void Start()
        {
            _graphicalInterface.KeyPressed -= ConsoleGraphicalInterface_KeyPressed;
            if (_currentState == GameState.Game)
            {
                _gameBoard = new GameBoard();
                _minimax = new Minimax(_gameBoard);

                _graphicalInterface.Fill(_field);

                _graphicalInterface.SetCursorPosition(_startPoint);

                var ai = _minimax.BestMove();
                _graphicalInterface.SetCursorPosition(new Point((ai.Point.X * 4) + 50, ai.Point.Y * 2));
                _graphicalInterface.PrintChar('O');
            }
            else if (_currentState == GameState.LoadGame)
            {
                _currentState = GameState.Game;
                _minimax = new Minimax(_gameBoard);
                Resume();
            }
            else
            {
                _gameMenu.Print();
            }

            _graphicalInterface.KeyPressed += ConsoleGraphicalInterface_KeyPressed;

            _graphicalInterface.StartRunLoop();
        }

        public IBoard GetBoard()
        {
            return _gameBoard;
        }

        public void SetBoard(BoardCell[] board)
        {

            for (int i = 0; i < _gameBoard.Cells.Count; i++)
            {
                _gameBoard.SetCellValue(board[i].Point.X, board[i].Point.Y, board[i].Value);
            }
        }

        private void BackToMenu()
        {
            SwitchState(GameState.Menu);

            _gameMenu.Print();
        }

        private void ConsoleGraphicalInterface_KeyPressed(object sender, KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) BackToMenu();
            if (_currentState == GameState.Game)
            {
                var res = _gameBoard.CheckWinner();

                if (res != BoardCell.DefaultCharValue)
                {
                    _graphicalInterface.SetCursorPosition(new Point(50, 15));
                    _graphicalInterface.PrintText($"{res} is winner!");
                }
                else
                {

                    if (e.KeyCode == Keys.Escape) BackToMenu();
                    if (e.KeyCode == Keys.Up && _startPoint.Y > 0) _startPoint.Y -= 2;
                    if (e.KeyCode == Keys.Down && _startPoint.Y < 4) _startPoint.Y += 2;
                    if (e.KeyCode == Keys.Left && _startPoint.X > 51) _startPoint.X -= 4;
                    if (e.KeyCode == Keys.Right && _startPoint.X < 56) _startPoint.X += 4;
                    if (e.KeyCode == Keys.Space)
                    {
                        if (_gameBoard.IsEmptyCell((_startPoint.X - 50) / 4, _startPoint.Y / 2))
                        {

                            _gameBoard.SetCellValue((_startPoint.X - 50) / 4, _startPoint.Y / 2, 'X');

                            _graphicalInterface.PrintChar('X');
                            var ai = _minimax.BestMove();
                            _graphicalInterface.SetCursorPosition(new Point((ai.Point.X * 4) + 50, ai.Point.Y * 2));
                            _graphicalInterface.PrintChar('O');
                        }
                    }
                    _graphicalInterface.SetCursorPosition(_startPoint);
                }
            }
            else
            {

                _gameMenu.HandleKey(e.KeyCode);
            }
        }
    }
}