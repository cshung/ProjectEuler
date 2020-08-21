namespace Problem098
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Common;
    using System.Text;

    class Program
    {
        static void Main(string[] args)
        {
            IEnumerable<string> squares = Enumerable.Range(1, 100000).Select(t => t * t).Select(t => t.ToString());
            IEnumerable<IGrouping<string, string>> squareAnagramGroups = squares.GroupBy(t => t.OrderBy(u => u).Select(u => "" + u).Aggregate((x, y) => x + y)).Where(k => k.Count() > 1);

            // With this, we understand there are quite a lot of square anagrams - there are 21163 groups of them
            // Console.WriteLine(squareAnagramGroups.Count());

            Dictionary<int, List<IGrouping<string, string>>> indexedSquareAnagramGroups = new Dictionary<int, List<IGrouping<string, string>>>();
            foreach (var squareAnagramGroup in squareAnagramGroups)
            {
                List<IGrouping<string, string>> squareAnagramGroupList;
                if (!indexedSquareAnagramGroups.TryGetValue(squareAnagramGroup.Key.Length, out squareAnagramGroupList))
                {
                    squareAnagramGroupList = new List<IGrouping<string, string>>();
                    indexedSquareAnagramGroups.Add(squareAnagramGroup.Key.Length, squareAnagramGroupList);
                }

                squareAnagramGroupList.Add(squareAnagramGroup);
            }

            string input = EulerUtil.ReadResourceAsString("Problem098.words.txt");
            IEnumerable<string> words = input.Split(',').Select(t => t.Substring(1, t.Length - 2));
            var wordAnagramGroups = words.GroupBy(t => t.OrderBy(u => u).Select(u => "" + u).Aggregate((x, y) => x + y)).Where(k => k.Count() > 1);

            // With this, we understand the longest length is just 9
            //Console.WriteLine(wordAnagramGroups.Select(g => g.Key.Length).Max());


            Console.WriteLine(CheckAnagrams(indexedSquareAnagramGroups, wordAnagramGroups).Max());
        }

        private static IEnumerable<int> CheckAnagrams(Dictionary<int, List<IGrouping<string, string>>> indexedSquareAnagramGroups, IEnumerable<IGrouping<string, string>> wordAnagramGroups)
        {
            foreach (var group in wordAnagramGroups)
            {
                foreach (var pair in EulerUtil.Combinations(group, 2))
                {
                    string[] pairs = pair.ToArray();
                    List<IGrouping<string, string>> squareAnagramGroups;
                    if (indexedSquareAnagramGroups.TryGetValue(pairs[0].Length, out squareAnagramGroups))
                    {
                        foreach (int result in Check(pairs[0], pairs[1], squareAnagramGroups))
                        {
                            yield return result;
                        }
                    }
                }
            }
        }

        private static IEnumerable<int> Check(string first, string second, List<IGrouping<string, string>> squareAnagramGroups)
        {
            foreach (var squareAnagramGroup in squareAnagramGroups)
            {
                foreach (string firstSquareCandidate in squareAnagramGroup)
                {
                    // Map first to firstSquareCandidate
                    Dictionary<char, char> map;
                    if (TryBuildMap(first, firstSquareCandidate, out map))
                    {
                        // Use the map - build the digit representation of second
                        StringBuilder sb = new StringBuilder(); 
                        foreach (char c in second)
                        {
                            sb.Append(map[c]);
                        }
                        string secondMapped = sb.ToString();
                        if (squareAnagramGroup.Contains(secondMapped))
                        {
                            //Console.WriteLine(first + " can be mapped to " + firstSquareCandidate);
                            //Console.WriteLine(second + " can be mapped to " + secondMapped);
                            //Console.WriteLine("SQUARE ANAGRAM FOUND!");
                            yield return int.Parse(firstSquareCandidate);
                            yield return int.Parse(secondMapped);
                        }
                    }
                }
            }
            
        }

        public static bool TryBuildMap(string first, string second, out Dictionary<char, char> forwardMap)
        {
            forwardMap = new Dictionary<char, char>();
            var backwardMap = new Dictionary<char, char>();
            if (first.Length != second.Length)
            {
                return false;
            }
            int length = first.Length;
            for (int i = 0; i < length; i++)
            {
                char was;
                if (forwardMap.TryGetValue(first[i], out was))
                {
                    if (was != second[i])
                    {
                        return false;
                    }
                }
                else
                {
                    forwardMap.Add(first[i], second[i]);
                }
                if (backwardMap.TryGetValue(second[i], out was))
                {
                    if (was != first[i])
                    {
                        return false;
                    }
                }
                else
                {
                    backwardMap.Add(second[i], first[i]);
                }
            }
            return true;
        }
    }

}
