// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.Commands;
using BattleShips.UI.Views;
using BattleShips.Utils;

namespace BattleShips.UI.ViewModels.Players
{
    public class ProfileViewModel : BaseViewModel, IModelProvider<IBattleshipGame>
    {
        private ICommand _createUserCommand;
        private ICommand _deleteUserCommand;
        private ICommand _addNameCommand;
        private ICommand _canselNameCommand;

        private string _userName;
        private ObservableCollection<string> _users;

        private NameCreater _nameCreater;

        public ProfileViewModel(IBattleshipGame game)
        {
            Model = game;

            foreach (var item in UserManager.Load())
                _users.Add(item);
            _nameCreater = new NameCreater();
        }

        public IReadOnlyList<string> Users => _users;

        public string UserName
        {
            get => _userName;
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;

                _userName = value;
            }
        }

        public IBattleshipGame Model { get; }

        private void CreateUser()
        {

            _nameCreater.DataContext = this;
            _nameCreater.ShowDialog();

            Model.CreatePlayer(UserName);
        }

        private void CanselCreateName()
        {
            _userName = string.Empty;
            _nameCreater.Close();
        }

        #region Commands

        public ICommand AddNameCommand
        {
            get => _addNameCommand ?? (_addNameCommand = new RelayCommand(() => _nameCreater.Close()));
        }

        public ICommand CanselNameCommand
        {
            get => _canselNameCommand ?? (_canselNameCommand = new RelayCommand(CanselCreateName));
        }

        public ICommand CreateUserCommand
        {
            get => _createUserCommand ?? (_createUserCommand = new RelayCommand(CreateUser));
        }

        #endregion Commands
    }
}