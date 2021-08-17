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

            var computerBoard = Computer.Board.Model;
            var isEmpty = computerBoard.GetCellValue(e).Value == GameConstants.Empty;

            PlayerRequest(e, computerBoard, isEmpty);

            ComputerResponse(e, computerBoard, isEmpty);
        }

        private void PlayerRequest(TicTacToe.Point e, IBattleShipBoard computerBoard, bool isEmpty)
        {
            var isAlive = true;

            computerBoard.ProcessShot(e);
            foreach (var item in computerBoard.Ships)
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
        }

        private void ComputerResponse(TicTacToe.Point e, IBattleShipBoard computerBoard, bool isEmpty)
        {
            if (isEmpty)
            {
                computerBoard.SetCellValue(e, GameConstants.Miss);
                _aiShooter.MakeShoot(Computer.Model, User.Model);

                foreach (var item in User.Board.Cells)
                    item.RefreshAllBindings();

                RaisePropertyChanged(nameof(User.Board.Cells));
            }
        }
    }
}