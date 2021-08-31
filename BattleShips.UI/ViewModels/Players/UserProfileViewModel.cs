// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.Commands;
using BattleShips.UI.Models.Visuals;
using BattleShips.Utils;

namespace BattleShips.UI.ViewModels.Players
{
    public class UserProfileViewModel : BaseViewModel, IModelProvider<IBattleshipGame>
    {
        private string _userName;

        public UserProfileViewModel(IBattleshipGame game, string name)
        {
            Model = game;
            _userName = name;
            SelectUserCommand = new RelayCommand(ClickExecute);
        }

        public event EventHandler<string> Clicked;

        public string UserName => _userName;

        public ICommand SelectUserCommand { get; }

        public IBattleshipGame Model { get; }

        public override string ToString() => _userName;

        private void SelectUser()
        {
            var path = UserManager.GetUserPath(UserName);
            Model.LoadPlayer(path);
        }

        private void ClickExecute()
        {
            Clicked?.Invoke(this, UserName);
            SelectUser();
            RefreshAllBindings();
        }
    }
}