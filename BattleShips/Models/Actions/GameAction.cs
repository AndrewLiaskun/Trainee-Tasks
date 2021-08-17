// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace BattleShips.Models
{
    public class GameAction
    {

        private Action<ActionContext> _handleAction;

        private Func<ActionContext, bool> _canHandleAction;

        public GameAction(Action<ActionContext> handle, Func<ActionContext, bool> canHandle)
        {
            _handleAction = handle ?? throw new ArgumentNullException(nameof(handle));
            _canHandleAction = canHandle ?? throw new ArgumentNullException(nameof(canHandle));
        }

        public void Handle(ActionContext parameter)
        {
            _handleAction(parameter);
        }

        public bool CanHandle(ActionContext parameter)
        {
            return _canHandleAction(parameter);
        }
    }
}