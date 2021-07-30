// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Misc;
using BattleShips.Models;

using Moq;

using NUnit.Framework;

namespace BattleShips.NUnitTests
{
    [TestFixture]
    public class ShootTests
    {
        private IPlayer _player;
        private IPlayer _ai;

        [SetUp]
        public void SetUp()
        {
            var mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            _player = new Player(mock.Object, new PlayerBoardConfig());
            _ai = new AiPlayer(mock.Object, new PlayerBoardConfig());

            _player.FillShips();
            _ai.FillShips();
        }

        [Test]
        public void ShootAlgorithm_EasyModShoot_ReturnTrue()
        {
            var shot = new ShootAlgorithm();
            shot.EaseModShoot(_player, _ai);

            var result = _ai.Board.Cells.Any(x => x.Value == GameConstants.Got || x.Value == GameConstants.Miss);

            Assert.IsTrue(result);
        }

        [Test]
        public void Player_MakeShot_ReturnTrue()
        {
            var ship = _ai.Board.Ships.First();

            _ai.Board.ProcessShot(ship.Start);

            _player.MakeShot(ship.Start, false, ship.IsAlive);

            var result = _player.PolygonBoard.Cells.Count(x => x.Value == GameConstants.Got);

            Assert.AreEqual(1, result);
        }
    }
}