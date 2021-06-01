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
        private int _indexOfCharE;

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

        public void Parse(string input)
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

        private string SetToNoramExponentialForm(string input)
        {
            string firstPart = input.Substring(0, 1);

            if (!IsDigitForm(input))
            {
                _indexOfCharE = input.IndexOf("e");
            }

            if (IsContainsDot(input))
            {
                int indexOfDot = input.IndexOf(".");
                int length = input.Length - indexOfDot;

                string partBeforeDot = input.Substring(1, indexOfDot - 1);
                string partAfterDot = input.Substring(indexOfDot + 1, length - 1);

                if (indexOfDot > 2)
                {
                    if (IsDigitForm(input))
                    {
                        input = firstPart + "." + partBeforeDot + partAfterDot + "e" + "+" + partBeforeDot.Length;
                    }
                    else
                    {

                        int power = int.Parse(input.Substring(_indexOfCharE + 1)) + partBeforeDot.Length;
                        if (!IsHavingPlusOrMinus(input))
                        {
                            input = firstPart + "." + partBeforeDot + "e" + "+" + power;
                        }
                        else
                        {
                            input = firstPart + "." + partBeforeDot + "e" + input.Substring(input.IndexOf("+"), 1) + power;
                        }
                    }
                }
                return input;
            }
            else
            {
                string mantisa;

                if (IsDigitForm(input))
                {
                    mantisa = input.Substring(1, input.Length - 1);
                    input = firstPart + "." + mantisa + "e" + "+" + input.Substring(1, input.Length - 1).Length;
                }
                else
                {
                    int power = int.Parse(input.Substring(_indexOfCharE + 1)) + input.Substring(1, _indexOfCharE - 1).Length;
                    mantisa = input.Substring(1, _indexOfCharE - 1);
                    if (!IsHavingPlusOrMinus(input))
                    {
                        input = firstPart + "." + mantisa + "e" + "+" + power;
                    }
                    else
                    {
                        input = firstPart + "." + mantisa + "e" + input.Substring(input.IndexOf("+"), 1) + power;
                    }
                }
                return input;
            }
        }

        private void ParseFromDigitToExponentialForm(string input)
        {
            input = SetToNoramExponentialForm(input);

            _indexAfterDot = input.IndexOf(".") + 1;

            _indexOfCharE = input.IndexOf("e");

            string mantisa = input.Substring(_indexAfterDot, _indexOfCharE - _indexAfterDot);
            int lengthOfNumber = input.Length;
            int indexAfterPlus = input.IndexOf("+") + 1;

            if (lengthOfNumber == 1)
            {
                _firstPart = byte.Parse(input);
                _mantisa = new long[1] { 0 };
                _power = 0;
            }
            else if (lengthOfNumber < 39)
            {
                _firstPart = byte.Parse(input.Substring(0, 1));
                _mantisa = SetMantisa(mantisa);
                _power = byte.Parse(input.Substring(indexAfterPlus));
            }
        }

        private void ParseFromExponentialToDigitForm(string input)
        {
            input = SetToNoramExponentialForm(input);
            _indexAfterDot = input.IndexOf(".") + 1;
            _indexOfCharE = input.IndexOf("e");

            string mantisa = input.Substring(_indexAfterDot, _indexOfCharE - _indexAfterDot);
            int indexAfterPlus = input.IndexOf("+") + 1;
            int lengthOfNumber = input.Length;
            if (lengthOfNumber == 1)
            {
                _firstPart = byte.Parse(input);
                _mantisa = new long[1] { 0 };
                _power = 0;
            }
            else if (lengthOfNumber < 39)
            {
                _firstPart = byte.Parse(input.Substring(0, 1));
                _mantisa = SetMantisa(mantisa);
                _power = byte.Parse(input.Substring(indexAfterPlus));
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