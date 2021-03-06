﻿namespace Euler
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    internal static partial class Program
    {
        public static void Problem026()
        {
            IEnumerable<Tuple<int, int>> recurringPartLength = GetRecurringPartLengths();
            int maxLength = recurringPartLength.Select(t => t.Item2).Max();
            Console.WriteLine(recurringPartLength.Single(t => t.Item2 == maxLength).Item1);
        }

        private static IEnumerable<Tuple<int, int>> GetRecurringPartLengths()
        {
            for (int d = 2; d < 1000; d++)
            {
                Tuple<List<int>, int> decimalRepresentation = GetDecimalRepresentation(d);
                if (decimalRepresentation != null)
                {
                    IEnumerable<int> recurringPart = decimalRepresentation.Item1.Skip(decimalRepresentation.Item2 - 1);
                    yield return Tuple.Create(d, recurringPart.Count());
                }
            }
        }

        private static Tuple<List<int>, int> GetDecimalRepresentation(int d)
        {
            int n = 1;
            List<int> quotients = new List<int>();
            // We need to stop when we see the same number to divide - we already know what would happen
            Dictionary<int, int> seen = new Dictionary<int, int>();
            int position = 0;
            while (true)
            {
                n = n * 10;
                position++;
                int previousIndex;

                if (seen.TryGetValue(n, out previousIndex))
                {
                    return Tuple.Create(quotients, previousIndex);
                }
                else
                {
                    seen.Add(n, position);
                    int quotient = n / d;
                    quotients.Add(quotient);
                }
                n = n % d;
                if (n == 0)
                {
                    return null;
                }
            }
        }
    }
}
