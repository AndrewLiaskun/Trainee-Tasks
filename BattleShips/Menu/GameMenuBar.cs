// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Collections.Generic;

using BattleShips.Abstract;
using BattleShips.Abstract.Visuals;
using BattleShips.Enums;
using BattleShips.Utils;

using TicTacToe;

using static BattleShips.Resources.Menu;
using static BattleShips.Resources.Serialization;

namespace BattleShips.Menu
{
    internal class GameMenuBar : IGameMenu
    {
        private readonly string[] _logo = Resources.GameLogo.Logo.Split('\n');

        private IVisualContext _shell;
        private IBattleshipGame _game;
        private List<IMenuCommand> _commands;

        public GameMenuBar(IVisualContext visualContext, IBattleshipGame game)
        {
            _game = game;
            _shell = visualContext;

            _commands = new List<IMenuCommand>();

            _commands.Add(new MenuCommand(NewGame, Keys.N, _game.StartNewGame));
            _commands.Add(new MenuCommand(ContinueGame, Keys.C, _game.Resume));
            _commands.Add(new MenuCommand<string>(LoadGame, Keys.L, _game.LoadGame));
            _commands.Add(new MenuCommand<string>(SaveGame, Keys.S, _game.SaveGame));
            _commands.Add(new MenuCommand(About, Keys.A, ShowAboutInfo));
        }

        public IReadOnlyList<IMenuCommand> Commands => _commands;

        public void HandleKey(Keys key)
        {
            foreach (var item in _commands)
            {
                if (key == item.Key)
                {
                    if (key == Keys.L || key == Keys.S)
                        item.Execute(GetPath(key));
                    item.Execute();
                }
            }
        }

        public void Print()
        {
            _shell.Output.Reset();
            _shell.Output.SetForegroundColor(ShellColor.Yellow);

            _shell.FillAtCenter(_logo, Point.Empty);

            _shell.Output.PrintText(PressKeyInBrackets, new Point(0, 8), true);

            int i = 1;
            int indexY = 10;
            foreach (var item in _commands)
            {
                _shell.Output.PrintText($"{i++}) {item.Name} ({item.Key})", new Point(0, indexY), true).EndLine();
                indexY += 2;
            }
            _shell.Output.ResetColor();
        }

        private string GetPath(Keys key)
        {
            _shell.Output.Reset();

            _shell.Output.SetForegroundColor(ShellColor.Yellow);
            _shell.Output.PrintText(key == Keys.L ? LoadPath : SavePath, Point.Empty, true);

            _shell.Output.PrintText(string.Empty, new Point(0, 3), true);
            return _shell.Output.ReadText();
        }

        private void ShowAboutInfo()
        {
            _game.SwitchState(BattleShipsState.About);

            _shell.Output.Reset();

            _shell.FillAtCenter(_game.GetAboutText().Split('\n'), new Point());
        }
    }
}