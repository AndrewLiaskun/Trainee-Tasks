// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.ObjectModel;

using BattleShips.Abstract;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.Models.Visuals;
using BattleShips.UI.ViewModels.Players;

using TicTacToe;

namespace BattleShips.UI.ViewModels
{
    public class BattleShipGameViewModel : BaseViewModel, IModelProvider<IBattleshipGame>
    {

        public BattleShipGameViewModel(IBattleshipGame game)
        {
            Model = game ?? throw new ArgumentNullException(nameof(game));

            Computer = new PlayerViewModel(game.Computer);
            User = new PlayerViewModel(game.User);
            History = new GameHistoryViewModel(game.GameHistory);

            User.MakeShot += User_MakeShot;

            Model.HistoryRecordsChanged += OnHistoryRecordsChanged;
        }

        public IBattleshipGame Model { get; }

        public PlayerViewModel Computer { get; }

        public PlayerViewModel User { get; }

        public GameHistoryViewModel History { get; }

        private void OnHistoryRecordsChanged(object sender, Misc.Args.HistoryRecordsChangedEventArgs e)
        {
            var record = new HistoryRecordViewModel(e.HistoryRecord);
            History.Add(record);
        }

        private void User_MakeShot(object sender, TicTacToe.Point e)
        {
            Model.ActiveBoard.SetCursor(e);
            UiVisualContext.Instance.GenerateKeyPress(Keys.Enter);
        }
    }
}