﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract.Visuals;

namespace BattleShips.UI.Models.Visuals
{
    internal class UiInteractionService : IUserInteractionService
    {
        private IVisualContext _shell;

        public UiInteractionService(IVisualContext shell) => _shell = shell;

        public bool AskYesNoQuestion(string question)
        {
            return true;
        }
    }
}