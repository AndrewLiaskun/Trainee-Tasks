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
            Console.WriteLine("Enter file or Url path:");
            var path = Console.ReadLine();

            TextSearcher textSearcher = new TextSearcher();
            textSearcher = TextSearcher.CreateSearcher(path);
            textSearcher.MatchFound += TextSearcher_MatchFound;
            textSearcher.Analyze();
            Console.ReadLine();
        }

        private static void TextSearcher_MatchFound(object sender, MatchEventArgs e)
        {
            Console.WriteLine();
            Console.WriteLine($"{e.Type}:\t {e.Value}");
        }
    }
}