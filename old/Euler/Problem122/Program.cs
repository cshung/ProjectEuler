namespace Problem122
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Common;
    using System.Text;

    class Program
    {
        static class NodeManager
        {
            //const int cacheSize = 67100000;
            const int cacheSize = Int32.MaxValue;
            static List<Node> nodes = new List<Node>();
            static FileStream fs = new FileStream(@"c:\nodes.bin", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.None);

            public static Node ReadNode(int index)
            {
                if (index < cacheSize)
                {
                    return nodes[index];
                }
                else
                {
                    // Turn out we don't really need the File I/O, but just in case, here is the code for doing File I/O
                    index -= cacheSize;
                    byte[] buffer = new byte[5];
                    fs.Seek(index * 5, SeekOrigin.Begin);
                    fs.Read(buffer, 0, 5);
                    byte value = buffer[0];
                    int parentIndex = BitConverter.ToInt32(buffer, 1);
                    return new Node(value, parentIndex);
                }
            }

            public static void CreateNode(byte value, int parentIndex)
            {
                Node node = new Node(value, parentIndex);
                if (nodes.Count < cacheSize)
                {
                    nodes.Add(node);
                }
                else
                {
                    byte[] buffer = new byte[5];
                    buffer[0] = value;
                    byte[] parentIndexBytes = BitConverter.GetBytes(parentIndex);
                    buffer[1] = parentIndexBytes[0];
                    buffer[2] = parentIndexBytes[1];
                    buffer[3] = parentIndexBytes[2];
                    buffer[4] = parentIndexBytes[3];
                    fs.Seek(0, SeekOrigin.End);
                    fs.Write(buffer, 0, 5);
                    fs.Flush();
                }
            }
        }

        class Node
        {
            public Node(byte value, int parentIndex)
            {
                this.Value = value;
                this.ParentIndex = parentIndex;
            }

            public byte Value { get; private set; }

            public int ParentIndex { get; private set; }

            public IEnumerable<byte> Data
            {
                get
                {
                    if (this.ParentIndex != -1)
                    {
                        foreach (byte parentValue in NodeManager.ReadNode(this.ParentIndex).Data)
                        {
                            yield return parentValue;
                        }
                    }

                    yield return Value;
                }
            }
        }

        static void Main(string[] args)
        {
            File.Delete(@"c:\nodes.bin");
            int max = 200;

            int datasum = LookupDatabase(max);
            Console.WriteLine(datasum - max);

            NodeManager.CreateNode(1, -1);
            int queueStart = 0;
            int queueEnd = 0;
            Dictionary<int, int> chainLengths = new Dictionary<int, int>();
            chainLengths.Add(1, 1);
            //var chainsMap = new SortedDictionary<byte, List<List<byte>>>();
            while (true)
            {
                Node current = NodeManager.ReadNode(queueStart++);
                byte[] data = current.Data.ToArray();
                SortedSet<byte> neighbors = new SortedSet<byte>();
                for (int i = 0; i < data.Length; i++)
                {
                    for (int j = i; j < data.Length; j++)
                    {
                        int sum = data[i] + data[j];
                        if (sum <= max)
                        {
                            byte sumb = (byte)sum;
                            if (!data.Contains(sumb) && sum <= max)
                            {
                                neighbors.Add(sumb);
                            }
                        }
                    }
                }

                foreach (byte sum in neighbors.Reverse())
                {
                    var currentPath = data.Concat(new byte[] { sum }).ToList();
                    int chainLength = currentPath.Count;
                    if (!chainLengths.ContainsKey(sum) && sum <= max)
                    {
                        chainLengths.Add(sum, chainLength);
                        Console.WriteLine("{0}\t{1}\t{2}", chainLengths.Count, sum, chainLength);
                        if (chainLengths.Count >= max)
                        {
                            Console.WriteLine(chainLengths.Values.Sum() - max);
                            // Showing the chains
                            //foreach (var chains in chainsMap)
                            //{
                            //    Console.WriteLine(chains.Key);
                            //    foreach (var chain in chains.Value)
                            //    {
                            //        Console.WriteLine("  " + chain.ToConcatString(" "));
                            //    }
                            //}
                            return;
                        }
                    }

                    // If the currently found chain is of minimal length - save it
                    //if (chainLengths.ContainsKey(sum) && chainLengths[sum] == chainLength)
                    //{
                    //    List<List<byte>> chains;
                    //    if (!chainsMap.TryGetValue(sum, out chains))
                    //    {
                    //        chains = new List<List<byte>>();
                    //        chainsMap.Add(sum, chains);
                    //    }
                    //    chains.Add(currentPath);
                    //}

                    NodeManager.CreateNode(sum, queueStart - 1);
                    queueEnd++;
                }
            }
        }

        private static int LookupDatabase(int max)
        {
            string database = EulerUtil.ReadResourceAsString("Problem122.acIndx.txt");
            string[] lines = database.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            int datasum = 0;
            for (int i = 0; i < max; i++)
            {
                int length = int.Parse(lines[i].Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1]);
                datasum += length;
            }

            return datasum;
        }
    }
}
