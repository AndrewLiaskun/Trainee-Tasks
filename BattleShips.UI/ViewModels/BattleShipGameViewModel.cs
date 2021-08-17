// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Windows;

using BattleShips.Abstract;
using BattleShips.Misc;
using BattleShips.Models;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.ViewModels.Players;

namespace BattleShips.UI.ViewModels
{
    public class BattleShipGameViewModel : BaseViewModel, IModelProvider<IBattleshipGame>
    {
        private ShootAlgorithm _aiShooter;

        public BattleShipGameViewModel(IBattleshipGame game)
        {
            Model = game ?? throw new ArgumentNullException(nameof(game));

            Computer = new PlayerViewModel(game.Computer);
            User = new PlayerViewModel(game.User);

            User.MakeShot += User_MakeShot;
            _aiShooter = new ShootAlgorithm();
        }

        public IBattleshipGame Model { get; }

        public PlayerViewModel Computer { get; }

        public PlayerViewModel User { get; }

        private void User_MakeShot(object sender, TicTacToe.Point e)
        {

            var isEmpty = Computer.Board.Model.GetCellValue(e).Value == GameConstants.Empty;
            var isAlive = true;

            Computer.Board.Model.ProcessShot(e);
            foreach (var item in Computer.Board.Model.Ships)
            {
                if (isEmpty)
                    break;
                if (item.Includes(e) && !item.IsAlive)
                {
                    isAlive = false;
                    break;
                }
            }
            User.Model.MakeShot(e, isEmpty, isAlive);

            if (isEmpty)
            {
                Computer.Board.Model.SetCellValue(e, GameConstants.Miss);
                _aiShooter.MakeShoot(Computer.Model, User.Model);
                foreach (var item in User.Board.Cells)
                    item.RefreshAllBindings();
                RaisePropertyChanged(nameof(User.Board.Cells));
            }
        }
    }
}