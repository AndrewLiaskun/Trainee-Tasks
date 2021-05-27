// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sequenceSum
{
    public class exponentialNumber
    {
        private const double EXPONENTIAL = 10;
        private float mainNumber;
        private float powNumber;
        private double resultNumber;
        private string convertNumber;

        public exponentialNumber(string _convertNumber)
        {
            convertNumber = _convertNumber;
        }

        public double getPow()
        {
            mainNumber = Convert.ToSingle(convertNumber.Substring(0, convertNumber.IndexOf("e")));
            powNumber = Convert.ToSingle(convertNumber.Substring(convertNumber.IndexOf("e") + 1, Convert.ToInt16(convertNumber.IndexOf("=")) - Convert.ToInt16(convertNumber.IndexOf("e") + 1)));
            resultNumber = mainNumber * Math.Pow(EXPONENTIAL, Convert.ToDouble(powNumber));
            return resultNumber;
        }
    }
}