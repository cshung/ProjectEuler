using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem030
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(GetPowers().Aggregate((x, y) => x + y));
        }

        private static IEnumerable<int> GetPowers()
        {
            int max = 9 * 9 * 9 * 9 * 9 * 6;
            int[] powers = Enumerable.Range(0, 10).Select(x => x * x * x * x * x).ToArray();
            for (int i = 1; i <= max; i++)
            {
                IEnumerable<int> digits = i.ToString().Select(c => c - '0');
                int digitPowerSum = digits.Select(d => powers[d]).Aggregate((x, y) => x + y);
                if (digitPowerSum == i && i != 1)
                {
                    yield return i;
                }
            }
        }
    }
}
