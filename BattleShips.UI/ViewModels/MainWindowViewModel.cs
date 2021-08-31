// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Windows.Input;

using BattleShips.Abstract;
using BattleShips.Abstract.Visuals;
using BattleShips.Enums;
using BattleShips.Misc;
using BattleShips.Models;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.Commands;
using BattleShips.UI.Models;
using BattleShips.UI.Models.Visuals;

using TicTacToe;

using Point = TicTacToe.Point;
using static BattleShips.Resources.Serialization;
using System.Windows;
using BattleShips.UI.ViewModels.Players;

namespace BattleShips.UI.ViewModels
{
    public class MainWindowViewModel : BaseViewModel
    {
        private readonly IBattleshipGame _battleShipsGame;
        private IDialogService _dialogService;

        private IVisualContext _context;

        private ICommand _startGameCommand;
        private ICommand _randomShootCommand;
        private ICommand _upStepCommand;
        private ICommand _downStepCommand;
        private ICommand _leftStepCommand;
        private ICommand _rightStepCommand;
        private ICommand _enterStepCommand;
        private ICommand _changeDirectionCommand;
        private ICommand _goToMenuCommand;
        private ICommand _saveGaneCommand;
        private ICommand _loadGameCommand;
        private ICommand _exitCommand;
        private ICommand _profileCommand;

        public MainWindowViewModel()
        {
            _context = UiVisualContext.Instance;
            _dialogService = new DialogService();

            _battleShipsGame = new BattleshipsGame(_context, new PlayerBoardConfig(Point.Empty));
            _battleShipsGame.Start();

            Game = new BattleShipGameViewModel(_battleShipsGame);
            Profile = new ProfileViewModel(this);
        }

        public BattleShipGameViewModel Game { get; }

        public ProfileViewModel Profile { get; }

        public BattleShipsState CurrentPage => Game.Model.State;

        private void CreateGame()
        {
            _battleShipsGame.StartNewGame();
            RaisePropertyChanged(nameof(CurrentPage));
        }

        private void BackToMenu()
        {
            _context.GenerateKeyPress(Keys.Escape);
            RaisePropertyChanged(nameof(CurrentPage));
        }

        private void SaveGame()
        {
            _dialogService.ShowMessage(SavePath);
            if (_dialogService.SaveFileDialog())
            {
                _battleShipsGame.SaveGame(_dialogService.FilePath);
                _dialogService.ShowMessage(SuccessfulSave);
            }
        }

        private void LoadGame()
        {
            _dialogService.ShowMessage(LoadPath);
            if (_dialogService.OpenFileDialog())
            {
                _battleShipsGame.LoadGame(_dialogService.FilePath);
            }

            RaisePropertyChanged(nameof(CurrentPage));
        }

        private void GetProfile()
        {
            Game.Model.SwitchState(BattleShipsState.Profile);
            RaisePropertyChanged(nameof(CurrentPage));
        }

        #region Commands

        public ICommand ProfileCommand
        {
            get => _profileCommand ?? (_profileCommand = new RelayCommand(GetProfile));
        }

        public ICommand ExitCommand
        {
            get => _exitCommand ?? (_exitCommand = new RelayCommand(() => Application.Current.MainWindow.Close()));
        }

        public ICommand StartGame
        {
            get => _startGameCommand ?? (_startGameCommand = new RelayCommand(CreateGame,
                       () => _battleShipsGame.State != BattleShipsState.Game));
        }

        public ICommand RandomShootCommand
        {
            get => _randomShootCommand ?? (_randomShootCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.R)));
        }

        public ICommand UpStepCommand
        {
            get => _upStepCommand ?? (_upStepCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.Up)));
        }

        public ICommand SaveGameCommand
        {
            get => _saveGaneCommand ?? (_saveGaneCommand = new RelayCommand(SaveGame));
        }

        public ICommand LoadGameCommand
        {
            get => _loadGameCommand ?? (_loadGameCommand = new RelayCommand(LoadGame));
        }

        public ICommand DownStepCommand
        {
            get => _downStepCommand ?? (_downStepCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.Down)));
        }

        public ICommand LeftStepCommand
        {
            get => _leftStepCommand ?? (_leftStepCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.Left)));
        }

        public ICommand RightStepCommand
        {
            get => _rightStepCommand ?? (_rightStepCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.Right)));
        }

        public ICommand EnterCommand
        {
            get => _enterStepCommand ?? (_enterStepCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.Enter)));
        }

        public ICommand ChangeDirectionCommand
        {
            get => _changeDirectionCommand ?? (_changeDirectionCommand = new RelayCommand(() => _context.GenerateKeyPress(Keys.Q)));
        }

        public ICommand GoToMenuCommand
        {
            get => _goToMenuCommand ?? (_goToMenuCommand = new RelayCommand(BackToMenu));
        }

        #endregion Commands
    }
}