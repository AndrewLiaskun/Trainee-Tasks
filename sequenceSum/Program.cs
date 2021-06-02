// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sequenceSum
{
    internal class Program
    {

        private static void Main(string[] args)
        {

            ExponentialNumber first = ExponentialNumber.Parse("23e2");
            ExponentialNumber second = ExponentialNumber.Parse("2.3e2");
            var sum = second + first;
            Console.WriteLine();
        }
    }
}