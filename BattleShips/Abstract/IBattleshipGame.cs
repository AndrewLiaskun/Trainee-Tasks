// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Enums;
using BattleShips.Misc.Args;
using BattleShips.Models;

namespace BattleShips.Abstract
{
    public interface IBattleshipGame
    {
        event EventHandler<BattleShipsStateChangedEventArgs> StateChanged;

        event EventHandler<HistoryRecordsChangedEventArgs> HistoryRecordsChanged;

        IGameHistory GameHistory { get; }

        IBattleShipBoard ActiveBoard { get; }

        IPlayer User { get; }

        IPlayer Computer { get; }

        BattleShipsState State { get; }

        string GetAboutText();

        void Start();

        void StartNewGame();

        void CreatePlayer(string name);

        void LoadPlayer(string path);

        void Resume();

        void LoadGame(string path);

        void SaveGame(string path);

        void SwitchState(BattleShipsState state);
    }
}