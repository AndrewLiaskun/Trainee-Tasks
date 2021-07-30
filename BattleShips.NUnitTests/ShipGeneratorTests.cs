// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Models;
using BattleShips.Ships.Generators;

using Moq;

using NUnit.Framework;

using TicTacToe;

namespace BattleShips.NUnitTests
{

    [TestFixture]
    public class ShipGeneratorTests
    {
        private Mock<IShell> _mock;
        private IPlayer _player;

        [SetUp]
        public void SetUp()
        {
            _mock = new Mock<IShell>();
            _mock.Setup(f => f.SetCursorVisible(false));

            _player = new Player(_mock.Object, new PlayerBoardConfig());
        }

        [Test]
        public void PlayerShipGenerator_CreateBattleship_ReturnsTrue()
        {
            var generator = new PlayerShipGenerator(_player);

            var ship = generator.CreateShip(new Point());

            Assert.IsTrue(ship.ShipKind == ShipType.Battleship);
        }

        [Test]
        public void OpponentShipGenerator_CreateTorpedoBoat_ReturnsTrue()
        {

            var generator = new OpponentShipGenerator(new BattleShipBoard(_mock.Object, new Point()), new PlayerShipGenerator(_player));
            var ship = generator.Create(new Point(), true);

            Assert.IsTrue(ship.ShipKind == ShipType.TorpedoBoat);
        }

        [Test]
        public void RandomShipGenerator_PlaceShips_ReturnTrue()
        {

            var generator = new RandomShipGenerator(_player);
            generator.PlaceShips();

            Assert.AreEqual(10, _player.Board.AliveShips);
        }

        [Test]
        public void RandomShipGenerator_CreateShip_Return_1()
        {

            _player.CreateShip(new Point());

            var result = _player.Board.Ships.Count;

            Assert.AreEqual(1, result);
        }
    }
}