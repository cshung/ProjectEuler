namespace Problem059
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Common;

    class Program
    {
        // The algorithm is simply trying to find a key such that the output has least number of non-alphabets.
        // It worked because most English text contains very few non-alpha characters
        static void Main(string[] args)
        {
            string content = EulerUtil.ReadResourceAsString("Problem059.cipher1.txt").Trim();
            byte[] cipherText = content.Split(',').Select(t => byte.Parse(t)).ToArray();
            byte[] plainText = new byte[cipherText.Length];

            List<Tuple<int, string, string>> solutions = new List<Tuple<int, string, string>>();

            for (char key1 = 'a'; key1 <= 'z'; key1++)
            {
                for (char key2 = 'a'; key2 <= 'z'; key2++)
                {
                    for (char key3 = 'a'; key3 <= 'z'; key3++)
                    {
                        int nonAlphaCount = 0;

                        byte[] key = new byte[] { (byte)key1, (byte)key2, (byte)key3 };
                        int j = 0;
                        for (int i = 0; i < cipherText.Length; i++)
                        {
                            plainText[i] = (byte)(cipherText[i] ^ key[j]);
                            if (plainText[i] < 'A' || plainText[i] > 'Z')
                            {
                                if (plainText[i] < 'a' || plainText[i] > 'z')
                                {
                                    nonAlphaCount++;
                                }
                            }

                            j = (j + 1) % 3;
                        }
                        string plainString = Encoding.ASCII.GetString(plainText);
                        solutions.Add(Tuple.Create(nonAlphaCount, (key1.ToString() + key2 + key3), plainString));
                    }
                }
            }

            int optimal = solutions.Select(t => t.Item1).Min();
            var solution = solutions.Single(t => t.Item1 == optimal);

            string correctKey = solution.Item2;
            string correctPlainText = solution.Item3;
            Console.WriteLine(correctKey);
            Console.WriteLine(correctPlainText);
            
            Console.WriteLine(Encoding.ASCII.GetBytes(correctPlainText).Select(x => (int)x).Aggregate((x, y) => x + y));
        }
    }
}
