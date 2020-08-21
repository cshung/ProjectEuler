using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem052
{
    class Program
    {
        static void Main(string[] args)
        {
            int i = 1;
            while (true)
            {
                var multiples = Enumerable.Range(1, 6).Select(t => i * t).Select(t => t.ToString().ToArray()).ToArray();
                foreach (var multiple in multiples)
                {
                    Array.Sort(multiple);
                }

                var sortedMultiple = multiples.Select(a => new string(a)).ToArray();
                bool allMatch = sortedMultiple.Select(s => string.Equals(s, sortedMultiple[0])).Aggregate((x, y) => x && y);
                if (allMatch)
                {
                    Console.WriteLine(i);
                    return;
                }
                i++;
            }
        }
    }
}
