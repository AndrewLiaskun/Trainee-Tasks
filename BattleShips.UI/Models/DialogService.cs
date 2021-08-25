// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using BattleShips.UI.Abstract;

using Microsoft.Win32;

namespace BattleShips.UI.Models
{
    public class DialogService : IDialogService
    {
        private string _filePath;

        public string FilePath => _filePath;

        public bool OpenFileDialog()
        {
            var open = new OpenFileDialog();
            if (open.ShowDialog() == true)
            {
                _filePath = open.FileName;
                return true;
            }
            return false;
        }

        public bool SaveFileDialog()
        {
            var save = new SaveFileDialog();
            if (save.ShowDialog() == true)
            {
                _filePath = save.FileName;
                return true;
            }
            return false;
        }

        public void ShowMessage(string message) => MessageBox.Show(message);
    }
}