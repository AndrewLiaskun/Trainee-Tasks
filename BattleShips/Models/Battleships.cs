// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Battleships()
        {
            _startPosition = new Point(59, 4);
            _shell = new Shell();
            _gameMenu = new Menu.GameMenu(_shell, this);
        }

        public void SwitchState(GameState state)
        {
            _currentState = state;
        }

        public void StarGame()
        {
            if (_currentState == GameState.Menu)
            {
                _gameMenu.Print();
            }
            else
            {

                _shell.KeyPressed -= _shell_KeyPressed;
                _shellCreater = new ShellCreater();
                _shellCreater.Create(_shell);

                _shell.KeyPressed += _shell_KeyPressed;
                _shell.StartRunLoop();
            }
        }

        private void BackToMenu()
        {
            SwitchState(GameState.Menu);

            _gameMenu.Print();
        }

        private void SetGamePoint(KeyboardHookEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) BackToMenu();
            if (e.KeyCode == Keys.Up && _startPosition.Y > 4) _startPosition.Y -= 1;
            if (e.KeyCode == Keys.Down && _startPosition.Y < 13) _startPosition.Y += 1;
            if (e.KeyCode == Keys.Left && _startPosition.X > 59) _startPosition.X -= 2;
            if (e.KeyCode == Keys.Right && _startPosition.X < 77) _startPosition.X += 2;
        }

        private void GameControl(KeyboardHookEventArgs e)
        {

            SetGamePoint(e);
            _shell.SetCursorPosition(_startPosition);
        }

        private void _shell_KeyPressed(object sender, KeyboardHookEventArgs e)
        {
            GameControl(e);
        }
    }
}