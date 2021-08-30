// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static BattleShips.Resources.Serialization;

namespace BattleShips.Utils
{
    public class UserManager
    {
        public static string[] Load()
        {
            if (!Directory.Exists(UsersFolderPath))
            {
                Directory.CreateDirectory(UsersFolderPath);
            }

            return Directory.GetFiles(UsersFolderPath);
        }

        public static bool TryDelete(string path)
        {
            File.Delete(path);

            return !File.Exists(path);
        }
    }
}