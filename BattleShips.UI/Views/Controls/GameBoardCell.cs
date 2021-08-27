// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using BattleShips.UI.ViewModels.Board;

namespace BattleShips.UI.Views.Controls
{
    /// <summary>
    /// Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
    ///
    /// Step 1a) Using this custom control in a XAML file that exists in the current project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:BattleShips.UI.Views.Controls"
    ///
    ///
    /// Step 1b) Using this custom control in a XAML file that exists in a different project.
    /// Add this XmlNamespace attribute to the root element of the markup file where it is
    /// to be used:
    ///
    ///     xmlns:MyNamespace="clr-namespace:BattleShips.UI.Views.Controls;assembly=BattleShips.UI.Views.Controls"
    ///
    /// You will also need to add a project reference from the project where the XAML file lives
    /// to this project and Rebuild to avoid compilation errors:
    ///
    ///     Right click on the target project in the Solution Explorer and
    ///     "Add Reference"->"Projects"->[Browse to and select this project]
    ///
    ///
    /// Step 2)
    /// Go ahead and use your control in the XAML file.
    ///
    ///     <MyNamespace:ControlTemplates/>
    ///
    /// </summary>
    public class GameBoardCell : ContentControl
    {
        public static DependencyProperty ShowRowsProperty = DependencyProperty.Register("ShowRows", typeof(bool), typeof(GameBoardCell), new PropertyMetadata());

        static GameBoardCell()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(GameBoardCell), new FrameworkPropertyMetadata(typeof(GameBoardCell)));
        }

        public bool ShowRows
        {
            get => (bool)GetValue(ShowRowsProperty);
            set => SetValue(ShowRowsProperty, value);
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            var showRowsHandleBinding = new Binding
            {
                Path = new PropertyPath(nameof(ShowRows)),
                Source = this,
                Converter = new BooleanToVisibilityConverter()
            };
        }
    }
}