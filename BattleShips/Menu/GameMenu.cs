// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Models;

using TicTacToe;
using TicTacToe.Abstract;

namespace BattleShips.Menu
{
    internal class GameMenu : IGameMenu
    {
        private const string _gameTitle = @"
██████╗░░█████╗░████████╗████████╗██╗░░░░░███████╗░██████╗██╗░░██╗██╗██████╗░░██████╗
██╔══██╗██╔══██╗╚══██╔══╝╚══██╔══╝██║░░░░░██╔════╝██╔════╝██║░░██║██║██╔══██╗██╔════╝
██████╦╝███████║░░░██║░░░░░░██║░░░██║░░░░░█████╗░░╚█████╗░███████║██║██████╔╝╚█████╗░
██╔══██╗██╔══██║░░░██║░░░░░░██║░░░██║░░░░░██╔══╝░░░╚═══██╗██╔══██║██║██╔═══╝░░╚═══██╗
██████╦╝██║░░██║░░░██║░░░░░░██║░░░███████╗███████╗██████╔╝██║░░██║██║██║░░░░░██████╔╝
╚═════╝░╚═╝░░╚═╝░░░╚═╝░░░░░░╚═╝░░░╚══════╝╚══════╝╚═════╝░╚═╝░░╚═╝╚═╝╚═╝░░░░░╚═════╝░";

        private IGraphicalInterface _graphicInterface;
        private Battleships _game;
        private List<IMenuCommand> _commands;

        public GameMenu(IGraphicalInterface graphicsInterface, Battleships game)
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

        public void HandleKey(Keys key) => throw new NotImplementedException();

        public void Print()
        {
            _graphicInterface.Clear();

            _graphicInterface.PrintText(_gameTitle + "\n\n");

            _graphicInterface.PrintText("Press key in ()\n\n");

            int i = 1;
            foreach (var item in _commands)
            {
                _graphicInterface.PrintText($"{i++}) {item.Name} ({item.Key})");
            }
        }

        private void ShowAboutInfo() => throw new NotImplementedException();

        private void SaveGame() => throw new NotImplementedException();

        private void LoadGame() => throw new NotImplementedException();

        private void ContinueGame() => throw new NotImplementedException();

        private void StartNewGame() => throw new NotImplementedException();
    }
}