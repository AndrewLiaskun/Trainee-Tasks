// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BullsAndCows
{
    public class BullsAndCows
    {
        private Random _rand = new Random();
        private Regex _regex = new Regex(@"^[0-9]{1}\,{1}[0-9]{1}$", RegexOptions.Compiled);

        private byte _length;
        private List<string> _answers;

        public BullsAndCows(byte length)
        {
            _length = length;
            _answers = GetAllCombinations();
        }

        public void StartGame()
        {
            // TODO: add here printing of game rules (HEADER)!!!!!!!!

            GameRules();
            while (_answers.Count > 1)
            {
                string input = "";
                byte bulls = 0;
                byte cows = 0;

                var answer = GetRandomGuess();
                Console.WriteLine("Computer guess: " + string.Join("", answer));
                Console.WriteLine();
                do
                {

                    (bulls, cows, input) = GetRightInput(input);
                }
                while (!IsGameRulesForm(input) || bulls + cows > _length);

                GenerateNewAnswers(answer, bulls, cows);

                // TODO: refactor this!!!!!

                Console.WriteLine($"Possible Answers:  {_answers.Count}");
                Console.WriteLine();
            }
            if (_answers.Count == 0)
                Console.WriteLine("You Win!");
            else
                Console.WriteLine("Answer: " + _answers[0]);
        }

        private static (byte Bulls, byte Cows) CheckPossibleGuess(string currentGuess, string expectedResult)
        {
            if (expectedResult.Equals(currentGuess))
                return (4, 0);

            byte bulls = 0, cows = 0;
            var editable = new StringBuilder(expectedResult);

            for (int i = 0; i < currentGuess.Length; i++)
            {
                if (currentGuess[i] == editable[i])
                {
                    editable[i] = '-';
                    ++bulls;
                }
                else
                {
                    if (editable.Contains(currentGuess[i]))
                    {
                        int idx = editable.IndexOf(currentGuess[i]);
                        if (currentGuess[idx] == editable[idx])
                            ++bulls;
                        else
                            ++cows;

                        editable[idx] = '-';
                    }
                }
            }

            return (bulls, cows);
        }

        private void GameRules()
        {
            Console.WriteLine("Rules of the game");
            Console.WriteLine("The computer thinks of four" +
                " (default number) digits from 0,1,2, ... 9." +
                " The player makes moves to find out these numbers and their order.");
            Console.WriteLine("Each move consists of four digits, 0 can come first.");
            Console.WriteLine("In response, the computer shows the number of guessed digits standing in their places" +
                " (the number of bulls) and the number of guessed digits that are not in their places (the number of cows).");

            Console.WriteLine();
        }

        private (byte bulls, byte cows, string output) GetRightInput(string input)
        {

            byte bulls = 0, cows = 0;
            do
            {

                Console.WriteLine("Enter bulls and cows like \"0,0\"");
                input = Console.ReadLine();
            }
            while (!IsGameRulesForm(input));
            bulls = byte.Parse(input.Substring(0, 1));
            cows = byte.Parse(input.Substring(2, 1));

            return (bulls, cows, input);
        }

        private bool IsGameRulesForm(string input)
        {
            return _regex.IsMatch(input);
        }

        private void GenerateNewAnswers(string guess, byte bulls, byte cows)
        {
            var current = new List<string>();

            for (int i = _answers.Count - 1; i >= 0; i--)
            {
                var nextAnswer = _answers[i];

                var (currentBulls, currentCows) = CheckPossibleGuess(guess, nextAnswer);

                if (currentBulls == bulls && currentCows == cows)
                {
                    current.Add(nextAnswer);
                }
            }

            _answers = current;
        }

        private string GetRandomGuess() => _answers[_rand.Next(_answers.Count)];

        private List<string> GetAllCombinations()
        {
            return Enumerable.Range(0, (int)Math.Pow(10, _length))
                             .Select(x => x.ToString($"d{_length}")).ToList();
        }
    }
}