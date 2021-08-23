// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract;
using BattleShips.Models;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
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
            Model.CellCollectionChanged += OnCellCollectionChanged;
        }

        public event EventHandler<CellChangedEventArgs> CellCollectionChanged;

        public event EventHandler<TicTacToe.Point> MakeShot;

        public PlayerBoardViewModel Board { get; }

        public PlayerBoardViewModel Polygon { get; }

        public IPlayer Model { get; }

        private void OnCellCollectionChanged(object sender, CellChangedEventArgs e)
        {
            CellCollectionChanged?.Invoke(this, e);
        }

        private void OnPlayerShot(object sender, TicTacToe.Point e)
        {
            MakeShot?.Invoke(this, e);
        }
    }
}