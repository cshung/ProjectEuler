using System;
using System.Linq;
using System.Collections.Generic;
using System.Diagnostics;

namespace Problem084
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] cells = "GO,A1,CC1,A2,T1,R1,B1,CH1,B2,B3,JAIL,C1,U1,C2,C3,R2,D1,CC2,D2,D3,FP,E1,CH2,E2,E3,R3,F1,F2,U2,F3,G2J,G1,G2,CC3,G3,R4,CH3,H1,T2,H2".Split(',');
            List<string> ccCards = new string[] { "Advance to GO", "Go to JAIL", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy" }.ToList();
            List<string> chCards = new string[] { "Advance to GO", "Go to JAIL", "Go to C1", "Go to E3", "Go to H2", "Go to R1", "Go to next R", "Go to next R", "Go to next U", "Go back 3 squares", "dummy", "dummy", "dummy", "dummy", "dummy", "dummy" }.ToList();

            int jailIndex = IndexOf(cells, "JAIL");
            int[] hitCount = new int[cells.Length];

            int position = 0;
            string cell = cells[position];
            Random random = new Random(6);

            int simStep = 50000;
            int diceSize = 4;
            int doubleCount = 0;
            for (int e = 0; e < 100; e++)
            {
                ShuffleCommunityChestCards(ccCards, random);
                ShuffleChanceCards(chCards, random);

                for (int i = 0; i < simStep; i++)
                {
                    int dice1 = random.Next(diceSize) + 1;
                    int dice2 = random.Next(diceSize) + 1;
                    if (dice1 == dice2)
                    {
                        doubleCount++;
                    }
                    else
                    {
                        doubleCount = 0;
                    }
                    if (doubleCount == 3)
                    {
                        doubleCount = 0;
                        position = jailIndex;
                        cell = cells[position];
                    }
                    else
                    {
                        int move = dice1 + dice2;
                        position = (position + move) % cells.Length;
                        cell = cells[position];
                        bool fled = false;
                        do
                        {
                            fled = false;
                            if (string.Equals(cell, "G2J"))
                            {
                                position = jailIndex;
                                cell = cells[position];
                                fled = true;
                            }
                            else if (cell.StartsWith("CC"))
                            {
                                HandleCommunityChest(cells, ccCards, jailIndex, ref position, ref cell, ref fled);
                            }
                            else if (cell.StartsWith("CH"))
                            {
                                HandleChance(cells, chCards, jailIndex, ref position, ref cell, ref fled);
                            }
                        } while (fled);                        
                    }

                    hitCount[position]++;
                }
            }

            List<Tuple<string, double>> hitCountList = new List<Tuple<string, double>>();
            for (int i = 0; i < hitCount.Length; i++)
            {
                hitCountList.Add(Tuple.Create(cells[i], Math.Round(hitCount[i] * 100.0 / simStep) / 100.0));
            }
            hitCountList = hitCountList.OrderBy((t => t.Item2)).ToList();
            foreach (var kvp in hitCountList)
            {
                Console.WriteLine(kvp.Item1 + "(" + IndexOf(cells, kvp.Item1) + ")\t" + kvp.Item2);
            }
        }

        private static void ShuffleChanceCards(List<string> chCards, Random random)
        {
            for (int i = chCards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i);
                string temp = chCards[j];
                chCards[j] = chCards[i];
                chCards[i] = temp;
            }
        }

        private static void ShuffleCommunityChestCards(List<string> ccCards, Random random)
        {
            for (int i = ccCards.Count - 1; i > 0; i--)
            {
                int j = random.Next(i);
                string temp = ccCards[j];
                ccCards[j] = ccCards[i];
                ccCards[i] = temp;
            }
        }

        private static void HandleChance(string[] cells, List<string> chCards, int jailIndex, ref int position, ref string cell, ref bool fled)
        {
            string chCard = chCards[0];
            chCards.RemoveAt(0);
            chCards.Add(chCard);

            if (chCard.Equals("Advance to GO"))
            {
                position = 0;
                cell = cells[position];
                fled = true;
            }
            else if (chCard.Equals("Go to JAIL"))
            {
                position = jailIndex;
                cell = cells[position];
                fled = true;
            }
            else if (chCard.Equals("Go to next R"))
            {
                do
                {
                    position = (position + 1) % cells.Length;
                    cell = cells[position];
                } while (!cell.StartsWith("R"));
                fled = true;
            }
            else if (chCard.Equals("Go to next U"))
            {
                do
                {
                    position = (position + 1) % cells.Length;
                    cell = cells[position];
                } while (!cell.StartsWith("U"));
                fled = true;
            }
            else if (chCard.Equals("Go back 3 squares"))
            {
                position = (position + cells.Length - 3) % cells.Length;
                cell = cells[position];
                fled = true;
            }
            else if (chCard.StartsWith("Go to "))
            {
                position = IndexOf(cells, chCard.Substring("Go to ".Length));
                cell = cells[position];
                fled = true;
            }
            else
            {
                Debug.Assert(chCard.Equals("dummy"));
            }
        }

        private static void HandleCommunityChest(string[] cells, List<string> ccCards, int jailIndex, ref int position, ref string cell, ref bool fled)
        {
            // draw a card from the CC stack
            string ccCard = ccCards[0];
            ccCards.RemoveAt(0);
            ccCards.Add(ccCard);

            if (ccCard.Equals("Advance to GO"))
            {
                position = 0;
                cell = cells[position];
                fled = true;
            }
            else if (ccCard.Equals("Go to JAIL"))
            {
                position = jailIndex;
                cell = cells[position];
                fled = true;
            }
            else
            {
                Debug.Assert(ccCard.Equals("dummy"));
            }
        }

        private static int IndexOf(string[] cells, string cellName)
        {
            return cells.Select((c, i) => Tuple.Create(c, i)).Where(t => string.Equals(t.Item1, cellName)).Single().Item2;
        }
    }
}
