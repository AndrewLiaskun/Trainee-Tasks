// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Enums;

namespace BattleShips.Abstract
{
    public interface IBattleshipGame
    {
        string GetAboutText();

        void Start();

        void SwitchState(BattleShipsState state);
    }
}