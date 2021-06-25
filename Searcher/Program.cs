// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Searcher
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Searcher searcher = new Searcher();
            searcher.GetUrl("https://stackoverflow.com/questions/708210/how-to-use-http-get-request-in-c-sharp-with-ssl-protocol-violation");
        }
    }
}