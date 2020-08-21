using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem095
{
    class Program
    {
        static void Main(string[] args)
        {
            int MAX = 1000000;
            var factorizations = Enumerable.Range(2, MAX - 2).Select(i => EulerUtil.BruteForceFactor(i).ToList()).ToList();
            var groupedFactorization = factorizations.Select(t => t.GroupBy(u => u).Select(v => Tuple.Create(v.Key, v.Count())).ToList()).ToList();
            var sumOfDivisors = groupedFactorization.Select(t => t.Select(u => (EulerUtil.Power(u.Item1, u.Item2 + 1) - 1)/(u.Item1 - 1)).Aggregate(1L, (x, y)=>x * y)).ToList();
            var sumOfProperDivisor = new List<int> { 0, 1 }.Concat(sumOfDivisors.Select((t, i) => (int)(t - i - 2))).ToArray();

            var cycles = ComputeCycles(MAX, sumOfProperDivisor).ToList();
            Console.WriteLine(cycles.ArgMax(t => t.Count).First());
        }

        // This is a problem of finding all cycles in a directed graph with at most one neighbor per node
        // The algorithm simply follow the chain with a couple memorization technique to reduce redundant scans
        // 1.) If we reach a node that is scanned before, it won't produce new cycle, so just skip it.
        // 2.) If we reach a node that is scanning in the current phase, a new cycle is created, mark all visited node and yield a cycle
        private static IEnumerable<List<int>> ComputeCycles(int MAX, int[] sumOfProperDivisor)
        {
            int[] cycleLengths = new int[MAX];
            for (int i = 0; i < MAX; i++)
            {
                if (cycleLengths[i] == 0)
                {
                    int current = i;
                    List<int> visitedList = new List<int>();
                    bool formNewCycle = true;
                    while (!visitedList.Contains(current))
                    {
                        if (current >= sumOfProperDivisor.Length)
                        {
                            formNewCycle = false;
                            break;
                        }
                        if (cycleLengths[current] != 0)
                        {
                            formNewCycle = false;
                            break;
                        }

                        visitedList.Add(current);
                        current = sumOfProperDivisor[current];
                    }

                    bool cycleStarted = false;
                    int skipCount = 0;
                    List<int> cycle = new List<int>();
                    foreach (int visited in visitedList)
                    {
                        if (!cycleStarted)
                        {
                            cycleStarted = visited == current;
                        }

                        if (cycleStarted)
                        {
                            cycleLengths[visited] = formNewCycle ? visitedList.Count - skipCount : -1;
                            cycle.Add(visited);
                        }
                        else
                        {
                            cycleLengths[visited] = -1;
                            skipCount++;
                        }
                    }
                    if (cycleStarted)
                    {
                        yield return cycle;
                    }
                }
            }
        }
    }
}
