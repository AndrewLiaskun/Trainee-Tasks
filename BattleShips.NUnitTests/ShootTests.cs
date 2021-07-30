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
        [Test]
        public void ShootAlgorithm_EasyModShoot_ReturnTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var player = new Player(mock.Object, new PlayerBoardConfig());
            var ai = new AiPlayer(mock.Object, new PlayerBoardConfig());

            player.FillShips();
            ai.FillShips();

            var shot = new ShootAlgorithm();
            shot.EaseModShoot(player, ai);

            Assert.AreEqual(true, ai.Board.Cells.Any(x => x.Value == GameConstants.Got || x.Value == GameConstants.Miss));
        }

        [Test]
        public void Player_MakeShot_ReturnTrue()
        {
            Mock<IShell> mock = new Mock<IShell>();
            mock.Setup(f => f.SetCursorVisible(false));

            var player = new Player(mock.Object, new PlayerBoardConfig());
            var ai = new AiPlayer(mock.Object, new PlayerBoardConfig());

            player.FillShips();
            ai.FillShips();

            ai.Board.ProcessShot(ai.Board.Ships.First().Start);
            player.MakeShot(ai.Board.Ships.First().Start, false, true);

            Assert.AreEqual(1, player.PolygonBoard.Cells.Count(x => x.Value == GameConstants.Got));
        }
    }
}