// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;

using TicTacToe;

using static BattleShips.Misc.GameConstants;
using static BattleShips.Misc.GameConstants.BoardMeasures;

namespace BattleShips.Models
{
    public class GameActionHandler
    {

        private List<GameAction> _actions;
        private ShootAlgorithm _randomShoot;
        private IShip _tempShip;
        private ShipDirection _shipDirection = ShipDirection.Horizontal;

        public GameActionHandler()
        {
            _randomShoot = new ShootAlgorithm();
            _actions = new List<GameAction>();
            RegiserActions();
        }

        public void HandleAction(ActionContext args)
        {
            foreach (var item in _actions)
            {
                if (item.CanHandle(args))
                {
                    item.Handle(args);
                }
            }
        }

        private void RegiserActions()
        {
            _actions.Add(new GameAction(HandleMenu, CanHandleMenu));
            _actions.Add(new GameAction(BackToMenu, CanBackToMenu));
            _actions.Add(new GameAction(SetGamePoint, CanSetGamePoint));
            _actions.Add(new GameAction(HandleShipCreation, CanHandleShipCreation));
            _actions.Add(new GameAction(RandomPlaceShips, (x) => x.IsRandomPlacement && x.Game.State == BattleShipsState.CreateShip));
            _actions.Add(new GameAction(ChangeDirection, CanHandleShipCreation));
            _actions.Add(new GameAction(Shoot, CanShoot));
            _actions.Add(new GameAction(RandomShoot, CanRandomShoot));
            _actions.Add(new GameAction(Debug, CanDebug));
        }

        private void HandleMenu(ActionContext args)
        {
            args.GameMenu.HandleKey(args.Key);
        }

        private bool CanHandleMenu(ActionContext args) => args.CurrentState == BattleShipsState.Menu;

        private bool CanBackToMenu(ActionContext args)
        {
            if (args.Key == Keys.Escape)
                return true;

            return false;
        }

        private bool CanSetGamePoint(ActionContext args)
        {
            if ((args.Key == Keys.Left || args.Key == Keys.Right ||
                args.Key == Keys.Up || args.Key == Keys.Down) &&
                args.CurrentState == BattleShipsState.Game ||
                args.CurrentState == BattleShipsState.CreateShip)
                return true;

            return false;
        }

        private bool CanHandleShipCreation(ActionContext args)
        {
            if (args.CurrentState == BattleShipsState.CreateShip && !args.IsRandomPlacement)
                return true;

            return false;
        }

        private bool CanShoot(ActionContext args)
        {
            if (args.CurrentState == BattleShipsState.Game &&
                args.Key == Keys.Enter)
                return true;

            return false;
        }

        private bool CanRandomShoot(ActionContext args)
        {
            if (args.CurrentState == BattleShipsState.Game &&
                args.Key == Keys.R)
                return true;

            return false;
        }

        private bool CanDebug(ActionContext args)
        {
            if (args.CurrentState == BattleShipsState.Game &&
                args.Key == Keys.D)
                return true;

            return false;
        }

        private void BackToMenu(ActionContext args)
        {
            args.Game.SwitchState(BattleShipsState.Menu);

            args.GameMenu.Print();
        }

        private void SetGamePoint(ActionContext args)
        {

            var newPoint = args.ActiveBoardPosition;

            var step = BoardMeasures.Step;

            if (args.Key == Keys.Up || args.Key == Keys.Down)
                newPoint.Y += args.Key == Keys.Up ? -step : step;

            if (args.Key == Keys.Left || args.Key == Keys.Right)
                newPoint.X += args.Key == Keys.Left ? -step : step;

            if (IsInValidRange(newPoint.X)
                && IsInValidRange(newPoint.Y))
                args.ActiveBoard.SetCursor(newPoint);
            if (args.CurrentState == BattleShipsState.Game)
                args.Player.MakeMove(args.ActiveBoard.CurrentPosition);
        }

        private void HandleShipCreation(ActionContext args)
        {

            if (args.Key == Keys.Enter && _tempShip != null)
            {
                if (!_tempShip.IsValid)
                    return;

                _tempShip?.Freeze();
                _tempShip = null;
            }

            if (_tempShip is null)
            {
                _tempShip = args.Player.CreateShip(args.ActiveBoardPosition);
                if (_tempShip != null)
                {
                    _tempShip.ChangeDirection(_shipDirection);
                    args.ActiveBoard.MoveShip(args.ActiveBoardPosition, _tempShip, _shipDirection);
                }

                if (_tempShip is null)
                {
                    args.ActiveBoard.SetCursor(Point.Empty);
                    args.Game.SwitchState(BattleShipsState.Game);
                }
            }
            else
                args.ActiveBoard.MoveShip(args.ActiveBoardPosition, _tempShip, _shipDirection);

            args.ActiveBoard.SetCursor(args.ActiveBoardPosition);
        }

        private void RandomPlaceShips(ActionContext args)
        {
            var player = args.Game.User;
            if (args.Key == Keys.Enter && player.Board.Ships.Count != 0)
            {
                args.Game.SwitchState(BattleShipsState.Game);
                return;
            }

            player.Reset();

            player.FillShips();
            player.ShowBoards();

            //_shell.Output.PrintText(Resources.PlaceShipInformation.Text, new Point(30, 25), true);
        }

        private void ChangeDirection(ActionContext args)
        {
            if (args.Key == Keys.Q)
            {
                if (_shipDirection == ShipDirection.Horizontal)
                    _shipDirection = ShipDirection.Vertical;
                else
                    _shipDirection = ShipDirection.Horizontal;
            }
        }

        private bool IsValidCell(ActionContext args)
        {
            var board = args.Ai.Board;
            var point = args.ActiveBoardPosition;
            var isEmpty = board.IsEmptyCell(point);
            var isShip = board.GetCellValue(point).Value == Ship;
            var isGot = board.GetCellValue(point).Value == Got;
            var isMiss = board.GetCellValue(point).Value == Miss;

            return isEmpty || isShip && !isGot && !isMiss;
        }

        /// <summary>
        /// Check if the cell is empty
        /// </summary>
        /// <returns></returns>
        private bool IsEmptyCell(ActionContext args) => args.Ai.Board.IsEmptyCell(args.ActiveBoardPosition);

        private void Shoot(ActionContext args)
        {
            var aiBoard = args.Ai.Board;
            var isAlive = true;

            aiBoard.ProcessShot(args.ActiveBoardPosition);
            foreach (var item in aiBoard.Ships)
            {
                if (item.Includes(args.ActiveBoardPosition) && !item.IsAlive)
                {
                    isAlive = false;
                    break;
                }
            }

            if (IsValidCell(args))
            {
                args.Player.MakeShot(args.ActiveBoardPosition, IsEmptyCell(args), isAlive);

                if (IsEmptyCell(args))
                {
                    aiBoard.SetCellValue(args.ActiveBoardPosition, Miss);

                    _randomShoot.MakeShoot(args.Ai, args.Player);
                }
                args.Player.ShowBoards();
            }
        }

        private void RandomShoot(ActionContext args)
        {
            var player = args.Game.User;
            var ai = args.Game.Computer;
            _randomShoot.MakeShoot(player, ai);
            _randomShoot.MakeShoot(ai, player);
            player.ShowBoards();
        }

        private void Debug(ActionContext args)
        {
            //_shell.Output.Reset();
            //_ai.ShowBoards();
        }
    }
}