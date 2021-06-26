// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Searcher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            TextSearcher textSearcher = new TextSearcher(@"E:\c# - Read a HTML file into a string variable in memory - Stack Overflow.html");
            textSearcher.MatchFound += TextSearcher_MatchFound;
            Console.ReadLine();
        }

        private static void TextSearcher_MatchFound(object sender, MatchEventArgs e)
        {
            Console.WriteLine(e.Value);
        }
    }
}