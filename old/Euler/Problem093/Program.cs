namespace Problem093
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Common;

    public class Program
    {
        static void Main(string[] args)
        {
            var result = Compute().ToList();
            Console.WriteLine(result.ArgMax(t => t.Item2));
        }

        static IEnumerable<Tuple<string, int>> Compute()
        {
            List<Node> trees = GenerateTree(4);
            // Number Assignment
            foreach (var combination in EulerUtil.Combinations<int>(Enumerable.Range(0, 10), 4))
            {
                HashSet<int> resultSet = new HashSet<int>();
                foreach (var permutation in EulerUtil.Permutations(combination))
                {
                    foreach (Node tree in trees)
                    {
                        List<InternalNode> internalNodes = new List<InternalNode>();
                        List<LeafNode> leafNodes = new List<LeafNode>();
                        ExtractNodes(tree, internalNodes, leafNodes);
                        foreach (var internalNodeSignAssignment in EulerUtil.MultiRadixNumbers(Enumerable.Range(1, internalNodes.Count).Select(t => 3).ToList()))
                        {
                            foreach (var match in Enumerable.Zip(internalNodes, internalNodeSignAssignment, (x, y) => Tuple.Create(x, y)))
                            {
                                switch (match.Item2)
                                {
                                    case 0: match.Item1.Sign = '+'; break;
                                    case 1: match.Item1.Sign = '-'; break;
                                    case 2: match.Item1.Sign = '*'; break;
                                    case 3: match.Item1.Sign = '/'; break;
                                }
                            }
                            foreach (var leafNodeSignAssignment in EulerUtil.MultiRadixNumbers(Enumerable.Range(1, leafNodes.Count).Select(t => 1).ToList()))
                            {
                                foreach (var match in Enumerable.Zip(leafNodes, leafNodeSignAssignment, (x, y) => Tuple.Create(x, y)))
                                {
                                    switch (match.Item2)
                                    {
                                        case 0: match.Item1.Sign = '+'; break;
                                        case 1: match.Item1.Sign = '-'; break;
                                    }
                                }
                                foreach (var match in Enumerable.Zip(leafNodes, permutation, (x, y) => Tuple.Create(x, y)))
                                {
                                    match.Item1.Value = new BigRational(match.Item2, 1);
                                }
                                BigRational result = Evaluate(tree);
                                result.Factorize();
                                //Console.WriteLine(tree + " = " + result);
                                if (result.Denominator == 1)
                                {
                                    resultSet.Add(int.Parse(result.Numerator.ToString()));
                                }
                            }
                        }
                    }
                }
                
                int i = 1;
                while (true)
                {
                    if (!resultSet.Contains(i))
                    {
                        break;
                    }
                    else
                    {
                        i++;
                    }
                }
                yield return Tuple.Create(combination.ToConcatString(), (i - 1));
            }
        }


        static List<Node> GenerateTree(int count)
        {
            List<Node> result = new List<Node>();
            if (count == 1)
            {
                result.Add(new LeafNode());
            }
            else
            {
                for (int i = 1; i < count; i++)
                {
                    int j = count - i;
                    List<Node> leftChoices = GenerateTree(i);
                    List<Node> rightChoices = GenerateTree(j);
                    foreach (Node leftChoice in leftChoices)
                    {
                        foreach (Node rightChoice in rightChoices)
                        {
                            result.Add(new InternalNode { Left = leftChoice.Clone(), Right = rightChoice.Clone() });
                        }
                    }
                }
            }
            return result;
        }

        static void ExtractNodes(Node root, List<InternalNode> internalNodes, List<LeafNode> leafNodes)
        {
            InternalNode rootAsInternalNode = root as InternalNode;
            if (rootAsInternalNode != null)
            {
                internalNodes.Add(rootAsInternalNode);
                ExtractNodes(rootAsInternalNode.Left, internalNodes, leafNodes);
                ExtractNodes(rootAsInternalNode.Right, internalNodes, leafNodes);
            }
            else
            {
                LeafNode rootAsLeafNode = root as LeafNode;
                leafNodes.Add(rootAsLeafNode);
            }
        }

        static BigRational Evaluate(Node root)
        {
            InternalNode rootAsInternalNode = root as InternalNode;
            if (rootAsInternalNode != null)
            {
                BigRational leftValue = Evaluate(rootAsInternalNode.Left);
                BigRational rightValue = Evaluate(rootAsInternalNode.Right);
                switch (rootAsInternalNode.Sign)
                {
                    case '+': return leftValue + rightValue;
                    case '-': return leftValue - rightValue;
                    case '*': return leftValue * rightValue;
                    case '/': return leftValue / rightValue;
                    default: return null;
                }
            }
            else
            {
                LeafNode rootAsLeafNode = root as LeafNode;
                return new BigRational(rootAsLeafNode.Sign == '-' ? -1 : +1, 1) * (rootAsLeafNode.Value);
            }
        }
    }

    public abstract class Node
    {
        public abstract Node Clone();
    }

    public class InternalNode : Node
    {
        public Node Left { get; set; }
        public Node Right { get; set; }

        public override Node Clone()
        {
            return new InternalNode { Left = this.Left.Clone(), Right = this.Right.Clone() };
        }

        public override string ToString()
        {
            return "(" + Left.ToString() + " " + Sign + " " + Right.ToString() + ")";
        }

        public char Sign { get; set; }
    }

    public class LeafNode : Node
    {
        public override Node Clone()
        {
            return new LeafNode();
        }

        public override string ToString()
        {
            return this.Sign + "" + this.Value;
        }
        public char Sign { get; set; }

        public BigRational Value { get; set; }
    }
}
