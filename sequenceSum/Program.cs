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
            var message = "Enter your exponential number. If it ends enter: \'=\' in new line";
            string input = "";

            Console.WriteLine(message);

            do
            {
                try
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
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unhandled exception " + e.Message);
                }
            } while (input != "=");
            Console.WriteLine(sum);

            Console.ReadLine();
        }
    }
}