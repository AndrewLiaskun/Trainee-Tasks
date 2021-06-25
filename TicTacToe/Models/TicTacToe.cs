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

        private IGameMenu _gameMenu;

        private IGraphicalInterface _graphicalInterface;

        private IBoard _gameBoard;

        private Minimax _minimax;

        private Point _startPoint;
        private GameInfo _gameInfo;

        public TicTacToe()
        {
            _graphicalInterface = new ConsoleGraphicalInterface();
            _gameBoard = new GameBoard();
            _startPoint = new Point(54, 0);
            _gameMenu = new GameMenu(_graphicalInterface, this);
            _gameInfo = new GameInfo();
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
                _graphicalInterface.PrintChar(_gameBoard.Cells[i].Value, new Point((_gameBoard.Cells[i].Point.X * 4) + 50, _gameBoard.Cells[i].Point.Y * 2));
            }
        }

        public IBoard GetBoard() => _gameBoard;

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

        public void SetBoard(BoardCell[] board)
        {

            for (int i = 0; i < _gameBoard.Cells.Count; i++)
            {
                _gameBoard.SetCellValue(board[i].Point.X, board[i].Point.Y, board[i].Value);
            }
        }

        public void SaveGame()
        {
            _graphicalInterface.PrintText("Enter path: ( Path be like: C:\\folder\\file.json(xml) )");
            var path = _graphicalInterface.ReadText();

            _gameInfo.Board = (BoardCell[])_gameBoard.Cells;

            if (Serializer.TrySave(_gameInfo, path))
                _graphicalInterface.PrintText("Successful conservation.\nPress \'Esc\'");
            else _graphicalInterface.PrintText("Something wrong. Try again.\nPress \'Esc\'");
        }

        public void LoadGame()
        {
            _graphicalInterface.PrintText("Enter path: ( Path be like: C:\\folder\\file.json(xml) )");
            var path = _graphicalInterface.ReadText();

            if (Serializer.TryLoad(out _gameInfo, path))
            {
                SetBoard(_gameInfo.Board);
                _graphicalInterface.Clear();
                Start();
            }
            else _graphicalInterface.PrintText("Something wrong. Try again.\nPress \'Esc\'");
        }

        public void CreateNewPlayer()
        {
            _graphicalInterface.PrintText("Enter your name:");
            var name = _graphicalInterface.ReadText();
        }

        private void SetStartValues()
        {

            _graphicalInterface.Clear();

            _gameBoard = new GameBoard();
            _minimax = new Minimax(_gameBoard);

            _graphicalInterface.Fill(_field);

            var ai = _minimax.BestMove();
            _graphicalInterface.PrintChar(BoardCell.ZeroChar, new Point((ai.Point.X * 4) + 50, ai.Point.Y * 2));
            _graphicalInterface.SetCursorPosition(_startPoint);
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
                {
                    _graphicalInterface.PrintText("Tie", new Point(50, 15));
                    Start();
                }
                else
                {
                    _graphicalInterface.PrintText($"{res} is winner!", new Point(50, 15));
                    if (res == BoardCell.CrossChar)
                    {
                        Start();
                    }
                    else
                    {
                        Start();
                    }
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
                _gameBoard.SetCellValue((_startPoint.X - 50) / 4, _startPoint.Y / 2, BoardCell.CrossChar);
                _graphicalInterface.PrintChar(BoardCell.CrossChar);

                var ai = _minimax.BestMove();
                _graphicalInterface.PrintChar(BoardCell.ZeroChar, new Point((ai.Point.X * 4) + 50, ai.Point.Y * 2));
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
                if (_currentState == GameState.Menu)
                    _gameMenu.HandleKey(e.KeyCode);
            }
        }
    }
}