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

            ExponentialNumber sum = ExponentialNumber.Zero;
            sum.Parse("21965868598e+13");
            Console.Read();
        }
    }
}