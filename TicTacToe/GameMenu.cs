// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

using Newtonsoft.Json;

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

        private void ContinueGame()
        {
            _game.SwitchState(GameState.Game);
            _graphicInterface.Clear();
            _game.Resume();
        }

        private void SaveGame()
        {
            _game.SwitchState(GameState.SaveGame);
            if (_game.GetFileFormat() == SaveFormat.XML)
                XmlSave();
            if (_game.GetFileFormat() == SaveFormat.JSON)
                JsonSave();
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
            if (_game.GetFileFormat() == SaveFormat.XML)
                _game.SetBoard(XmlLoad());
            if (_game.GetFileFormat() == SaveFormat.JSON)
                _game.SetBoard(JsonLoad());
            _game.Start();
        }

        private void StartNewGame()
        {
            _game.SwitchState(GameState.Game);
            _graphicInterface.Clear();
            _game.Start();
        }

        private void ShowAboutInfo()
        {
            _game.SwitchState(GameState.About);
            _graphicInterface.Clear();
            _graphicInterface.PrintText("INFA!!!!!!!!!!!!!!");
        }

        private void XmlSave()
        {
            var board = _game.GetBoard();
            var formatter = new XmlSerializer(typeof(BoardCell[]));
            using (var fs = new FileStream("board.xml", FileMode.OpenOrCreate))
            {
                formatter.Serialize(fs, board.Cells);
            }
        }

        private BoardCell[] XmlLoad()
        {
            BoardCell[] loadedBoard;
            using (var fs = new FileStream("board.xml", FileMode.OpenOrCreate))
            {
                var formatter = new XmlSerializer(typeof(BoardCell[]));
                loadedBoard = (BoardCell[])formatter.Deserialize(fs);
            }
            return loadedBoard;
        }

        private void JsonSave()
        {
            var board = _game.GetBoard();
            using (StreamWriter file = File.CreateText(@"E:\path.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, board.Cells as BoardCell[]);
            }
        }

        private BoardCell[] JsonLoad()
        {
            BoardCell[] loadedBoard;
            using (StreamReader file = File.OpenText(@"E:\path.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                loadedBoard = (BoardCell[])serializer.Deserialize(file, typeof(BoardCell[]));
            }
            return loadedBoard;
        }
    }
}