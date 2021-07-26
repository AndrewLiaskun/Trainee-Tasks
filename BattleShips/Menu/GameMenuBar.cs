// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Models;

using TicTacToe;

namespace BattleShips.Menu
{
    internal class GameMenuBar : IGameMenu
    {

        private static readonly string[] _logo = { @"██████╗░░█████╗░████████╗████████╗██╗░░░░░███████╗░██████╗██╗░░██╗██╗██████╗░░██████╗
", @"██╔══██╗██╔══██╗╚══██╔══╝╚══██╔══╝██║░░░░░██╔════╝██╔════╝██║░░██║██║██╔══██╗██╔════╝
", @"██████╦╝███████║░░░██║░░░░░░██║░░░██║░░░░░█████╗░░╚█████╗░███████║██║██████╔╝╚█████╗░
", @"██╔══██╗██╔══██║░░░██║░░░░░░██║░░░██║░░░░░██╔══╝░░░╚═══██╗██╔══██║██║██╔═══╝░░╚═══██╗
", @"██████╦╝██║░░██║░░░██║░░░░░░██║░░░███████╗███████╗██████╔╝██║░░██║██║██║░░░░░██████╔╝
" };

        private IShell _graphicInterface;
        private BattleshipsGame _game;
        private List<IMenuCommand> _commands;

        public GameMenuBar(IShell graphicsInterface, BattleshipsGame game)
        {
            _game = game;
            _graphicInterface = graphicsInterface;

            _commands = new List<IMenuCommand>();

            _commands.Add(new MenuCommand("New game", Keys.N, StartNewGame));
            _commands.Add(new MenuCommand("Continue game", Keys.C, ContinueGame));
            _commands.Add(new MenuCommand("Load game", Keys.L, LoadGame));
            _commands.Add(new MenuCommand("Save game", Keys.S, SaveGame));
            _commands.Add(new MenuCommand("About", Keys.A, ShowAboutInfo));
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
            _graphicInterface.Clear();
            _graphicInterface.SetForegroundColor(ShellColor.Yellow);
            _graphicInterface.FillAtCenter(new Point(), _logo);

            _graphicInterface.PrintTextInCenter("Press key in ()", new Point(0, 8));

            int i = 1;
            int indexY = 10;
            foreach (var item in _commands)
            {
                _graphicInterface.PrintTextInCenter($"{i++}) {item.Name} ({item.Key})", new Point(0, indexY)).EndLine();
                indexY += 2;
            }
            _graphicInterface.ResetColor();
        }

        private void ShowAboutInfo()
        {
            _game.SwitchState(BattleShipsState.About);
            _graphicInterface.Clear();
            _graphicInterface.PrintText(_game.GetAboutText());
        }

        private void SaveGame() => throw new NotImplementedException();

        private void LoadGame() => throw new NotImplementedException();

        private void ContinueGame()
        {
            _game.SwitchState(BattleShipsState.Game);
            _game.Resume();
        }

        private void StartNewGame()
        {
            _game.SwitchState(BattleShipsState.Game);
            _game.StartGame();
        }
    }
}