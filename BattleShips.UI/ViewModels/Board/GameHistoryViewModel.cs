// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.UI.Basic;
using BattleShips.UI.ViewModels.Players;

using TicTacToe;

namespace BattleShips.UI.ViewModels.Board
{
    public class GameHistoryViewModel : BaseViewModel
    {
        private PlayerBoardViewModel _user;
        private PlayerBoardViewModel _ai;

        public GameHistoryViewModel(PlayerBoardViewModel user, PlayerBoardViewModel ai)
        {
            _user = user;
            _ai = ai;
            _ai.PlayerShot += _user_PlayerShot;
            foreach (var item in _user.Cells)
                item.Clicked += OnBoardChange;
            foreach (var item in _ai.Cells)
                item.Clicked += OnBoardChange;
            foreach (var item in _ai.Ships)
                item.Model.ShipChanged += Model_ShipChanged;
        }

        private void Model_ShipChanged(object sender, Misc.ShipChangedEventArgs e)
        {
            var s = 5;
        }

        private void _user_PlayerShot(object sender, Point e)
        {
            var s = 5;
        }

        private void OnBoardChange(object sender, TicTacToe.Point e)
        {
            var s = 5;
        }
    }
}