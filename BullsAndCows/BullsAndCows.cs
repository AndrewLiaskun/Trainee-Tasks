// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BullsAndCows
{
    public class BullsAndCows
    {
        private Regex _regex = new Regex(@"^[0-9]+$", RegexOptions.Compiled);
        private byte[] _bulls;
        private byte[] _cows;

        private byte[] _digitsFromUser;

        public BullsAndCows()
        {
        }

        public void StartGame(string input)
        {
            TakeDigitsFromUser(input);
            int length = _digitsFromUser.Length;
            _cows = new byte[length];
            _bulls = new byte[length];
            while (!_bulls.SequenceEqual(_digitsFromUser))
                _bulls = GetBullsOrCows();

            Console.WriteLine(string.Join("", _bulls));
        }

        private byte[] GenerateNumbersWithoutBullsOrCows()
        {
            Random a = new Random();
            int length = _digitsFromUser.Length;
            byte[] randomNumbers = new byte[length];
            for (int i = 0; i < length;)
            {
                int newNumber = a.Next(0, 10);

                bool noBullsInNewNumber = _bulls != null ? !_bulls.Contains((byte)newNumber) : true;
                bool noCowsInNewNumber = _cows != null ? !_cows.Contains((byte)newNumber) : true;
                bool noRepetition = !randomNumbers.Contains((byte)newNumber);

                if (noBullsInNewNumber && noCowsInNewNumber && noRepetition)
                {
                    randomNumbers[i] = (byte)newNumber;
                    i++;
                }
            }
            return randomNumbers;
        }

        private void TakeDigitsFromUser(string input)
        {
            if (IsOkayDigits(input))
            {
                _digitsFromUser = GetDigits(input);
            }
            else { throw new Exception("STOP! IT'S NOT OKAY"); }
        }

        private byte[] GetDigits(string input)
        {
            return input.Select(x => byte.Parse(x.ToString())).ToArray();
        }

        private byte[] GetBullsOrCows()
        {
            int length = _digitsFromUser.Length;

            int countForCows = 0;
            var random = GenerateNumbersWithoutBullsOrCows();
            for (int i = 0; i < length; i++)
            {

                if (_digitsFromUser.Contains(random[i]))
                {
                    _cows[countForCows] = random[i];
                    countForCows++;
                }
                else if (random[i] == _digitsFromUser[i])
                {
                    _bulls[i] = random[i];
                }
            }
            return _bulls;
        }

        private bool IsOkayDigits(string input)
        {
            return IsDigit(input) == IsNoRepetitionDigits(input);
        }

        private bool IsNoRepetitionDigits(string input)
        {
            return input.Distinct().Count() == input.Length;
        }

        private bool IsDigit(string input)
        {
            return _regex.IsMatch(input);
        }
    }
}