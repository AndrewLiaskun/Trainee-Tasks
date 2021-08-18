// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Enums;

using TicTacToe;

using static BattleShips.Misc.GameConstants;

namespace BattleShips.Models
{
    public class GameActionHandler
    {
        private IGameMenu _gameMenu;
        private List<GameAction> _action;
        private ShootAlgorithm _randomShoot;

        public GameActionHandler()
        {
            RegiserActions();
        }

        public void HandleAction(ActionContext args)
        {
            foreach (var item in _action)
            {
            }
        }

        private void RegiserActions()
        {
            _action.Add(new GameAction(BackToMenu, CanBackToMenu));
            _action.Add(new GameAction(SetGamePoint, CanSetGamePoint));
            _action.Add(new GameAction(HandleShipCreation, CanHandleShipCreation));
            _action.Add(new GameAction(RandomPlaceShips, CanHandleShipCreation));
            _action.Add(new GameAction(ChangeDirection, CanHandleShipCreation));
            _action.Add(new GameAction(Shoot, CanShoot));
            _action.Add(new GameAction(RandomShoot, CanRandomShoot));
            _action.Add(new GameAction(Debug, CanDebug));
        }

        private bool CanBackToMenu(ActionContext args)
        {
            if (args.Key == Keys.Escape && args.Game.State == BattleShipsState.Game)
                return true;

            return false;
        }

        private bool CanSetGamePoint(ActionContext args)
        {
            if ((args.Key == Keys.Left || args.Key == Keys.Right ||
                args.Key == Keys.Up || args.Key == Keys.Down) &&
                args.Game.State == BattleShipsState.Game ||
                args.Game.State == BattleShipsState.CreateShip)
                return true;

            return false;
        }

        private bool CanHandleShipCreation(ActionContext args)
        {
            if (args.Game.State == BattleShipsState.CreateShip)
                return true;

            return false;
        }

        private bool CanShoot(ActionContext args)
        {
            if (args.Game.State == BattleShipsState.Game &&
                args.Key == Keys.Enter)
                return true;

            return false;
        }

        private bool CanRandomShoot(ActionContext args)
        {
            if (args.Game.State == BattleShipsState.Game &&
                args.Key == Keys.R)
                return true;

            return false;
        }

        private bool CanDebug(ActionContext args)
        {
            if (args.Game.State == BattleShipsState.Game &&
                args.Key == Keys.D)
                return true;

            return false;
        }

        private void BackToMenu(ActionContext args)
        {
            args.Game.SwitchState(BattleShipsState.Menu);

            _gameMenu.Print();
        }

        private void SetGamePoint(ActionContext args)
        {
            var step = BoardMeasures.Step;
            var newPoint = args.Game.User.Board.CurrentPosition;

            if (args.Key == Keys.Up || args.Key == Keys.Down)
                newPoint.Y += args.Key == Keys.Up ? -step : step;

            if (args.Key == Keys.Left || args.Key == Keys.Right)
                newPoint.X += args.Key == Keys.Left ? -step : step;

            if (IsInValidRange(newPoint.X)
                && IsInValidRange(newPoint.Y))
                _currentPosition = newPoint;
        }

        private void HandleShipCreation(ActionContext args)
        {
            if (e.KeyCode == Keys.Enter && _tempShip != null)
            {
                if (!_tempShip.IsValid)
                    return;

                _tempShip?.Freeze();
                _tempShip = null;
            }

            if (_tempShip is null)
            {
                _tempShip = _player.CreateShip(_player.Board.CurrentPosition);
                if (_tempShip != null)
                {
                    _tempShip.ChangeDirection(_shipDirection);
                    ActiveBoard.MoveShip(_currentPosition, _tempShip, _shipDirection);
                }

                if (_tempShip is null)
                {
                    _currentPosition = new Point();
                    SwitchState(BattleShipsState.Game);
                }
            }
            else
                ActiveBoard.MoveShip(_currentPosition, _tempShip, _shipDirection);

            ActiveBoard.SetCursor(_currentPosition);
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

            _shell.Output.PrintText(Resources.PlaceShipInformation.Text, new Point(30, 25), true);
        }

        private void ChangeDirection(ActionContext args)
        {
            if (e.KeyCode == Keys.Q)
            {
                if (_shipDirection == ShipDirection.Horizontal)
                    _shipDirection = ShipDirection.Vertical;
                else
                    _shipDirection = ShipDirection.Horizontal;
            }
        }

        private void Shoot(ActionContext args)
        {
            var isAlive = true;

            _ai.Board.ProcessShot(_currentPosition);
            foreach (var item in _ai.Board.Ships)
            {
                if (item.Includes(_currentPosition) && !item.IsAlive)
                {
                    isAlive = false;
                    break;
                }
            }

            if (IsValidCell())
            {
                _player.MakeShot(_currentPosition, IsEmptyCell(), isAlive);

                if (IsEmptyCell())
                {
                    _ai.Board.SetCellValue(_currentPosition.X, _currentPosition.Y, Miss);

                    _randomShoot.MakeShoot(_ai, _player);
                }
                _player.ShowBoards();
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
            _shell.Output.Reset();
            _ai.ShowBoards();
        }
    }
}