namespace Euler
{
    using System;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem011()
        {
            string input = ReadResourceAsString("Euler.Problem011.txt");
            int[] inputFlatten = input.Split(new char[] { ' ', '\r', 'n' }, StringSplitOptions.RemoveEmptyEntries).Select(s => Int32.Parse(s)).ToArray();
            int[,] inputArray = new int[20, 20];
            int p = 0;
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    inputArray[i, j] = inputFlatten[p++];
                }
            }

            int max = -1;

            // Try all horizontal lines
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    int prod = 1;
                    for (int k = i; k < i + 4; k++)
                    {
                        prod *= inputArray[k, j];
                    }
                    if (prod > max)
                    {
                        max = prod;
                    }
                }
            }

            // Vertical lines
            for (int i = 0; i < 20; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    int prod = 1;
                    for (int k = j; k < j + 4; k++)
                    {
                        prod *= inputArray[i, k];
                    }
                    if (prod > max)
                    {
                        max = prod;
                    }
                }
            }

            // Slash 
            for (int i = 0; i < 17; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    int prod = 1;
                    for (int k = 0; k < 4; k++)
                    {
                        prod *= inputArray[i + k, j + k];
                    }
                    if (prod > max)
                    {
                        max = prod;
                    }
                }
            }

            // Backslash
            for (int i = 3; i < 20; i++)
            {
                for (int j = 0; j < 17; j++)
                {
                    int prod = 1;
                    for (int k = 0; k < 4; k++)
                    {
                        prod *= inputArray[i - k, j + k];
                    }
                    if (prod > max)
                    {
                        max = prod;
                    }
                }
            }
            Console.WriteLine(max);
        }
    }
}
