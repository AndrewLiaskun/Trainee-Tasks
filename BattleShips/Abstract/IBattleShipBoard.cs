﻿// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;

using BattleShips.Abstract.Ships;
using BattleShips.Enums;
using BattleShips.Metadata;
using BattleShips.Misc;
using BattleShips.Models;

using TicTacToe;
using TicTacToe.Abstract;

namespace BattleShips.Abstract
{
    public interface IBattleShipBoard : IBoard
    {
        event EventHandler<BoardShipsChangedEventArgs> ShipsCollectionChanged;

        event EventHandler<ShipChangedEventArgs> ShipChanged;

        event EventHandler<BoardCell> CellChanged;

        Point Position { get; }

        Point ZeroCellPosition { get; }

        IReadOnlyList<IShip> Ships { get; }

        int AliveShips { get; }

        Point CurrentPosition { get; }

        BoardCell GetCellValue(Point point);

        void SetCellValue(Point point, char value);

        IShip GetShipAtOrDefault(Point point);

        void ChangeOrAddShip(Point point, IShip ship);

        bool IsEmptyCell(Point point);

        void Show();

        void ProcessShot(Point point);

        void SelectCell(Point point);

        void Load(BoardDto board, IShipFactory factory);

        void AddShip(IShip ship);

        void MoveShip(Point point, IShip ship, ShipDirection direction);

        void SetCursor(Point position);

        bool ValidateShip(Point point, IShip ship);

        void Reset();
    }
}