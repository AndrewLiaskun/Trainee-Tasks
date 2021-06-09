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
        private Random rand = new Random();

        private byte _length;

        public BullsAndCows(byte length)
        {
            _length = length;
        }

        public void startGame()
        {
            var allAnswers = GetAllPossibleAnswers();

            while (allAnswers.Count > 1)
            {
                var answer = GetOneAnswer();
                Console.WriteLine(string.Join("", answer));
                Console.WriteLine("How much bulls?");
                byte bull = byte.Parse(Console.ReadLine());
                Console.WriteLine("How much cows?");
                byte cows = byte.Parse(Console.ReadLine());
                allAnswers = GetNewListWithoutBadAnswers(allAnswers, answer, bull, cows);
                Console.WriteLine($"Possible Answers:  {allAnswers.Count}");
                Console.WriteLine();
            }
            Console.WriteLine($"Answer: {string.Join("", allAnswers[0])}");
        }

        private byte[] CheckAnswer(byte[] answer, byte[] nextPosibleAnswer)
        {
            byte bulls = 0;
            byte cows = 0;
            for (int i = 0; i < _length; i++)
            {
                if (answer[i] == nextPosibleAnswer[i])
                {
                    bulls++;
                }
                else if (nextPosibleAnswer.Contains(answer[i]) && (answer[i] != nextPosibleAnswer[i]))
                    cows++;
            }

            return new byte[] { bulls, cows };
        }

        private List<byte[]> GetNewListWithoutBadAnswers(List<byte[]> list, byte[] badAnswer, byte bulls, byte cows)
        {
            for (int i = 0; i < list.Count; i++)
            {
                byte[] bullsAndCowsFromCurrAnswer = CheckAnswer(badAnswer, list[i]);

                if (bullsAndCowsFromCurrAnswer[0] != bulls || bullsAndCowsFromCurrAnswer[1] != cows)
                    list.Remove(list[i]);
            }
            return list;
        }

        private byte[] GetOneAnswer()
        {
            var answers = GetAllPossibleAnswers();
            var index = rand.Next(answers.Count);
            return answers[index];
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