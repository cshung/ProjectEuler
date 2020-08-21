using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem031
{
    class Program
    {
        static void Main(string[] args)
        {
            // Count the number of using different coins
            int[] coinValues = new int[] { 1, 2, 5, 10, 20, 50, 100, 200 };
            // Ways(value, coinValues) = sum(i = 0; i <= floor(value/max(coinValues)); i++) { sum(value - i * max(coinValues); coinValues - {max(coinValue)}}
            Console.WriteLine(Way(200, coinValues, coinValues.Length - 1));
        }

        static int Way(int value, int[] coinValues, int maxIndex)
        {
            // This is special because we know it is 1, the value is can always be represented as sum of 1 in only 1 way.
            if (maxIndex == 0) { return 1; }
            int maxValue = coinValues[maxIndex];
            return Enumerable.Range(0, value / maxValue + 1).Select(t => Way(value - maxValue * t, coinValues, maxIndex-1)).Aggregate((x, y) => x + y);
        }
    }
}
