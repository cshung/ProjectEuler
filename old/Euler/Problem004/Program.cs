using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem004
{
    class Program
    {
        static void Main(string[] args)
        {
            int max = 0;
            for (int i = 999; i > 0; i--)
            {
                for (int j = 999; j > 0; j--)
                {
                    int candidate = i * j;
                    IEnumerable<char> check = candidate.ToString();
                    IEnumerable<char> hcehc = check.Reverse();
                    if (check.SequenceEqual(hcehc))
                    {
                        if (candidate > max)
                        {
                            max = candidate;
                        }
                    }
                }
            }
            Console.WriteLine(max);
        }
    }
}
