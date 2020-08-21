namespace Problem090
{
    using System.Linq;
    using Common;
    using System.Collections.Generic;
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            int count = 0;
            foreach (var dice1 in EulerUtil.Combinations(Enumerable.Range(0, 10), 6))
            {
                foreach (var dice2 in EulerUtil.Combinations(Enumerable.Range(0, 10), 6))
                {
                    var dice1Set = new HashSet<int>(dice1);
                    var dice2Set = new HashSet<int>(dice2);

                    if (dice1Set.Contains(6)) { dice1Set.Add(9); }
                    if (dice1Set.Contains(9)) { dice1Set.Add(6); }
                    if (dice2Set.Contains(6)) { dice2Set.Add(9); }
                    if (dice2Set.Contains(9)) { dice2Set.Add(6); }

                    int[,] squares = new int[,]
                    {
                        {0,1}, {0,4},{0,9},{1,6},{2,5},{3,6},{4,9},{6,4},{8,1}
                    };
                    bool valid = true;
                    for (int i = 0; valid && i < squares.Length / 2; i++)
                    {
                        if (dice1Set.Contains(squares[i, 0]) && dice2Set.Contains(squares[i, 1]))
                        {
                            continue;
                        }
                        if (dice2Set.Contains(squares[i, 0]) && dice1Set.Contains(squares[i, 1]))
                        {
                            continue;
                        }
                        valid = false;
                    }
                    if (valid)
                    {
                        count++;
                    }
                }
            }

            // Swapping dice1 and dice2 does not count as a new combination.
            Console.WriteLine(count / 2);
        }
    }
}
