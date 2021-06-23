// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Collections.Generic;

using TicTacToe.Abstract;
using TicTacToe.Enums;

namespace TicTacToe
{
    public class GameMenu
    {
        private IGraphicalInterface _graphicInterface;
        private TicTacToe _game;
        private List<MenuCommand> _commands;

        public GameMenu(IGraphicalInterface graphicsInterface, TicTacToe game)
        {
            _game = game;
            _graphicInterface = graphicsInterface;

            _commands = new List<MenuCommand>();

            _commands.Add(new MenuCommand("Continue game", Keys.C, ContinueGame));
            _commands.Add(new MenuCommand("New game", Keys.N, StartNewGame));
            _commands.Add(new MenuCommand("Save game", Keys.S, SaveGame));
            _commands.Add(new MenuCommand("Load game", Keys.L, LoadGame));
            _commands.Add(new MenuCommand("Game results", Keys.R, GameResults));
            _commands.Add(new MenuCommand("About", Keys.A, ShowAboutInfo));
            _commands.Add(new MenuCommand("Create new player", Keys.P, CreateNewPlayer));
        }

        public IReadOnlyList<MenuCommand> Commands { get; }

        public void Print()
        {
            _graphicInterface.Clear();
            foreach (var item in _commands)
            {
                _graphicInterface.PrintText($"Press {item.Key} to {item.Name}");
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
            _graphicInterface.Clear();
            _game.SetNewPlayer();
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
            _graphicInterface.PrintText("Enter path: ( Path be like: C:\\folder\\file.json(xml) )");
            var path = _graphicInterface.ReadText();
            if (path.Contains(".xml"))
            {
                Serializer.XmlSave(_game, path);
                _graphicInterface.PrintText("Successful conservation");
            }
            if (path.Contains(".json"))
            {
                Serializer.JsonSave(_game, path);
                _graphicInterface.PrintText("Successful conservation");
            }
            _graphicInterface.PrintText("Press \'Esc\'");
        }

        private void GameResults()
        {
            _game.SwitchState(GameState.Results);
            _graphicInterface.Clear();
        }

        private void LoadGame()
        {
            _game.SwitchState(GameState.LoadGame);
            _graphicInterface.Clear();
            _graphicInterface.PrintText("Enter path: ( Path be like: C:\\folder\\file.json(xml) )");
            var path = _graphicInterface.ReadText();

            if (path.Contains(".xml"))
                _game.SetBoard(Serializer.XmlLoad(path));
            if (path.Contains(".json"))
                _game.SetBoard(Serializer.JsonLoad(path));
            _graphicInterface.Clear();
            _game.Start();
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