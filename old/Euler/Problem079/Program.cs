using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem079
{
    class Program
    {
        static void Main(string[] args)
        {
            string loginString = EulerUtil.ReadResourceAsString("Problem079.keylog.txt");
            string[] loginAttempts = loginString.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            // The basic idea is to find a topological sorting of the keys 
            Dictionary<char, HashSet<char>> graph = new Dictionary<char, HashSet<char>>();
            foreach (string loginAttempt in loginAttempts)
            {
                if (!graph.ContainsKey(loginAttempt[2]))
                {
                    graph[loginAttempt[2]] = new HashSet<char>();
                }
                AddToGraph(graph, loginAttempt[0], loginAttempt[1]);
                AddToGraph(graph, loginAttempt[1], loginAttempt[2]);
            }

            // Topological sort
            // for each node in the graph - if it is not depended by anyone, split it out
            bool found;
            do
            {
                found = false;
                HashSet<char> pointed = new HashSet<char>(graph.Select(kvp => (IEnumerable<char>)kvp.Value).Aggregate((IEnumerable<char>)new List<char>(), (x, y) => x.Concat(y)));
                char[] candidates = graph.Keys.Where(k => !pointed.Contains(k)).ToArray();
                foreach (char candidate in candidates)
                {
                    Console.Write(candidate);
                    graph.Remove(candidate);
                    found = true;
                }
            } while (found);
            Console.WriteLine();
        }

        private static void AddToGraph(Dictionary<char, HashSet<char>> graph, char from, char to)
        {
            HashSet<char> dependencies;
            if (!graph.TryGetValue(from, out dependencies))
            {
                dependencies = new HashSet<char>();
                graph.Add(from, dependencies);
            }

            dependencies.Add(to);
        }
    }
}
