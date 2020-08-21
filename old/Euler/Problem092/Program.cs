using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem092
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, int> destiny = new Dictionary<int, int>();
            for (int i = 1; i <= 10000000; i++)
            {
                int j = i;
                List<int> trials = new List<int>();
                trials.Add(j);
                while (j != 1 && j != 89)
                {
                    if (destiny.ContainsKey(j))
                    {
                        j = destiny[j];
                        break;
                    }
                    else
                    {
                        //Console.Write(j + " ");
                        j = DigitSquareSum(j);
                        trials.Add(j);
                    }
                }
                foreach (int trial in trials)
                {
                    destiny[trial] = j;
                }
                //Console.WriteLine();
                //Console.ReadKey();
                //Console.WriteLine();
                //foreach (var kvp in destiny) {
                //    Console.WriteLine(kvp.Key + " " + kvp.Value);
                //}
            }
            Console.WriteLine(destiny.Where(t => t.Value == 89).Count());
        }

        private static int DigitSquareSum(int i)
        {
            return i.ToString().Select(c => c - '0').Select(x => x * x).Aggregate((x, y) => x  + y);
        }
    }
}
