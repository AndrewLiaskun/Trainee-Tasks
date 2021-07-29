// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Linq;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;
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
        [Test]
        public void BattleShipBoard_AddBattleship_ReturnsTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var board = new BattleShipBoard(mock.Object, new Point());
            board.AddShip(new Battleship(new Point()));

            Assert.AreEqual(true, board.Ships.Contains(new Battleship(new Point())));
        }

        [Test]
        public void BattleShipBoard_ProcessShot_ReturnsTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var board = new BattleShipBoard(mock.Object, new Point());

            board.AddShip(new Battleship(new Point()));
            board.AddShip(new Destroyer(new Point(5, 3)));

            board.ProcessShot(new Point(0, 0));

            Assert.AreEqual(1, board.Ships.Count(x => x.Deck > x.Health));
        }

        [Test]
        public void BattleShipBoard_MoveShip_ReturnsFalse()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var board = new BattleShipBoard(mock.Object, new Point());

            board.AddShip(new Battleship(new Point()));
            var ship = board.Ships.First();

            board.MoveShip(new Point(2, 4), ship, ShipDirection.Vertical);

            Assert.AreEqual(false, ship.Start == new Point(0, 0) && ship.Direction == ShipDirection.Horizontal);
        }

        [Test]
        public void BattleShipBoard_ValidateShip_ReturnsFalse()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var board = new BattleShipBoard(mock.Object, new Point());

            board.AddShip(new Battleship(new Point()));

            Assert.AreEqual(false, board.ValidateShip(new Point(), new Destroyer(new Point(0, 1))) && board.ValidateShip(new Point(), new Destroyer(new Point())));
        }

        [Test]
        public void BattleShipBoard_ChangeOrAddShip_ReturnsTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var board = new BattleShipBoard(mock.Object, new Point());

            board.AddShip(new Destroyer(new Point()));

            board.ChangeOrAddShip(new Point(), new Battleship(new Point()));

            Assert.AreEqual(0, board.Ships.Count(x => x.ShipKind == ShipType.Destroyer));
        }

        [Test]
        public void BattleShipBoard_Reset_ReturnsTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var board = new BattleShipBoard(mock.Object, new Point());

            board.AddShip(new Destroyer(new Point()));
            board.AddShip(new Destroyer(new Point(0, 1)));
            board.AddShip(new Destroyer(new Point(0, 2)));

            board.Reset();

            Assert.AreEqual(true, board.Ships.Count() == 0 && board.GetShipAtOrDefault(new Point()) == null);
        }
    }
}