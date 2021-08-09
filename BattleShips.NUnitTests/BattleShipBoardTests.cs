// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Linq;

using BattleShips.Abstract;
using BattleShips.Abstract.Visuals;
using BattleShips.Enums;
using BattleShips.Models;
using BattleShips.Ships;

using Moq;

using NUnit.Framework;

using TicTacToe;

namespace BattleShips.NUnitTests
{
    [TestFixture]
    public class BattleShipBoardTests
    {
        private IBattleShipBoard _board;

        [SetUp]
        public void SetUp()
        {
            var tableMock = new Mock<IVisualTable>().SetupAllProperties();

            Mock<IVisualContext> mock = new Mock<IVisualContext>().SetupAllProperties();

            mock.Setup((x) => x.Create(It.IsAny<Point>())).Returns(tableMock.Object);

            _board = new BattleShipBoard(mock.Object, new Point());
        }

        [Test]
        public void BattleShipBoard_AddBattleship_ReturnsTrue()
        {

            _board.AddShip(new Battleship(new Point()));

            Assert.AreEqual(true, _board.Ships.Contains(new Battleship(new Point())));
        }

        [Test]
        public void BattleShipBoard_ProcessShot_ReturnsTrue()
        {

            _board.AddShip(new Battleship(new Point()));
            _board.AddShip(new Destroyer(new Point(5, 3)));

            _board.ProcessShot(new Point(0, 0));

            Assert.AreEqual(1, _board.Ships.Count(x => x.Deck > x.Health));
        }

        [Test]
        public void BattleShipBoard_MoveShip_ReturnsFalse()
        {

            _board.AddShip(new Battleship(new Point()));
            var ship = _board.Ships.First();

            _board.MoveShip(new Point(2, 4), ship, ShipDirection.Vertical);

            Assert.IsFalse(ship.Start == new Point(0, 0));
        }

        [Test]
        public void BattleShipBoard_ValidateShip_ReturnsFalse()
        {
            var battleship = new Battleship(new Point());
            _board.AddShip(battleship);

            var destroyer = new Destroyer(new Point(0, 1));

            Assert.IsFalse(_board.ValidateShip(destroyer.Start, destroyer));
        }

        [Test]
        public void BattleShipBoard_ChangeOrAddShip_ReturnsTrue()
        {
            var destroyer = new Destroyer(new Point());
            _board.AddShip(destroyer);

            _board.ChangeOrAddShip(new Point(), new Battleship(new Point()));

            var result = _board.Ships.First().ShipKind == ShipType.Battleship;

            Assert.IsTrue(result);
        }

        [Test]
        public void BattleShipBoard_Reset_Returns_0()
        {
            _board.AddShip(new Destroyer(new Point()));

            _board.Reset();

            var result = _board.Ships.Count == 0;

            Assert.IsTrue(result);
        }
    }
}