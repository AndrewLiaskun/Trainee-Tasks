// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Collections.Generic;
using System.IO;

using TicTacToe.Abstract;
using TicTacToe.Enums;

namespace TicTacToe
{
    public class GameMenu : IGameMenu
    {
        private const string _gameTitle = @"
████████╗██╗░█████╗░  ████████╗░█████╗░░█████╗░  ████████╗░█████╗░███████╗
╚══██╔══╝██║██╔══██╗  ╚══██╔══╝██╔══██╗██╔══██╗  ╚══██╔══╝██╔══██╗██╔════╝
░░░██║░░░██║██║░░╚═╝  ░░░██║░░░███████║██║░░╚═╝  ░░░██║░░░██║░░██║█████╗░░
░░░██║░░░██║██║░░██╗  ░░░██║░░░██╔══██║██║░░██╗  ░░░██║░░░██║░░██║██╔══╝░░
░░░██║░░░██║╚█████╔╝  ░░░██║░░░██║░░██║╚█████╔╝  ░░░██║░░░╚█████╔╝███████╗
░░░╚═╝░░░╚═╝░╚════╝░  ░░░╚═╝░░░╚═╝░░╚═╝░╚════╝░  ░░░╚═╝░░░░╚════╝░╚══════╝";

        private IGraphicalInterface _graphicInterface;
        private TicTacToe _game;
        private List<IMenuCommand> _commands;

        public GameMenu(IGraphicalInterface graphicsInterface, TicTacToe game)
        {
            _game = game;
            _graphicInterface = graphicsInterface;

            _commands = new List<IMenuCommand>();

            _commands.Add(new MenuCommand("Create new player", Keys.P, CreateNewPlayer));
            _commands.Add(new MenuCommand("New game", Keys.N, StartNewGame));
            _commands.Add(new MenuCommand("Continue game", Keys.C, ContinueGame));
            _commands.Add(new MenuCommand("Load game", Keys.L, LoadGame));
            _commands.Add(new MenuCommand("Save game", Keys.S, SaveGame));
            _commands.Add(new MenuCommand("About", Keys.A, ShowAboutInfo));
        }

        public IReadOnlyList<IMenuCommand> Commands { get; }

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

        private void CreateNewPlayer()
        {
            _game.SwitchState(GameState.CreateNewPlayer);
            _graphicInterface.Clear();
            _game.CreateNewPlayer();
            _graphicInterface.PrintText("Press \'Esc\'");
        }

        private void ContinueGame()
        {
            _game.SwitchState(GameState.Game);
            _graphicInterface.Clear();
            _game.Resume();
        }

        private void SaveGame()
        {
            _game.SwitchState(GameState.SaveGame);
            _graphicInterface.Clear();
            _game.SaveGame();
        }

        private void LoadGame()
        {
            _game.SwitchState(GameState.LoadGame);
            _graphicInterface.Clear();
            _game.LoadGame();
        }

        private void StartNewGame()
        {
            _game.SwitchState(GameState.Game);
            _game.Start();
        }

        private void ShowAboutInfo()
        {
            _game.SwitchState(GameState.About);
            _graphicInterface.Clear();
            _graphicInterface.PrintText("INFA!!!!!!!!!!!!!!");
        }
    }
}