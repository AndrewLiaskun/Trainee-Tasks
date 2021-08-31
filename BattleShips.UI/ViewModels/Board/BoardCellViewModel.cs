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
        public static readonly ImageBrush EmptyBrush = new ImageBrush(new BitmapImage(new Uri("pack://application:,,,/Resources/Sea.png")));
        private static ResourceDictionary _shipsImage;

        private ImageBrush _image;

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

        public static ResourceDictionary ShipImages => _shipsImage;

        public BoardCell Model { get; }

        public ICommand ClickCommand { get; }

        public ImageBrush Image
        {
            get
            {
                if (IsShip)
                    return _image;
                if (IsDamagedShip)
                    return _image;

                return EmptyBrush;
            }
            set
            {
                if (value != null || _image != value)
                    _image = value;
            }
        }

        public bool IsNewRow => Model.Point.X == 0;

        public bool IsNewColumn => Model.Point.Y == 0;

        public bool IsShip => Model.Value == GameConstants.Ship;

        public bool IsDamagedShip => Model.Value == GameConstants.Got;

        public bool IsMiss => Model.Value == GameConstants.Miss;

        public int RowNumber => Model.Point.Y + 1;

        public char ColumnLetter => (char)(Model.Point.X + 65);

        public char Value => Model.Value;

        private void ClickExecute()
        {
            Clicked?.Invoke(this, Model.Point);

            RefreshAllBindings();
        }
    }
}