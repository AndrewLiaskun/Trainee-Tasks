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
using BattleShips.UI.Models.Visuals;
using BattleShips.UI.Views;
using BattleShips.Utils;

using static BattleShips.Resources.Serialization;

namespace BattleShips.UI.ViewModels.Players
{
    public class ProfileViewModel : BaseViewModel, IModelProvider<IBattleshipGame>
    {
        private static int _pathStringCount = UsersFolderPath.Length;
        private ICommand _createUserCommand;
        private ICommand _deleteUserCommand;
        private ICommand _addNameCommand;
        private ICommand _canselNameCommand;

        private string _userName;
        private string _currentUser;
        private ObservableCollection<UserProfileViewModel> _users;
        private NameCreator _nameCreater;

        public ProfileViewModel(MainWindowViewModel game)
        {
            window = game;
            Model = game.Game.Model;
            _users = new ObservableCollection<UserProfileViewModel>(GetUsers(Model));
            foreach (var item in _users)
            {
                item.Clicked += Item_Clicked;
            }
        }

        public string CurrentUser => _currentUser;

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

        public IEnumerable<UserProfileViewModel> Users => _users;

        public IBattleshipGame Model { get; }

        private MainWindowViewModel window { get; }

        private static string SelectUser(string item)
        {
            var dotIndex = item.LastIndexOf('.');
            var name = item.Substring(_pathStringCount, dotIndex - _pathStringCount);
            return name;
        }

        private static IEnumerable<UserProfileViewModel> GetUsers(IBattleshipGame game) =>
                            UserManager.Load().Select(x => new UserProfileViewModel(game, SelectUser(x)));

        private void Item_Clicked(object sender, string e)
        {
            var item = Users.First(x => x.UserName == e);
            _currentUser = e;
            RefreshAllBindings();
        }

        private void DeleteUser()
        {
            if (string.IsNullOrEmpty(UserName)) return;

            MessageBox.Show(UserManager.TryDelete(UserName) ? SuccessfulDeleteUser : WrongDeleteUser);

            _users.Remove(_users.First(x => x.UserName == UserName));
            RaisePropertyChanged(nameof(Users));
        }

        private void CreateUser()
        {
            _nameCreater = new NameCreator();
            _nameCreater.DataContext = this;
            _nameCreater.ShowDialog();

            var user = new UserProfileViewModel(Model, UserName);

            _users.Add(user);

            Model.CreatePlayer(UserName);
        }

        private void CanselCreateName()
        {
            _userName = string.Empty;
            _nameCreater.Close();
        }

        #region Commands

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