// Copyright (c) 2021 Medtronic, Inc. All rights reserved.

using BattleShips.Abstract.Visuals;
using TicTacToe;

namespace BattleShips.Utils
{
    public static class VisualContextExtensions
    {
        public static void Fill(this IVisualContext context, string[] array)
        {
            context.Output.PrintText(string.Empty).EndLine();

            for (int i = 0; i < array.Length; i++)
                context.Output.PrintText(array[i]);
        }

        public static void Fill(this IVisualContext context, Point position, string[] array)
        {
            context.Output.PrintText("\n", position);

            var y = position.Y;

            for (int i = 0; i < array.Length; i++)
            {
                var point = new Point(position.X, y);
                context.Output.PrintText(array[i], point).EndLine();
                y++;
            }
        }

        public static void FillAtCenter(this IVisualContext context, string[] array, Point position)
        {
            context.Output.PrintText("\n", position);

            var y = position.Y;

            for (int i = 0; i < array.Length; i++)
            {
                var point = new Point(position.X, y);
                context.Output.PrintText(array[i], point, true).EndLine();
                y++;
            }
        }
    }
}