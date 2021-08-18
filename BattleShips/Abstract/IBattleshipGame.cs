﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Enums;
using BattleShips.Misc.Args;

namespace BattleShips.Abstract
{
    public interface IBattleshipGame
    {
        event EventHandler<BattleShipsStateChangedEventArgs> StateChanged;

        IBattleShipBoard ActiveBoard { get; }

        IPlayer User { get; }

        IPlayer Computer { get; }

        BattleShipsState State { get; }

        string GetAboutText();

        void Start();

        void StartNewGame();

        void Resume();

        void LoadGame();

        void SaveGame();

        void SwitchState(BattleShipsState state);
    }
}