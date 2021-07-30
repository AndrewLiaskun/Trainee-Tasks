// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Misc;
using BattleShips.Models;
using BattleShips.Utils;

using Moq;

using NUnit.Framework;

using TicTacToe;

namespace BattleShips.NUnitTests
{
    [TestFixture]
    public class GameSerializerTests
    {
        [Test]
        public void GameSerializer_TrySaveXML_ReturnsTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var player = new Player(mock.Object, new PlayerBoardConfig());
            var ai = new AiPlayer(mock.Object, new PlayerBoardConfig());
            player.FillShips();
            ai.FillShips();

            Assert.AreEqual(true, GameSerializer.TrySave(GameMetadata.FromGame(player, ai), "save.xml"));
        }

        [Test]
        public void GameSerializer_TryLoadXML_ReturnsTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var player = new Player(mock.Object, new PlayerBoardConfig());
            var ai = new AiPlayer(mock.Object, new PlayerBoardConfig());
            if (GameSerializer.TryLoad("save.xml", out var game))
            {
                player.Load(game.Players[0]);
                ai.Load(game.Players[1]);
            }

            Assert.AreEqual(true, player.Board.Ships.Count() == 10 && ai.Board.Ships.Count() == 10);
        }

        [Test]
        public void GameSerializer_TrySaveJSON_ReturnsTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var player = new Player(mock.Object, new PlayerBoardConfig());
            var ai = new AiPlayer(mock.Object, new PlayerBoardConfig());
            player.FillShips();
            ai.FillShips();

            Assert.AreEqual(true, GameSerializer.TrySave(GameMetadata.FromGame(player, ai), "save.json"));
        }

        [Test]
        public void GameSerializer_TryLoadJSON_ReturnsTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var player = new Player(mock.Object, new PlayerBoardConfig());
            var ai = new AiPlayer(mock.Object, new PlayerBoardConfig());
            if (GameSerializer.TryLoad("save.json", out var game))
            {
                player.Load(game.Players[0]);
                ai.Load(game.Players[1]);
            }

            Assert.AreEqual(true, player.Board.Ships.Count() == 10 && ai.Board.Ships.Count() == 10);
        }
    }
}