// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

using BattleShips.Misc;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;
using BattleShips.UI.Commands;

using TicTacToe;

using Point = TicTacToe.Point;

namespace BattleShips.UI.ViewModels.Board
{
    public class BoardCellViewModel : BaseViewModel, IModelProvider<BoardCell>
    {
        private static ResourceDictionary _shipsImage;

        static BoardCellViewModel()
        {
            var path = "pack://application:,,,/Styles/ShipsImages.xaml";

            _shipsImage = new ResourceDictionary();
            _shipsImage.Source = new Uri(path, UriKind.RelativeOrAbsolute);
        }

        public BoardCellViewModel(BoardCell boardCell)
        {
            Model = boardCell;
            ClickCommand = new RelayCommand(ClickExecute, () => Value == GameConstants.Empty);
        }

        public event EventHandler<Point> Clicked;

        public BoardCell Model { get; }

        public ICommand ClickCommand { get; }

        public ImageBrush Image
        {

            get
            {
                if (IsShip)
                    return _shipsImage["jej3"] as ImageBrush;
                if (IsGot)
                    return _shipsImage["jej"] as ImageBrush;
                if (IsMiss)
                    return _shipsImage["jej2"] as ImageBrush;

                return new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/SaveGame.png")));
            }
        }

        public bool IsShip => Model.Value == GameConstants.Ship;

        public bool IsGot => Model.Value == GameConstants.Got;

        public bool IsMiss => Model.Value == GameConstants.Miss;

        public char Value => Model.Value;

        private void ClickExecute()
        {
            Clicked?.Invoke(this, Model.Point);
        }
    }
}