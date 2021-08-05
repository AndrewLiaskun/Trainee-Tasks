// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Models;

using TicTacToe;

using static BattleShips.Resources.Menu;

namespace BattleShips.Menu
{
    internal class GameMenuBar : IGameMenu
    {

        private readonly string[] _logo = Resources.GameLogo.Logo.Split('\n');
        private IShell _shell;
        private BattleshipsGame _game;
        private List<IMenuCommand> _commands;

        public GameMenuBar(IShell graphicsInterface, BattleshipsGame game)
        {
            _game = game;
            _shell = graphicsInterface;

            _commands = new List<IMenuCommand>();

            _commands.Add(new MenuCommand(NewGame, Keys.N, _game.StartNewGame));
            _commands.Add(new MenuCommand(ContinueGame, Keys.C, _game.Resume));
            _commands.Add(new MenuCommand(LoadGame, Keys.L, _game.LoadGame));
            _commands.Add(new MenuCommand(SaveGame, Keys.S, _game.SaveGame));
            _commands.Add(new MenuCommand(About, Keys.A, ShowAboutInfo));
        }

        public IReadOnlyList<IMenuCommand> Commands => throw new NotImplementedException();

        public void HandleKey(Keys key)
        {
            foreach (var item in _commands)
            {
                if (key == item.Key)
                {
                    item.Execute();
                }
            }
        }

        public void Print()
        {
            _shell.Clear();
            _shell.SetForegroundColor(ShellColor.Yellow);
            _shell.FillAtCenter(_logo, new Point());

            _shell.PrintTextInCenter(PressKeyInBrackets, new Point(0, 8));

            int i = 1;
            int indexY = 10;
            foreach (var item in _commands)
            {
                _shell.PrintTextInCenter($"{i++}) {item.Name} ({item.Key})", new Point(0, indexY)).EndLine();
                indexY += 2;
            }
            _shell.ResetColor();
        }

        private void ShowAboutInfo()
        {
            _game.SwitchState(BattleShipsState.About);
            _shell.Clear();
            _shell.FillAtCenter(_game.GetAboutText().Split('\n'), new Point());
        }
    }
}