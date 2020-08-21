using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem018
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = @"
75
 95 64
 17 47 82
 18 35 87 10
 20 04 82 47 65
 19 01 23 75 03 34
 88 02 77 73 07 63 67
 99 65 04 28 06 16 70 92
 41 41 26 56 83 40 80 70 33
 41 48 72 33 47 32 37 16 94 29
 53 71 44 65 25 43 91 52 97 51 14
 70 11 33 28 77 73 17 78 39 68 17 57
 91 71 52 38 17 14 91 43 58 50 27 29 48
 63 66 04 68 89 53 67 30 73 16 69 87 40 31
 04 62 98 27 23 09 70 98 73 93 38 53 60 04 23
";
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
