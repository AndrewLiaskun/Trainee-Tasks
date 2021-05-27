// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sequenceSum
{
    public class ExponentialNumber
    {
        private const double EXPONENTIAL = 10;
        private float _mainNumber;
        private float _powNumber;
        private double _resultNumber;
        private string _convertNumber;

        public ExponentialNumber(string convertNumber)
        {
            _convertNumber = convertNumber;
        }

        public double getPow()
        {
            _mainNumber = Convert.ToSingle(_convertNumber.Substring(0, _convertNumber.IndexOf("e")));
            _powNumber = Convert.ToSingle(_convertNumber.Substring(_convertNumber.IndexOf("e") + 1, Convert.ToInt16(_convertNumber.IndexOf("=")) - Convert.ToInt16(_convertNumber.IndexOf("e") + 1)));
            _resultNumber = _mainNumber * Math.Pow(EXPONENTIAL, Convert.ToDouble(_powNumber));
            return _resultNumber;
        }
    }
}