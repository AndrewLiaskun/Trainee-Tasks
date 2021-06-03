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
            var sum = ExponentialNumber.Zero;
            Console.WriteLine("Enter your expinentail number. If it ends enter: \'=\' in new line");
            string input = "";
            do
            {
                input = Console.ReadLine();
                switch (input)
                {
                    case "=":
                        break;

                    default:
                        sum += ExponentialNumber.Parse(input);
                        break;
                }
            } while (input != "=");
            Console.WriteLine(sum);
        }
    }
}