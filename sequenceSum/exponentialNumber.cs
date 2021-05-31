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

        private int _indexAfterDot;

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
            else if (IsDigitForm(input))
            {
                ParseFromDigitToExponentialForm(input);
            }
        }

        public string SetToNoramExponentialForm(string input)
        {
            int indexOfDot = input.IndexOf(".");
            int length = input.Length - indexOfDot;
            string partBeforeDot = input.Substring(1, indexOfDot - 1);
            string partAfterDot = input.Substring(indexOfDot + 1, length - 1);
            if (indexOfDot > 2)
            {
                input = input.Substring(0, 1) + "." + partBeforeDot + partAfterDot;
            }
            return input;
        }

        private void ParseFromDigitToExponentialForm(string input)
        {
            int lengthOfNumber = input.Length;
            if (lengthOfNumber == 1)
            {
                _firstPart = byte.Parse(input);
                _mantisa = new long[1] { 0 };
                _power = 0;
            }
            else if (lengthOfNumber < 39)
            {
                if (IsContainsDot(input))
                {
                    _indexAfterDot = input.IndexOf(".") + 1;
                    _firstPart = byte.Parse(input.Substring(0, _indexAfterDot - 2));
                    _mantisa = SetMantisa(input.Substring(_indexAfterDot, lengthOfNumber - _indexAfterDot));
                    _power = (byte)(lengthOfNumber - 3);
                }
            }
        }

        private void ParseFromExponentialToDigitForm(string input)
        {
            _indexAfterDot = input.IndexOf(".") + 1;

            int indexOfCharE = input.IndexOf("e");

            string mantisa = input.Substring(_indexAfterDot, indexOfCharE - _indexAfterDot);

            if (IsContainsDot(input))
            {
                if ((!IsHavingPlusOrMinus(input)))
                {
                    _firstPart = byte.Parse(input.Substring(0, _indexAfterDot - 1));
                    _mantisa = SetMantisa(mantisa);
                    _power = byte.Parse(input.Substring(indexOfCharE + 1, 2));
                }
            }
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
            if (!input.Contains("e") && !IsHavingPlusOrMinus(input))
            {
                return IsExponentialForm(input);
            }
            return false;
        }

        private bool IsHavingPlusOrMinus(string input)
        {
            if (input.Contains("+") || input.Contains("-"))
            {
                return true;
            }
            return false;
        }

        private bool IsContainsDot(string input)
        {
            if (input.Contains("."))
            {
                return true;
            }
            return false;
        }

        //public override string ToString()
        //{

        //}
    }
}