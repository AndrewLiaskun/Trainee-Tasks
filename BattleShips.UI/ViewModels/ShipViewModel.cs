// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.UI.Abstract;
using BattleShips.UI.Basic;

namespace BattleShips.UI.ViewModels
{
    public class ShipViewModel : BaseViewModel, IModelProvider<IShip>
    {
        public ShipViewModel(IShip ship)
        {
            Model = ship;
            Model.ShipChanged += OnShipChanged;
        }

        public IShip Model { get; }

        private void OnShipChanged(object sender, Misc.ShipChangedEventArgs e)
        {
            RefreshAllBindings();
        }
    }
}