// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace Searcher
{
    public class MatchEventArgs : EventArgs
    {
        public MatchEventArgs(MatchType type, string value)
        {
            Type = type;
            Value = value;
        }

        public MatchType Type { get; }

        public string Value { get; }
    }
}