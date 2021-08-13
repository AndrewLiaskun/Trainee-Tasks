// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Enums;
using BattleShips.UI.Views;

namespace BattleShips.UI.ValueConverters
{
    public class GamePageValueConverter : BaseValueConverter<GamePageValueConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            switch ((BattleShipsState)value)
            {
                case BattleShipsState.Game:
                    return new PlayerBoards();

                default:
                    Debugger.Break();
                    return null;
            }
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}