namespace Problem060
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(DateTime.Now);
            int max = 10000;
            int primeMax = 5000 * 5000;
            var primeSet = new HashSet<int>(EulerUtil.Primes(primeMax));
            Console.WriteLine(DateTime.Now + "Build prime list");
            var primes = EulerUtil.Primes(max).ToArray();
            var goodPairs = new List<Tuple<int, int>>();
            for (int aIndex = 0; aIndex < primes.Length; aIndex++)
            {
                for (int bIndex = aIndex + 1; bIndex < primes.Length; bIndex++)
                {
                    int a = primes[aIndex];
                    int b = primes[bIndex];
                    int first = int.Parse(a + "" + b);
                    int second = int.Parse(b + "" + a);
                    if (((first < primeMax && primeSet.Contains(first)) || EulerUtil.IsPrime(first)) && ((second < primeMax && primeSet.Contains(second)) || EulerUtil.IsPrime(second)))
                    {
                        goodPairs.Add(Tuple.Create(a, b));
                        // TODO, be more space efficient
                        goodPairs.Add(Tuple.Create(b, a));
                    }
                }
            }

            Console.WriteLine(DateTime.Now + " Found good pairs");

            var graph = new Dictionary<int, HashSet<int>>();
            foreach (var goodPair in goodPairs)
            {
                HashSet<int> neighbor;
                if (!graph.TryGetValue(goodPair.Item1, out neighbor))
                {
                    neighbor = new HashSet<int>();
                    graph.Add(goodPair.Item1, neighbor);
                }
                neighbor.Add(goodPair.Item2);
            }

            Console.WriteLine(DateTime.Now + " Built graph");

            var sorted = graph.Select(t => Tuple.Create(t.Key, t.Value.Count)).OrderBy(t => t.Item2).Select(t => t.Item1).ToList();
            while (sorted.Count > 0)
            {
                int considering = sorted.First();
                HashSet<int> consideringNeighbors = graph[considering];
                sorted.RemoveAt(0);

                // If considering has less than four children, it does not worth considering at all
                if (consideringNeighbors.Count < 4)
                {
                    graph.Remove(considering);
                    foreach (int consideringNeighbor in consideringNeighbors)
                    {
                        // These nodes should not consider [considering] as a neighbor anymore
                        graph[consideringNeighbor].Remove(considering);
                    }
                }
                else
                {
                    bool partOfClique = false;
                    foreach (var combination in EulerUtil.Combinations(consideringNeighbors, 4))
                    {
                        int[] combinationArray = combination.ToArray();
                        int a, b, c, d;
                        a = combinationArray[0];
                        b = combinationArray[1];
                        c = combinationArray[2];
                        d = combinationArray[3];
                        if (graph[a].Contains(b))
                        {
                            if (graph[a].Contains(c))
                            {
                                if (graph[a].Contains(d))
                                {
                                    if (graph[b].Contains(c))
                                    {
                                        if (graph[b].Contains(d))
                                        {
                                            if (graph[c].Contains(d))
                                            {
                                                partOfClique = true;
                                                Console.WriteLine(a + "  " + b + " " + c + " " + d + " " + considering);
                                                Console.WriteLine("Found!");
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }

                    if (!partOfClique)
                    {
                        graph.Remove(considering);
                        foreach (int consideringNeighbor in consideringNeighbors)
                        {
                            // These nodes should not consider [considering] as a neighbor anymore
                            graph[consideringNeighbor].Remove(considering);
                        }
                    }
                }
            }
        }
    }
}
