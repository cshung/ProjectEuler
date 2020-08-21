using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem002
{
    class Program
    {
        static void Main(string[] args)
        {
            int fib, fibLast, fibLastLast;
            fibLast = 1;
            fibLastLast = 1;
            int sum = 0;
            do
            {
                fib = fibLast + fibLastLast;
                if (fib % 2 == 0)
                {
                    sum += fib;
                }

                fibLastLast = fibLast;
                fibLast = fib;
            } while (fib < 4000000);
            Console.WriteLine(sum);
        }
    }
}
