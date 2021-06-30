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

            var kek = TextSearcher.CreateSearcher(path);
            kek.MatchFound += TextSearcher_MatchFound;
            kek.Analyze();
            Console.ReadLine();
        }

        private static void TextSearcher_MatchFound(object sender, MatchEventArgs e)
        {
            Console.WriteLine($"\n{e.Type}:\t {e.Value}");
        }
    }
}