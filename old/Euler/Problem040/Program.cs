namespace Problem040
{
    using System;
    using System.Linq;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            long lastTrunk = 0;

            // Look like these trunkEnds can be analytically computed
            // But it doesn't really matter since computing these is blinking fast

            // The tuple is interpreted as first index of the trunk (1-based), the number that starts the trunk, and the number of digits in the trunk
            List<Tuple<long, long, int>> trunks = new List<Tuple<long, long, int>>();
            trunks.Add(Tuple.Create(1L, 1L, 1));
            for (int i = 1; i < 10; i++)
            {
                lastTrunk += (Power(10, i) - Power(10, i - 1)) * i;
                trunks.Add(Tuple.Create(lastTrunk + 1, Power(10, i), i + 1));
            }

            int prod = 1;

            for (int i = 0; i < 7; i++)
            {
                long digit = Power(10, i);
                Tuple<long, long, int> currentTrunk = trunks.Where(t => t.Item1 <= digit).ArgMax(t => t.Item1);
                //Console.WriteLine(digit + " is in " + currentTrunk);

                // Now you wanted to find, within the current trunk, which number it is.
                // You know the starting digit, I also want to know the starting number as well 
                long firstTrunkIndex = currentTrunk.Item1;
                long firstTrunkValue = currentTrunk.Item2;
                int trunkCellSize = currentTrunk.Item3;

                long remainingDigitsToConsume = digit - (firstTrunkIndex - 1);
                //Console.WriteLine("digits to consume within trunk = " + remainingDigitsToConsume);

                // Cell number is a 1 based index of what cell are passed in the first pass (i.e. exclude offset)
                long cellNumber = remainingDigitsToConsume / trunkCellSize; 

                // Cell value is where we stopped - notice it could be that we have take no step and stopped before the starting line
                long cellValue = firstTrunkValue + cellNumber - 1;

                int cellOffset = (int)(remainingDigitsToConsume % trunkCellSize);

                if (cellOffset != 0)
                {
                    cellValue++;
                }

                //Console.WriteLine(cellNumber + " cells are passes by = [" + firstTrunkValue + " to " + cellValue + "]");
                int stoppingDigit = -1;
                if (cellOffset == 0)
                {
                    stoppingDigit = (int)(cellValue % 10);
                }
                else
                {
                    stoppingDigit = cellValue.ToString()[cellOffset - 1] - '0';
                }
                //Console.WriteLine("At the stopping digit, the value is " + stoppingDigit);
                prod *= stoppingDigit;
                //Console.WriteLine();
            }
            Console.WriteLine(prod);
        }

        private static long Power(long x, long y)
        {
            if (y == 0)
            {
                return 1;
            }
            else if (y == 1)
            {
                return x;
            }
            else
            {
                long r = Power(x, y / 2);
                return y % 2 == 0 ? r * r : r * r * x;
            }
        }
    }

    public static class LinqHelper
    {
        public static TSrc ArgMax<TSrc, TArg>(this IEnumerable<TSrc> ie, Converter<TSrc, TArg> fn) where TArg : IComparable<TArg>
        {
            IEnumerator<TSrc> e = ie.GetEnumerator();
            if (!e.MoveNext())
                throw new InvalidOperationException("Sequence has no elements.");
            TSrc t_try, t = e.Current;
            if (!e.MoveNext())
                return t;
            TArg v, max_val = fn(t);
            do
            {
                if ((v = fn(t_try = e.Current)).CompareTo(max_val) > 0)
                {
                    t = t_try;
                    max_val = v;
                }
            }

            while (e.MoveNext());
            return t;
        }
    }
}
