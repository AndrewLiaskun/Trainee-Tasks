// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace sequenceSum
{
    public class ExponentialNumber
    {

        public static ExponentialNumber Zero = new ExponentialNumber();
        private static Regex _regex = new Regex(@"^[0-9]+\.?[0-9]+e?[\+\-]?[0-9]+$", RegexOptions.Compiled);

        private byte _firstPart;
        private byte _power;
        private long[] _mantisa;

        public ExponentialNumber()
        {
            _firstPart = 0;
            _power = 0;
            _mantisa = new long[1] { 0 };
        }

        //public static ExponentialNumber ParseStr(string enterStr)
        //{
        //    ExponentialNumber result = new ExponentialNumber();
        //}

        public void ParseProperties(string input)
        {
            if (IsExponentialForm(input))
            {
                ParseFromExponentialToDigitForm(input);
            }
            else
            {
            }
        }

        private void ParseFromExponentialToDigitForm(string input)
        {
            int indexAfterDot = input.IndexOf(".") + 1;

            int indexOfCharE = input.IndexOf("e");

            string mantisa = input.Substring(indexAfterDot, indexOfCharE - indexAfterDot);

            if (input.Contains("."))
            {
                if ((!input.Contains("+") || !input.Contains("-")))
                {
                    _firstPart = byte.Parse(input.Substring(0, indexAfterDot - 1));
                    _mantisa = SetMantisa(mantisa);
                    _power = byte.Parse(input.Substring(indexOfCharE + 1, 2));
                }
            }
        }

        private void ParseFromDigitToExponentialForm(string input)
        {
        }

        private long[] SetMantisa(string input)
        {
            int maxLengthLong = long.MaxValue.ToString().Length;
            int lengthOfSecondPart = input.Length - maxLengthLong;
            if (input.Length <= maxLengthLong)
            {
                _mantisa = new long[1] { long.Parse(input) };
                return _mantisa;
            }
            else
            {
                _mantisa = new long[2] { long.Parse(input.Substring(0, maxLengthLong)),
                    long.Parse(input.Substring(maxLengthLong, lengthOfSecondPart))  };
                return _mantisa;
            }
        }

        private bool IsExponentialForm(string input)
        {
            return _regex.IsMatch(input);
        }

        private bool IsDigitForm(string input)
        {
            if (!input.Contains("e") && !input.Contains("+") && !input.Contains("-"))
            {
                return IsExponentialForm(input);
            }
            return false;
        }

        //public override string ToString()
        //{

        //}
    }
}