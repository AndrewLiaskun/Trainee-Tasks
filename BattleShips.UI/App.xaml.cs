// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using BattleShips.UI.ViewModels;
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
            // Let the base application do what it needs
            base.OnStartup(e);

            // Show the main window
            MainWindow = new MainWindow();
            MainWindow.DataContext = new MainWindowViewModel();

            MainWindow.Show();
        }
    }
}