// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Abstract;
using BattleShips.Models;

using TicTacToe;

namespace BattleShips.Menu
{
    internal class GameMenuBar : IGameMenu
    {
        private const string _gameTitle = @"
██████╗░░█████╗░████████╗████████╗██╗░░░░░███████╗░██████╗██╗░░██╗██╗██████╗░░██████╗
██╔══██╗██╔══██╗╚══██╔══╝╚══██╔══╝██║░░░░░██╔════╝██╔════╝██║░░██║██║██╔══██╗██╔════╝
██████╦╝███████║░░░██║░░░░░░██║░░░██║░░░░░█████╗░░╚█████╗░███████║██║██████╔╝╚█████╗░
██╔══██╗██╔══██║░░░██║░░░░░░██║░░░██║░░░░░██╔══╝░░░╚═══██╗██╔══██║██║██╔═══╝░░╚═══██╗
██████╦╝██║░░██║░░░██║░░░░░░██║░░░███████╗███████╗██████╔╝██║░░██║██║██║░░░░░██████╔╝
╚═════╝░╚═╝░░╚═╝░░░╚═╝░░░░░░╚═╝░░░╚══════╝╚══════╝╚═════╝░╚═╝░░╚═╝╚═╝╚═╝░░░░░╚═════╝░";

        private IShell _graphicInterface;
        private Battleships _game;
        private List<IMenuCommand> _commands;

        public GameMenuBar(IShell graphicsInterface, Battleships game)
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

            _graphicInterface.PrintTextLine(_gameTitle + "\n\n");

            _graphicInterface.PrintTextLine("Press key in ()\n\n");

            int i = 1;
            foreach (var item in _commands)
            {
                _graphicInterface.PrintTextLine($"{i++}) {item.Name} ({item.Key})");
            }
        }

        private void ShowAboutInfo()
        {
            _game.SwitchState(TicTacToe.Enums.GameState.About);
            _graphicInterface.Clear();
            _graphicInterface.PrintTextLine(_game.GetAboutText());
        }

        private void SaveGame() => throw new NotImplementedException();

        private void LoadGame() => throw new NotImplementedException();

        private void ContinueGame() => throw new NotImplementedException();

        private void StartNewGame()
        {
            _game.SwitchState(TicTacToe.Enums.GameState.Game);
            _game.StarGame();
        }
    }
}