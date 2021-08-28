// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System.Runtime.Serialization;

using BattleShips.Enums;
using BattleShips.Misc;

using TicTacToe;

namespace BattleShips.Metadata
{
    [DataContract(Name = "battleship-ship-state", Namespace = "http://schemas.datacontract.org/2004/07/BattleShips")]
    public class ShipStateDto
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "type")]
        public ShipType Type { get; set; }

        [DataMember(Name = "is-frozen")]
        public bool IsFrozen { get; set; }

        [DataMember(Name = "start-position")]
        public Point Start { get; set; }

        [DataMember(Name = "end-position")]
        public Point End { get; set; }

        [DataMember(Name = "direction")]
        public ShipDirection Direction { get; set; }

        [DataMember(Name = "deck")]
        public int Deck { get; set; }

        [DataMember(Name = "health")]
        public int Health { get; set; }

        public static ShipStateDto FromShipState(ShipState shipState)
        {
            ShipStateDto current = new ShipStateDto();

            current.Id = shipState.ShipId;
            current.Deck = shipState.Decks;
            current.Direction = shipState.Direction;
            current.Start = shipState.Start;
            current.End = shipState.End;
            current.Health = (int)shipState.Health;
            current.IsFrozen = shipState.IsFrozen;
            current.Type = shipState.ShipKind;

            return current;
        }

        public ShipState GetState() => new ShipState(Id, Direction, Start, End, Type);
    }
}