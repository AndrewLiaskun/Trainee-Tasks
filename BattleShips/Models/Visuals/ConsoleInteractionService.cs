// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract.Visuals;
using BattleShips.Enums;

using TicTacToe;

using static BattleShips.Resources.Questions;

namespace BattleShips.Models.Visuals
{
    internal class ConsoleInteractionService : IUserInteractionService
    {
        private IVisualContext _shell;

        public ConsoleInteractionService(IVisualContext shell) => _shell = shell;

        public bool AskYesNoQuestion(string question, QuestionParams parameter)
        {
            var comparison = StringComparison.OrdinalIgnoreCase;

            string answer = string.Empty;

            _shell.Output.SetForegroundColor(parameter.ForegroundColor);

            do
            {
                _shell.Output.PrintText(question, Point.Empty, true);

                _shell.Output.PrintText(string.Empty, new Point(0, 2), true);
                answer = _shell.Output.ReadText().Trim();
                _shell.Output.Reset();
            }
            while (!answer.Equals(PositiveAnswer, comparison) && !answer.Equals(NegativeAnswer, comparison));

            _shell.Output.ResetColor();
            return answer.Equals(PositiveAnswer, comparison);
        }
    }
}