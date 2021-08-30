// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

using BattleShips.Enums;
using BattleShips.UI.ViewModels;
using BattleShips.UI.Views;
using BattleShips.UI.Views.Controls;

namespace BattleShips.UI.ValueConverters
{
    public class GamePageValueConverter : BaseValueConverter<GamePageValueConverter>, IMultiValueConverter
    {

        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ctx = parameter as MainWindowViewModel;

            switch ((BattleShipsState)value)
            {
                case BattleShipsState.Game:
                case BattleShipsState.CreateShip:
                    return new PlayerBoards
                    {
                        DataContext = ctx?.Game
                    };

                case BattleShipsState.Menu:
                    return new MenuPage
                    {
                        DataContext = ctx
                    };

                case BattleShipsState.Profile:
                    return new ProfilePage()
                    {
                        DataContext = ctx?.Profile
                    };

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
            => Convert(values.ElementAtOrDefault(0), targetType, values.ElementAtOrDefault(1), culture);

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}