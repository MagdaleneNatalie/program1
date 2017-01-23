
namespace TicTacToeGame
{
    using System;
    using System.Linq;

    public static class Helper
    {
        public static int GetMoveFromUser()
        {
            var correctValues = new[] { "0", "1", "2", "3", "4", "5", "6", "7", "8" };

            var keyValidation = false;
            var move = string.Empty;

            while (!keyValidation)
            {
                move = Console.ReadKey().KeyChar.ToString();

                if (correctValues.Contains(move))
                {
                    keyValidation = true;
                }
                else
                {
                    Console.WriteLine(" -- Błędna wartość. Poprawne wartości to: {0} ", string.Join(",", correctValues));
                }
            }

            return int.Parse(move);
        }

        public static void DrawBoard(int[] grid)
        {
            Console.Clear();

            for (int i = 0; i < grid.Length - 1; i += 3)
            {
                Console.WriteLine(" -----------");
                Console.WriteLine("| {0} | {1} | {2} |", DrawSign(grid[i]), DrawSign(grid[i + 1]), DrawSign(grid[i + 2]));
            }

            Console.WriteLine(" -----------");

        }

        private static string DrawSign(int i)
        {
            switch (i)
            {
                case 1:
                    return "X";
                case 2:
                    return "O";
                default:
                    return " ";
            }
        }
    }
}
