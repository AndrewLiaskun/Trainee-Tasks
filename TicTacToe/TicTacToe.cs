// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using TicTacToe.Abstract;
using TicTacToe.Enums;

namespace TicTacToe
{
    public class TicTacToe
    {
        private static string[] _field = { "   |   |   ", "---+---+---", "   |   |   ", "---+---+---", "   |   |   " };

        private GameState _currentState = GameState.Menu;

        private GameMenu _gameMenu;

        private IGraphicalInterface _graphicalInterface;

        private GameBoard _gameBoard;

        private Minimax _minimax;

        private Point _startPoint;

        private ComputerPlayer _ai;

        private Player _player;

        public TicTacToe()
        {
            _ai = new ComputerPlayer();
            _player = new Player();
            _graphicalInterface = new ConsoleGraphicalInterface();
            _gameBoard = new GameBoard();
            _startPoint = new Point(54, 0);
            _gameMenu = new GameMenu(_graphicalInterface, this);
        }

        public void SwitchState(GameState state)
        {
            _currentState = state;
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
                SetStartValues();
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

        public void SetNewPlayer()
        {
            _graphicalInterface.PrintText("Enter your name:");
            var name = _graphicalInterface.ReadText();
            _player = new Player(name);
        }

        private void SetStartValues()
        {

            _graphicalInterface.Clear();

            _gameBoard = new GameBoard();
            _minimax = new Minimax(_gameBoard);

            _graphicalInterface.Fill(_field);

            _graphicalInterface.PrintText(_gameBoard.GetGameScore(_player, _ai));

            _graphicalInterface.SetCursorPosition(_startPoint);

            var ai = _minimax.BestMove();
            _graphicalInterface.SetCursorPosition(new Point((ai.Point.X * 4) + 50, ai.Point.Y * 2));
            _graphicalInterface.PrintChar('O');
        }

        private void BackToMenu()
        {
            SwitchState(GameState.Menu);

            _gameMenu.Print();
        }

        private void GameControl(KeyboardHookEventArgs e)
        {
            var res = _gameBoard.CheckWinner();

            if (res != BoardCell.DefaultCharValue)
            {
                if (res == 'T')
                    Start();
                else
                {
                    if (res == 'X')
                    {
                        _player.SetWin();
                        Start();
                    }
                    else
                    {
                        _ai.SetWin();
                        Start();
                    }

                    _graphicalInterface.SetCursorPosition(new Point(50, 15));
                    _graphicalInterface.PrintText($"{res} is winner!");
                }
            }
            else
            {

                SetGamePoint(e);
                SetStep(e);
                _graphicalInterface.SetCursorPosition(_startPoint);
            }
        }

        private void SetGamePoint(KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) BackToMenu();
            if (e.KeyCode == Keys.Up && _startPoint.Y > 0) _startPoint.Y -= 2;
            if (e.KeyCode == Keys.Down && _startPoint.Y < 4) _startPoint.Y += 2;
            if (e.KeyCode == Keys.Left && _startPoint.X > 51) _startPoint.X -= 4;
            if (e.KeyCode == Keys.Right && _startPoint.X < 56) _startPoint.X += 4;
        }

        private void SetStep(KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Space && _gameBoard.IsEmptyCell((_startPoint.X - 50) / 4, _startPoint.Y / 2))
            {

                _gameBoard.SetCellValue((_startPoint.X - 50) / 4, _startPoint.Y / 2, 'X');

                _graphicalInterface.PrintChar('X');
                var ai = _minimax.BestMove();
                _graphicalInterface.SetCursorPosition(new Point((ai.Point.X * 4) + 50, ai.Point.Y * 2));
                _graphicalInterface.PrintChar('O');
            }
        }

        private void ConsoleGraphicalInterface_KeyPressed(object sender, KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) BackToMenu();
            if (_currentState == GameState.Game)
            {
                GameControl(e);
            }
            else
            {
                if (_currentState != GameState.LoadGame && _currentState != GameState.SaveGame)
                    _gameMenu.HandleKey(e.KeyCode);
            }
        }
    }
}