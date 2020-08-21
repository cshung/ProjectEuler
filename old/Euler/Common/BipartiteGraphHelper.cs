namespace Common
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    public static class BipartiteGraphHelper
    {
        public static List<Tuple<U, V>> Matching<U, V>(Dictionary<U, HashSet<V>> graph)
            where U : class
            where V : class
        {
            return BipartiteGraphHelper<U, V>.Matching(graph);
        }
    }

    internal static class BipartiteGraphHelper<U, V>
        where U : class
        where V : class
    {
        public static List<Tuple<U, V>> Matching(Dictionary<U, HashSet<V>> graph)
        {
            // First - making the graph easier to work with by having both U and V objects treated the same way internally as a node.
            // This is because the algorithm need to do BFS on the graph and would be very inconvenient to work with more than one type
            Dictionary<Node, List<Node>> nodeGraph = BuildNodeGraph(graph);

            // The current matching, initialized to empty
            Dictionary<Node, Node> matching = new Dictionary<Node, Node>();

            while (true)
            {
                HashSet<Node> unmatchedNodes = new HashSet<Node>(nodeGraph.Keys.Where(t => !matching.ContainsKey(t)));
                List<Node> augmentingPath = null;
                foreach (Node unmatchedNode in unmatchedNodes)
                {
                    // Finding augmenting path using BFS
                    HashSet<Node> visitedNodes = new HashSet<Node>();
                    Dictionary<Node, Node> parents = new Dictionary<Node, Node>();
                    // The bool indicates the requirement for the next edge to be (false: unmatched) (true: matched)
                    Queue<Tuple<Node, bool>> bfsQueue = new Queue<Tuple<Node, bool>>();
                    bfsQueue.Enqueue(Tuple.Create(unmatchedNode, false));
                    bool firstIteration = true;
                    while (bfsQueue.Count > 0)
                    {
                        var visitingPair = bfsQueue.Dequeue();
                        Node visiting = visitingPair.Item1;
                        bool requireMatchedEdge = visitingPair.Item2;
                        //visitedNodes.Add(visiting);

                        if (!firstIteration)
                        {
                            if (unmatchedNodes.Contains(visiting))
                            {
                                // Walking the parent chain to get to the actual augmenting path
                                Node current = visiting;
                                Node next;
                                augmentingPath = new List<Node>();
                                while (parents.TryGetValue(current, out next))
                                {
                                    augmentingPath.Insert(0, current);
                                    current = next;
                                }
                                augmentingPath.Insert(0, current);
                                break;
                            }
                        }
                        firstIteration = false;

                        // TODO, this is not right, it must be alternating
                        if (requireMatchedEdge)
                        {
                            Node neighbor;
                            if (matching.TryGetValue(visiting, out neighbor))
                            {
                                if (!visitedNodes.Contains(neighbor))
                                {
                                    visitedNodes.Add(neighbor);
                                    parents.Add(neighbor, visiting);
                                    bfsQueue.Enqueue(Tuple.Create(neighbor, false));
                                }
                            }
                        }
                        else
                        {
                            foreach (Node neighbor in nodeGraph[visiting])
                            {
                                if (!visitedNodes.Contains(neighbor))
                                {
                                    visitedNodes.Add(neighbor);
                                    parents.Add(neighbor, visiting);
                                    bfsQueue.Enqueue(Tuple.Create(neighbor, true));
                                }
                            }
                        }
                    }
                    if (augmentingPath != null)
                    {
                        break;
                    }
                }
                if (augmentingPath != null)
                {
                    // Now - creating a matching based on the augmenting path
                    //Console.WriteLine("Found augmenting path: " + augmentingPath.Aggregate("", (x, y) => x.ToString() + " " + y.ToString()));
                    Node[] augmentingPathArray = augmentingPath.ToArray();
                    bool isInOriginalMatching = false;

                    // Remove all the existing edges
                    for (int i = 0; i < augmentingPathArray.Length - 1; i++)
                    {
                        if (isInOriginalMatching)
                        {
                            matching.Remove(augmentingPathArray[i]);
                            matching.Remove(augmentingPathArray[i + 1]);
                        }
                        isInOriginalMatching = !isInOriginalMatching;
                    }
                    // And add all the new ones
                    isInOriginalMatching = false;
                    for (int i = 0; i < augmentingPathArray.Length - 1; i++)
                    {
                        if (!isInOriginalMatching)
                        {
                            matching[augmentingPathArray[i]] = augmentingPathArray[i + 1];
                            matching[augmentingPathArray[i + 1]] = augmentingPathArray[i];
                        }
                        isInOriginalMatching = !isInOriginalMatching;
                    }
                }
                else
                {
                    break;
                }
            }

            List<Tuple<U, V>> result = new List<Tuple<U, V>>();
            foreach (var match in matching)
            {
                if (match.Key.U != null)
                {
                    result.Add(Tuple.Create(match.Key.U, match.Value.V));
                }
            }
            return result;
        }

        private static Dictionary<Node, List<Node>> BuildNodeGraph(Dictionary<U, HashSet<V>> graph)
        {
            Dictionary<Node, List<Node>> nodeGraph = new Dictionary<Node, List<Node>>();
            foreach (var row in graph)
            {
                foreach (var cell in row.Value)
                {
                    // When doing matching - the direction is not important
                    AddEdge(nodeGraph, new Node(row.Key), new Node(cell));
                    AddEdge(nodeGraph, new Node(cell), new Node(row.Key));
                }
            }
            return nodeGraph;
        }

        private static void AddEdge(Dictionary<Node, List<Node>> nodeGraph, Node source, Node target)
        {
            List<Node> neighbors;
            if (!nodeGraph.TryGetValue(source, out neighbors))
            {
                neighbors = new List<Node>();
                nodeGraph.Add(source, neighbors);
            }

            neighbors.Add(target);
        }

        class Node
        {
            private U u;
            private V v;

            public Node(U u)
            {
                if (u == null)
                {
                    throw new ArgumentNullException("u");
                }

                this.u = u;
            }

            public Node(V v)
            {
                if (v == null)
                {
                    throw new ArgumentNullException("v");
                }

                this.v = v;
            }

            public U U { get { return u; } }

            public V V { get { return v; } }

            public override bool Equals(object obj)
            {
                Node that = obj as Node;
                if (that != null)
                {
                    return (object.Equals(this.u, that.u)) && (object.Equals(this.v, that.v));
                }

                return false;
            }

            public override int GetHashCode()
            {
                if (this.u != null)
                {
                    return u.GetHashCode();
                }
                else
                {
                    Debug.Assert(this.v != null);
                    return v.GetHashCode();
                }
            }

            public override string ToString()
            {
                if (this.u != null)
                {
                    return u.ToString();
                }
                else
                {
                    Debug.Assert(this.v != null);
                    return v.ToString();
                }
            }
        }
    }
}
