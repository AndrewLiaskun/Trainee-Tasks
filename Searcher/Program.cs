// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;

namespace Searcher
{
    internal class Program
    {

        private static void Main(string[] args)
        {
            Console.WriteLine("Enter file or Url path:");
            var path = Console.ReadLine();

            var uriSearcher = TextSearcher.CreateSearcher(path);
            uriSearcher.MatchFound += TextSearcher_MatchFound;
            uriSearcher.Analyze();
            Console.ReadLine();
        }

        private static void TextSearcher_MatchFound(object sender, MatchEventArgs e)
        {
            Console.WriteLine($"\n{e.Type}:\t {e.Value}");
        }
    }
}