﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Collections.Generic;

using BattleShips.Enums;

using TicTacToe;
using TicTacToe.Abstract;

namespace BattleShips.Abstract
{
    public interface IBattleShipBoard : IBoard
    {
        Point Position { get; }

        Point ZeroCellPosition { get; }

        IReadOnlyList<IShip> Ships { get; }

        int ShipsCount { get; }

        void ChangeOrAddShip(Point point, IShip ship);

        void Draw();

        void ProcessShot(Point point);

        void DrawSelectedCell(Point point);

        void AddShip(IShip ship);

        void MoveShip(Point point, IShip ship, ShipDirection direction);

        void SetCursor(Point position);

        bool ValidateShip(Point point, IShip ship);
    }
}