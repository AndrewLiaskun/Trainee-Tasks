// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

using TicTacToe.Abstract;

namespace TicTacToe
{
    [DataContract]
    public class GameInfo
    {
        [DataMember]
        public BoardCell[] Board { get; set; }
    }
}