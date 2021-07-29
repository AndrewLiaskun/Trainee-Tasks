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
        [Test]
        public void PlayerShipGenerator_CreateBattleship_ReturnsTrue()
        {
            var generator = new PlayerShipGenerator();

            var ship = generator.CreateShip(new Point());

            Assert.AreEqual(true, ship.ShipKind == ShipType.Battleship);
        }

        [Test]
        public void OpponentShipGenerator_CreateTorpedoBoat_ReturnsTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var generator = new OpponentShipGenerator(new BattleShipBoard(mock.Object, new Point()), new PlayerShipGenerator());
            var ship = generator.Create(new Point(), true);

            Assert.AreEqual(true, ship.ShipKind == ShipType.TorpedoBoat);
        }

        [Test]
        public void RandomShipGenerator_PlaceShips_ReturnTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var player = new Player(mock.Object, new PlayerBoardConfig());
            var generator = new RandomShipGenerator(player);
            generator.PlaceShips();

            Assert.AreEqual(10, player.Board.AliveShips);
        }
    }
}