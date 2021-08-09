// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Abstract.Visuals;
using BattleShips.Misc;
using BattleShips.Models;
using BattleShips.Utils;

using Moq;

using NUnit.Framework;

namespace BattleShips.NUnitTests
{
    [TestFixture]
    public class GameSerializerTests
    {
        private IPlayer _player;
        private IPlayer _ai;

        [SetUp]
        public void SetUp()
        {
            Mock<IVisualContext> mock = new Mock<IVisualContext>().SetupAllProperties();

            _player = new Player(mock.Object, new PlayerBoardConfig());
            _ai = new AiPlayer(mock.Object, new PlayerBoardConfig());
        }

        [Test]
        public void GameSerializer_TrySaveXML_ReturnsTrue()
        {

            _player.FillShips();
            _ai.FillShips();

            Assert.IsTrue(GameSerializer.TrySave(GameMetadata.FromGame(_player, _ai), "save.xml"));
        }

        [Test]
        public void GameSerializer_TryLoadXML_ReturnsTrue()
        {
            Assert.IsTrue(GameSerializer.TryLoad("save.xml", out var game));
        }

        [Test]
        public void GameSerializer_TrySaveJSON_ReturnsTrue()
        {

            _player.FillShips();
            _ai.FillShips();

            Assert.IsTrue(GameSerializer.TrySave(GameMetadata.FromGame(_player, _ai), "save.json"));
        }

        [Test]
        public void GameSerializer_TryLoadJSON_ReturnsTrue()
        {
            Assert.IsTrue(GameSerializer.TryLoad("save.json", out var game));
        }
    }
}