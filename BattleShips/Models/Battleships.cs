// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Menu;
using BattleShips.Misc;

using TicTacToe;
using TicTacToe.Enums;

namespace BattleShips.Models
{
    public class Battleships
    {
        private string[] _board = new string[] { "   1 2 3 4 5 6 7 8 9 10", "  ╔════════════════════╗", " ║∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║", "  ╚════════════════════╝" };

        private string[] _ships = new string[] { " ║∙████████∙∙∙∙∙∙∙∙∙∙∙║ - Battleship x1 in game",
                                         " ║∙██████∙∙∙∙∙∙∙∙∙∙∙∙∙║ - Cruiser x2 in game",
                                         " ║∙████∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║ - Destroyer x3 in game",
                                         " ║∙██∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙∙║ - Torpedo Boar x4 in game" };

        private string _answer = "";

        private IGameMenu _gameMenu;
        private Point _startPosition;
        private GameState _currentState = GameState.Menu;
        private IShell _shell;
        private IPlayer _player;
        private IPlayer _ai;

        public Battleships()
        {

            _shell = new Shell();
            _gameMenu = new GameMenuBar(_shell, this);
            _player = new Player();
            _ai = new AiPlayer();
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
                PreGameSettings(_player);
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
        private void PreGameSettings(IPlayer player)
        {
            do
            {
                _shell.PrintText("Do you want to randomly place ships? (enter y/n)");
                _answer = _shell.ReadText().ToLower();
                _shell.Clear();
            }
            while (_answer != "y" && _answer != "n");

            _startPosition = _answer == "y" ? new Point(3, 4) : new Point(34, 4);

            if (_answer == "y")
                _shell.Fill(_board, _ships, true);
            else
                _shell.Fill(_board, _board);
        }

        private void BackToMenu()
        {
            SwitchState(GameState.Menu);

            _gameMenu.Print();
        }

        private void SetGamePoint(KeyboardHookEventArgs e)
        {
            var maxHight = _answer == "n" ? ParamOfTheEnemyBoard.MaxHight : ParamOfThePlayerBoard.MaxHight;
            var minHight = _answer == "n" ? ParamOfTheEnemyBoard.MinHight : ParamOfThePlayerBoard.MinHight;
            var maxWidth = _answer == "n" ? ParamOfTheEnemyBoard.MaxWidth : ParamOfThePlayerBoard.MaxWidth;
            var minWidth = _answer == "n" ? ParamOfTheEnemyBoard.MinWidth : ParamOfThePlayerBoard.MinWidth;

            if (e.KeyCode == Keys.Up && _startPosition.Y > maxHight)
                _startPosition.Y += Step.Up;

            if (e.KeyCode == Keys.Down && _startPosition.Y < minHight)
                _startPosition.Y += Step.Down;

            if (e.KeyCode == Keys.Left && _startPosition.X > minWidth)
                _startPosition.X += Step.Left;

            if (e.KeyCode == Keys.Right && _startPosition.X < maxWidth)
                _startPosition.X += Step.Right;
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