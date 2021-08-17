// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Windows;
using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.Misc;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.Commands;
using BattleShips.UI.ViewModels.Board;

namespace BattleShips.UI.ViewModels.Players
{
    public class PlayerViewModel : BaseViewModel, IModelProvider<IPlayer>
    {

        public PlayerViewModel(IPlayer player)
        {
            Model = player ?? throw new ArgumentNullException(nameof(player));

            Board = new PlayerBoardViewModel(player.Board);
            Polygon = new PlayerBoardViewModel(player.PolygonBoard);

            Polygon.PlayerShot += OnPlayerShot;
            Board.PlayerShot += OnPlayerShot;
        }

        public event EventHandler<TicTacToe.Point> MakeShot;

        public PlayerBoardViewModel Board { get; }

        public PlayerBoardViewModel Polygon { get; }

        public IPlayer Model { get; }

        private void OnPlayerShot(object sender, TicTacToe.Point e)
        {
            MakeShot?.Invoke(this, e);
        }
    }
}