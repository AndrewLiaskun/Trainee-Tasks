// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.ViewModels.Board;

namespace BattleShips.UI.ViewModels.Players
{
    public class PlayerViewModel : BaseViewModel, IModelProvider<IPlayer>
    {
        private ICommand _resetCommand;

        private ICommand _fillCommand;

        public PlayerViewModel(IPlayer player)
        {
            Model = player ?? throw new ArgumentNullException(nameof(player));

            Board = new PlayerBoardViewModel(player.Board);
            Polygon = new PlayerBoardViewModel(player.PolygonBoard);
        }

        public ICommand ResetCommand
        {
            get => _resetCommand ?? (_resetCommand = new RelayCommand(Model.Reset));
        }

        public ICommand FillCommand
        {
            get => _fillCommand ?? (_fillCommand = new RelayCommand(Model.FillShips));
        }

        public PlayerBoardViewModel Board { get; }

        public PlayerBoardViewModel Polygon { get; }

        public IPlayer Model { get; }

        public void FillShips()
        {
            Model.FillShips();
        }
    }
}