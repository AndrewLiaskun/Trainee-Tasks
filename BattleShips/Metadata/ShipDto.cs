// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using BattleShips.Abstract;
using BattleShips.Enums;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Metadata
{
    [DataContract(Name = "battleship-ship", Namespace = "http://schemas.datacontract.org/2004/07/BattleShips")]
    public class ShipDto
    {
        [DataMember(Name = "id")]
        public int ShipId { get; set; }

        [DataMember(Name = "type")]
        public ShipType Type { get; set; }

        [DataMember(Name = "is-frozen")]
        public bool IsFrozen { get; set; }

        [DataMember(Name = "start-position")]
        public Point Start { get; set; }

        [DataMember(Name = "end-position")]
        public Point End { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "direction")]
        public ShipDirection Direction { get; set; }

        [DataMember(Name = "deck")]
        public int Deck { get; set; }

        [DataMember(Name = "health")]
        public int Health { get; set; }

        [DataMember(Name = "is-valid")]
        public bool IsValid { get; set; }

        public static ShipDto FromShip(IShip ship)
        {
            ShipDto current = new ShipDto();

            current.Deck = ship.Deck;
            current.Direction = ship.Direction;
            current.Start = ship.Start;
            current.End = ship.End;
            current.Health = ship.Health;
            current.IsFrozen = ship.IsFrozen;
            current.IsValid = ship.IsValid;
            current.Name = ship.Name;
            current.Type = ship.ShipKind;

            return current;
        }

        public static ShipDto FromShipState(ShipState shipState)
        {
            ShipDto current = new ShipDto();

            current.ShipId = shipState.ShipId;
            current.Deck = shipState.Decks;
            current.Direction = shipState.Direction;
            current.Start = shipState.Start;
            current.End = shipState.End;
            current.Health = shipState.Health ?? 0;
            current.IsFrozen = shipState.IsFrozen;
            current.Type = shipState.ShipKind;

            return current;
        }

        public ShipState GetState() => new ShipState(ShipId, Direction, Start, End, Type);
    }
}