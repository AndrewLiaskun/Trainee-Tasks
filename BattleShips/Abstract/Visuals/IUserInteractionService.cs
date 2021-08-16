// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleShips.Abstract.Visuals
{
    public interface IUserInteractionService
    {
        bool AskYesNoQuestion(string question);
    }
}