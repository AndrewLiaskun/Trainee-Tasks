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

            ExponentialNumber sum = ExponentialNumber.Parse("24e3");
            ExponentialNumber kek = ExponentialNumber.Parse("422436e12");
            var simple = kek.Add(sum);
            Console.Read();
        }
    }
}