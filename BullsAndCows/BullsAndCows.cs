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
        private Random _rand = new Random();

        private byte _length;

        public BullsAndCows(byte length)
        {
            _length = length;
        }

        public void StartGame()
        {
            var allAnswers = GetAllPossibleAnswers();
            while (allAnswers.Count > 1)
            {
                var answer = GetOneAnswer(allAnswers);
                Console.WriteLine(string.Join("", answer));
                Console.WriteLine("How much bulls?");
                byte bulls;
                byte.TryParse(Console.ReadLine(), out bulls);
                if (bulls == 4)
                {
                    allAnswers.Clear();
                    allAnswers.Add(answer);
                    break;
                }
                Console.WriteLine("How much cows?");
                byte cows;
                byte.TryParse(Console.ReadLine(), out cows);
                allAnswers = GetNewListWithoutBadAnswers(allAnswers, answer, bulls, cows);
                Console.WriteLine($"Possible Answers:  {allAnswers.Count}");
                Console.WriteLine();
            }
            Console.WriteLine($"Answer: {string.Join("", allAnswers[0])}");
        }

        private static byte[] TestAnswers(string expectedResult, string curAnswer)
        {
            if (expectedResult.Equals(curAnswer))
                return new byte[] { 4, 0 };
            StringBuilder counterString = new StringBuilder(expectedResult);
            string result = expectedResult;
            for (int i = 0; i < curAnswer.Length; i++)
            {
                if (curAnswer[i] == result[i]) counterString[i] = 'b';
                else if (result.Contains(curAnswer[i]))
                {
                    int idx = result.IndexOf(curAnswer[i]);
                    if (curAnswer[idx] == result[idx])
                        counterString[idx] = 'b';
                    else counterString[idx] = 'c';
                }
                result = counterString.ToString();
            }
            byte bulls = Convert.ToByte(result.Count(c => c == 'b'));
            byte cows = Convert.ToByte(result.Count(c => c == 'c'));
            return new byte[] { bulls, cows };
        }

        private List<byte[]> GetNewListWithoutBadAnswers(List<byte[]> list, byte[] currAnswer, byte bulls, byte cows)
        {
            if (bulls == 0 && cows == 0)
            {
                list.RemoveAll(number => number.Any(digit => currAnswer.Contains(digit)));
            }
            else
                for (int i = 0; i < list.Count; i++)
                {
                    var index = _rand.Next(list.Count);

                    var nextAnswer = list[index];
                    byte[] bullsAndCowsFromCurrAnswer = TestAnswers(string.Join("", nextAnswer), string.Join("", currAnswer));
                    if (bullsAndCowsFromCurrAnswer[0] != bulls || bullsAndCowsFromCurrAnswer[1] != cows)
                    {
                        list.RemoveAt(index);
                    }
                }
            list.Remove(currAnswer);
            return list;
        }

        private byte[] GetOneAnswer(List<byte[]> allAnswers)
        {
            var index = _rand.Next(allAnswers.Count);
            return allAnswers[index];
        }

        private List<byte[]> GetAllPossibleAnswers()
        {
            List<byte[]> answers = new List<byte[]>();
            double lenghtForList = Math.Pow(10, (int)_length);

            for (int i = 1; i < lenghtForList; i++)
            {
                byte[] currAnswer = new byte[_length];
                string tmp = i.ToString();

                if (tmp.Length < _length)
                    tmp = tmp.PadLeft(_length, '0');

                for (int j = _length - 1; j >= 0; j--)
                {
                    currAnswer[j] = byte.Parse(tmp[j].ToString());
                }
                answers.Add(currAnswer);
            }
            return answers;
        }
    }
}