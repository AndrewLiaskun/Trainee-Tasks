// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Enums;
using BattleShips.Misc;

using TicTacToe;
using TicTacToe.Abstract;
using TicTacToe.Enums;

namespace BattleShips.Models
{
    public class Battleships
    {
        private IGameMenu _gameMenu;
        private Point _startPosition;
        private GameState _currentState = GameState.Menu;
        private IGraphicalInterface _shell;
        private ShellCreater _shellCreater;
        private IBoard _board;

        public Battleships()
        {
            _startPosition = new Point(59, 4);
            _shell = new Shell();
            _gameMenu = new Menu.GameMenu(_shell, this);
            _board = new GameBoard();
        }

        public void SwitchState(GameState state)
        {
            _currentState = state;
        }

        public void StarGame()
        {
            _shell.KeyPressed -= _shell_KeyPressed;
            _shell.Clear();
            if (_currentState == GameState.Menu)
            {
                _gameMenu.Print();
            }
            else if (_currentState == GameState.Game)
            {
                PreGameSettings(_board);
            }
            _shell.KeyPressed += _shell_KeyPressed;
            _shell.StartRunLoop();
        }

        public string GetAboutText()
        {
            return "For Global Logic trainee course by Andrii Liaskun";
        }

        /// <summary>
        /// This method must ask some questions for random create board or by myself
        /// </summary>
        /// <param name="PreGameSettings"></param>
        private void PreGameSettings(IBoard board)
        {
            _shellCreater = new ShellCreater();
            //TODO set Question
            _shell.PrintText("Some questions");
            _shellCreater.Create(_shell);
        }

        private void BackToMenu()
        {
            SwitchState(GameState.Menu);

            _gameMenu.Print();
        }

        private void SetGamePoint(KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Up && _startPosition.Y > (int)SetBoard.MaxHight) _startPosition.Y += (int)SetStep.Up;
            if (e.KeyCode == Keys.Down && _startPosition.Y < (int)SetBoard.MinHight) _startPosition.Y += (int)SetStep.Down;
            if (e.KeyCode == Keys.Left && _startPosition.X > (int)SetBoard.MinWidth) _startPosition.X += (int)SetStep.Left;
            if (e.KeyCode == Keys.Right && _startPosition.X < (int)SetBoard.MaxWidth) _startPosition.X += (int)SetStep.Right;
        }

        private void GameControl(KeyboardHookEventArgs e)
        {

            SetGamePoint(e);
            _shell.SetCursorPosition(_startPosition);
        }

        private void _shell_KeyPressed(object sender, KeyboardHookEventArgs e)
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