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
            string inputNuber = Console.ReadLine();
            if (!inputNuber.Contains("="))
                inputNuber += "=";
            ExponentialNumber exponentialNumber = new ExponentialNumber(inputNuber);
            Console.WriteLine("{0:0.###,###,###,###,###,###,###,###,###,###,###,###,###}", exponentialNumber.getPow());
        }
    }
}