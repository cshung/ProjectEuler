namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem008()
        {
            string n = ReadResourceAsString("Euler.Problem008.txt");
            n = n.Replace("\n", string.Empty);
            n = n.Replace("\r", string.Empty);
            int[] d = n.ToCharArray().Select(c => c - '0').ToArray();
            int max = -1;
            for (int i = 0; i < 1000 - 5; i++)
            {
                int p = 1;
                for (int j = i; j < i + 5; j++)
                {
                    p *= d[j];
                }
                if (p > max)
                {
                    max = p;
                }
            }
            Console.WriteLine(max);
        }
    }
}