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

        public ExponentialNumber(byte firstPart, byte power, byte[] mantisa)
        {
            _firstPart = firstPart;
            _power = power;
            _mantisa = mantisa;
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

        public static ExponentialNumber operator +(ExponentialNumber a, ExponentialNumber b) => a.Add(b);

        public ExponentialNumber Add(ExponentialNumber other)
        {
            var normolizedNumber = other.GetNormalizedNumber(this);
            var normolizedSelf = this.GetNormalizedNumber(other);

            var sumMantissa = new List<byte>();

            byte overflow = 0;
            var power = normolizedSelf._power;

            for (int i = normolizedSelf._mantisa.Length - 1; i >= 0; i--)
            {
                var sum = (byte)(normolizedSelf._mantisa[i] + normolizedNumber._mantisa[i] + overflow);
                if (sum > 10)
                {
                    overflow = (byte)(sum % 10);
                    sum -= 10;
                }
                sumMantissa.Insert(0, sum);
            }
            var firstPart = normolizedNumber._firstPart + normolizedSelf._firstPart + overflow;
            if (firstPart > 10)
            {
                power++;
                sumMantissa.Insert(0, (byte)(firstPart % 10));
                firstPart -= firstPart;
            }

            return new ExponentialNumber((byte)firstPart, power, sumMantissa.ToArray());
        }

        public override string ToString() => $"{_firstPart}.{string.Join("", _mantisa)}e{_power}";

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

        private ExponentialNumber GetNormalizedNumber(ExponentialNumber other)
        {

            int difference = this._power > other._power ? 0 : other._power - this._power;

            byte power = Math.Max(this._power, other._power);

            var length = Math.Max(other._mantisa.Length, this._mantisa.Length) + Math.Abs(this._power - other._power);
            byte[] newMantissa = new byte[length];

            if (difference > 0)
            {
                newMantissa[difference - 1] = this._firstPart;
            }
            Array.Copy(this._mantisa, 0, newMantissa, difference, this._mantisa.Length);

            return new ExponentialNumber(difference == 0 ? this._firstPart : (byte)0, power, newMantissa);
        }
    }
}