namespace Euler
{
    using System;
    using System.Linq;
    using System.Numerics;

    internal static partial class Program
    {
        public static void Problem018()
        {
            string input = ReadResourceAsString("Euler.Problem018.txt");
            int[] inputParsed = input.Replace('\r', ' ').Replace('\n', ' ').Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(x => Int32.Parse(x)).ToArray();
            int numRows = 15;

            // answer(row, column) = data[row, column] + max(answer(row + 1, column), answer(row + 1, column + 1));

            int[,] answer = new int[numRows, numRows];
            for (int i = 1; i <= numRows; i++)
            {
                answer[i - 1, numRows - 1] = inputParsed[i + numRows * (numRows - 1) / 2 - 1];
            }
            for (int currentRow = numRows - 1; currentRow > 0; currentRow--)
            {
                for (int currentColumn = 1; currentColumn <= currentRow; currentColumn++)
                {
                    int currentValue = inputParsed[currentColumn + currentRow * (currentRow - 1) / 2 - 1];
                    answer[currentColumn - 1, currentRow - 1] = currentValue + Math.Max(answer[currentColumn - 1, currentRow + 1 - 1], answer[currentColumn + 1 - 1, currentRow + 1 - 1]);
                }
            }
            Console.WriteLine(answer[0, 0]);
        }
    }
}
