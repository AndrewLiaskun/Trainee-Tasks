// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using BattleShips.UI.Views;

namespace BattleShips.UI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Current.MainWindow = new MainWindow();
            Current.MainWindow.Show();
        }
    }
}