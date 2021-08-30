// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.Commands;
using BattleShips.UI.Views;
using BattleShips.Utils;

using static BattleShips.Resources.Serialization;

namespace BattleShips.UI.ViewModels.Players
{
    public class ProfileViewModel : BaseViewModel, IModelProvider<IBattleshipGame>
    {
        private ICommand _createUserCommand;
        private ICommand _deleteUserCommand;
        private ICommand _addNameCommand;
        private ICommand _canselNameCommand;
        private ICommand _selectUserCommand;

        private int _pathStringCount = UsersFolderPath.Length;

        private string _userName;
        private ObservableCollection<string> _users;

        private NameCreator _nameCreater;

        public ProfileViewModel(IBattleshipGame game)
        {
            Model = game;
            _users = new ObservableCollection<string>();

            foreach (var item in UserManager.Load())
                _users.Add(SelectUser(item));
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

        private string SelectUser(string item)
        {
            var dotIndex = item.LastIndexOf('.');
            var name = item.Substring(_pathStringCount, dotIndex - _pathStringCount);
            return name;
        }

        private void DeleteUser()
        {
            MessageBox.Show(UserManager.TryDelete(UserName) ? SuccessfulDeleteUser : WrongDeleteUser);
            _users.Remove(UserName);
            RaisePropertyChanged(nameof(Users));
        }

        private void CreateUser()
        {
            _nameCreater = new NameCreator();
            _nameCreater.DataContext = this;
            _nameCreater.ShowDialog();

            _users.Add(UserName);
            Model.CreatePlayer(UserName);
        }

        private void CanselCreateName()
        {
            _userName = string.Empty;
            _nameCreater.Close();
        }

        private void SelectUser()
        {
            var path = UserManager.GetUserPath(UserName);
            Model.LoadPlayer(path);
            Model.SwitchState(BattleShipsState.Menu);
        }

        #region Commands

        public ICommand SelectUserCommand
        {
            get => _selectUserCommand ?? (_selectUserCommand = new RelayCommand(SelectUser));
        }

        public ICommand DeleteUserCommand
        {
            get => _deleteUserCommand ?? (_deleteUserCommand = new RelayCommand(DeleteUser));
        }

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