using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem085
{
    class Program
    {
        // The number of rectangle is simply C(w + 1, 2) x C(h + 1, 2)
        // This can be understood as finding two horizontal lines and two vertical lines will form a rectangle
        // So the goal is to find a number such that it factorize into two triangle numbers.
        static void Main(string[] args)
        {
            int goal = 2000000;
            long minDistance = 2000000;
            long minReach = 0;
            int minI = 0;
            int minJ = 0;
            // 2000 reaches all triangle numbers less than 2,000,000
            var triangleNumbers = Enumerable.Range(1, 2000).Select(n => n * (n + 1) / 2).ToArray();
            for (int i = 0; i < triangleNumbers.Length; i++)
            {
                for (int j = 0; j < triangleNumbers.Length; j++)
                {
                    long reach = triangleNumbers[i] * (long)triangleNumbers[j];
                    long distance = goal > reach ? goal - reach : reach - goal;
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        minReach = reach;
                        minI = i;
                        minJ = j;
                    }
                }
            }

            // This means (minI + 1)(minI + 2)(minJ + 1)(minJ + 2)/4 = minReach
            // c(n, 2) = n!/(n-2)!2! = n(n-1)(n-2)!/2! = n(n - 1)/2 
            // Therefore = (minI + 1)(minI + 2)/2 = C(minI + 2, 2) => w = minI + 1
            Console.WriteLine((minI + 1) * (minJ + 1));

        }
    }
}
