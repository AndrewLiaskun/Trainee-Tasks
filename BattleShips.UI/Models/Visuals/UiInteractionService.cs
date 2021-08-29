// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Windows;

using BattleShips.Abstract.Visuals;
using BattleShips.Models.Visuals;

namespace BattleShips.UI.Models.Visuals
{
    internal class UiInteractionService : IUserInteractionService
    {
        private IVisualContext _shell;

        public UiInteractionService(IVisualContext shell) => _shell = shell;

        public bool AskYesNoQuestion(string question, QuestionParams parameter)
            => MessageBox.Show(question, "Question", MessageBoxButton.YesNo) == MessageBoxResult.Yes;
    }
}