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
        private byte[] _mantisa;

        public ExponentialNumber()
        {
            _firstPart = 0;
            _power = 0;
            _mantisa = new byte[] { 0 };
        }

        public static ExponentialNumber Parse(string input)
        {
            if (!IsExponentialForm(input))
            {
                throw new FormatException("Incorrectly entered data");
            }

            var normalizedInput = GetNormalizeInput(input);

            var dotIndex = normalizedInput.IndexOf(".");
            var expIndex = normalizedInput.IndexOf("e");

            var integralDigits = GetDigits(normalizedInput.Substring(0, dotIndex));
            var floatingDigits = GetDigits(normalizedInput.Substring(dotIndex + 1, (expIndex - 1) - dotIndex));
            byte exponential;
            if (IsHavingPlusOrMinus(normalizedInput))
            {
                exponential = byte.Parse(normalizedInput.Substring(expIndex + 2));
            }
            else
            {
                exponential = byte.Parse(normalizedInput.Substring(expIndex + 1));
            }

            var power = exponential + integralDigits.Length - 1;
            var mantisa = new byte[integralDigits.Length + floatingDigits.Length - 1];

            var integralNum = integralDigits[0];
            Array.Copy(integralDigits, 1, mantisa, 0, integralDigits.Length - 1);
            Array.Copy(floatingDigits, 0, mantisa, integralDigits.Length - 1, floatingDigits.Length);
            return new ExponentialNumber { _firstPart = integralNum, _mantisa = mantisa, _power = (byte)power };
        }

        public ExponentialNumber GetSimpleView(ExponentialNumber other)
        {
            byte difference = (byte)(this._power - other._power);

            var mantisa = new byte[other._mantisa.Length + difference];
            int count = 0;
            for (int i = 0; i < mantisa.Length; i++)
            {
                if (difference > i)
                {
                    mantisa[i] = 0;
                }
                else
                {
                    mantisa[i] = other._mantisa[count++];
                }
            }

            return new ExponentialNumber { _firstPart = 0, _mantisa = mantisa, _power = this._power };
        }

        public ExponentialNumber Add(ExponentialNumber other)
        {
            ExponentialNumber sumOfTwo = new ExponentialNumber();
            if (this._power > other._power)
            {
                other = this.GetSimpleView(other);
                sumOfTwo._firstPart = (byte)(this._firstPart + other._firstPart);
                sumOfTwo._power = this._power;

                byte difference = (byte)(this._power - other._power);
                sumOfTwo._mantisa = new byte[other._mantisa.Length + difference];
                for (int i = sumOfTwo._mantisa.Length; i >= 0; i--)
                {
                    int lengthOfFirstMantisa = this._mantisa.Length;
                    int lengthOfSecondMantisa = other._mantisa.Length;
                    if ((this._mantisa[i] + other._mantisa[i]) <= 9)
                    {
                        sumOfTwo._mantisa[i] += (byte)(this._mantisa[i] + other._mantisa[i]);
                    }
                    else
                    {
                        sumOfTwo._mantisa[i] += (byte)((this._mantisa[i] + other._mantisa[i]) / 10);
                        sumOfTwo._mantisa[i--] = 1;
                    }
                }
                return sumOfTwo;
            }
            else if (this._power == other._power)
            {
                return new ExponentialNumber();
            }
            return new ExponentialNumber();
        }

        private static string GetNormalizeInput(string input)
        {
            if (IsContainsDot(input))
            {
                if (IsExponentialForm(input))
                {
                    return input;
                }
                else
                {
                    return input + "e1";
                }
            }
            else if (IsExponentialForm(input) != IsDigitForm(input))
            {
                int indexOfE = input.IndexOf("e");
                return input.Substring(0, indexOfE) + ".0" + input.Substring(indexOfE);
            }
            return input + ".0e1";
        }

        private static byte[] GetDigits(string input)
        {
            return input.Select(x => byte.Parse(x.ToString())).ToArray();
        }

        private static bool IsExponentialForm(string input)
        {
            return _regex.IsMatch(input);
        }

        private static bool IsDigitForm(string input)
        {
            if (!input.Contains("e") && !IsHavingPlusOrMinus(input))
            {
                return IsExponentialForm(input);
            }
            return false;
        }

        private static bool IsHavingPlusOrMinus(string input)
        {
            if (input.Contains("+") || input.Contains("-"))
            {
                return true;
            }
            return false;
        }

        private static bool IsContainsDot(string input)
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