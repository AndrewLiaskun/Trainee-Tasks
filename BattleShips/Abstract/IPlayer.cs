// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

using BattleShips.Abstract.Ships;
using BattleShips.Enums;
using BattleShips.Metadata;
using BattleShips.Models;

using TicTacToe;

namespace BattleShips.Abstract
{
    public interface IPlayer
    {
        event EventHandler ResetOcurred;

        event EventHandler<CellChangedEventArgs> CellCollectionChanged;

        IBattleShipBoard Board { get; }

        IBattleShipBoard PolygonBoard { get; }

        IShipFactory ShipFactory { get; }

        PlayerType Type { get; }

        string Name { get; }

        void ShowBoards();

        void MakeShot(Point point, bool isEmpty, bool isAlive);

        IShip CreateShip(Point point);

        void MakeMove(Point point);

        void FillShips();

        void Load(PlayerDto player);

        void Reset();
    }
}