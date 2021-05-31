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
            sum.ParseProperties("12.4324235342623464326547657364658");
        }
    }
}