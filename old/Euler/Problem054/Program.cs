namespace Problem054
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using Common;

    public class Card
    {
        public class CardValue
        {
            public enum SpecialValues
            {
                T = 10,
                J = 11,
                Q = 12,
                K = 13,
                A = 14,
            }

            SpecialValues? specialValue;
            int? value;

            // Exactly one of them be null
            public SpecialValues? SpecialValue
            {
                get { return this.specialValue; }
                set { this.value = null; this.specialValue = value; }
            }

            public int? Value
            {
                get { return this.value; }
                set { this.specialValue = null; this.value = value; }
            }

            public int CanonicalizedValue
            {
                get
                {
                    if (this.specialValue.HasValue)
                    {
                        return (int)this.specialValue.Value;
                    }
                    else
                    {
                        return value.Value;
                    }
                }
            }

            public static CardValue Read(char cardValue)
            {
                if (cardValue >= '2' && cardValue <= '9')
                {
                    return new CardValue { Value = cardValue - '0' };
                }
                else
                {
                    return new CardValue { SpecialValue = (SpecialValues)Enum.Parse(typeof(SpecialValues), cardValue + string.Empty) };
                }
            }
        }

        public enum CardSuit
        {
            D = 0,
            C = 1,
            H = 2,
            S = 3,
        }

        public CardValue Value { get; set; }
        public CardSuit Suit { get; set; }

        public static Card Read(string card)
        {
            if (card.Length > 2)
            {
                throw new ArgumentException("card");
            }
            else
            {
                return new Card
                {
                    Value = CardValue.Read(card[0]),
                    Suit = (CardSuit)Enum.Parse(typeof(CardSuit), card[1] + string.Empty)
                };
            }

        }
    }

    enum HandTypes
    {
        HighCard,
        OnePair,
        TwoPairs,
        ThreeOfAKind,
        Straight,
        Flush,
        FullHouse,
        FourOfAKind,
        StraightFlush,
        RoyalFlush,
    }

    class Program
    {
        static void Main(string[] args)
        {
            string content = EulerUtil.ReadResourceAsString("Problem054.poker.txt").Trim();
            string[] matchStrings = content.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            var seen = new Dictionary<HandTypes, List<Card[]>>();
            var sameRank = new HashSet<HandTypes>();

            int winCount = 0;
            int rankWinCount = 0;
            int rankLoseCount = 0;
            int loseCount = 0;
            foreach (var matchString in matchStrings)
            {
                string realMatchString = matchString;
                Card[] deal = realMatchString.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Select(t => Card.Read(t)).ToArray();
                Card[] player1 = deal.Take(5).ToArray();
                Card[] player2 = deal.Skip(5).ToArray();
                HandTypes player1Type = GetHandType(player1);
                HandTypes player2Type = GetHandType(player2);

                Record(seen, player1, player1Type);
                Record(seen, player2, player2Type);

                if (player1Type > player2Type)
                {
                    winCount++;
                    rankWinCount++;
                }
                else if (player1Type < player2Type)
                {
                    loseCount++;
                    rankLoseCount++;
                }
                else
                {
                    Debug.Assert(player1Type == player2Type);
                    // The loose rule for comparing hands is justified because the test data 
                    // has only HighCard and OnePair if the rank matches
                    sameRank.Add(player1Type);
                    var player1Array = GetArrayForCompare(player1);
                    var player2Array = GetArrayForCompare(player2);
                    if (CompareArray(player1Array, player2Array) > 0)
                    {
                        //Console.WriteLine(player1Array.Aggregate("", (x, y) => x + "\t" + y));
                        //Console.WriteLine(player2Array.Aggregate("", (x, y) => x + "\t" + y));
                        winCount++;
                    }
                    else
                    {
                        loseCount++;
                    }
                }
            }

            //Console.WriteLine("Rank Win  Count: " + rankWinCount);
            //Console.WriteLine("Rank Lose Count: " + rankLoseCount);
            Console.WriteLine("Win       Count: " + winCount);
            //Console.WriteLine("Lost      Count: " + loseCount);
            //Console.WriteLine("Game      Count: " + (winCount + loseCount));
            //Console.WriteLine(sameRank.Aggregate("", (x, y) => x + "\t" + y));
            //foreach (HandTypes handType in seen.Keys)
            //{
            //    Console.WriteLine(handType);
            //    foreach (var hand in seen[handType])
            //    {
            //        Console.WriteLine(hand.Select(c => c.Suit.ToString() + c.Value.CanonicalizedValue).Aggregate("", (x, y) => x + "\t" + y));
            //    }
            //}
        }

        private static void Record(Dictionary<HandTypes, List<Card[]>> seen, Card[] hand, HandTypes handType)
        {
            List<Card[]> temp;
            if (!seen.TryGetValue(handType, out temp))
            {
                temp = new List<Card[]>();
                seen.Add(handType, temp);
            }
            temp.Add(hand);
        }

        private static int[] GetArrayForCompare(Card[] player1)
        {
            var player1Grouped = player1.GroupBy(t => t.Value.CanonicalizedValue).ToArray();
            var pair = player1Grouped.Where(t => t.Count() > 1).Select(t => t.Key);
            var remaining = player1Grouped.Where(t => t.Count() == 1).Select(t => t.Key).ToArray();
            Array.Sort(remaining);
            var combined = pair.Concat(remaining.Reverse()).ToArray();
            return combined;
        }

        static int CompareArray(int[] first, int[] second)
        {
            Debug.Assert(first.Length == second.Length);
            for (int i = 0; i < first.Length; i++)
            {
                int diff = first[i] - second[i];
                if (diff != 0)
                {
                    return diff;
                }
            }
            return 0;
        }

        static HandTypes GetHandType(Card[] hand)
        {
            bool flush = (hand.Select(t => t.Suit).Distinct().Count() == 1);
            int[] values = hand.Select(t => t.Value.CanonicalizedValue).ToArray();
            Array.Sort(values);
            bool straight = false;
            if ((values[1] == values[0] + 1) && (values[2] == values[1] + 1) && (values[3] == values[2] + 1) && (values[4] == values[3] + 1))
            {
                straight = true;
            }

            if ((values[0] == 2) && (values[1] == 3) && (values[2] == 4) && (values[3] == 5) && (values[4] == 14))
            {
                straight = true;
            }

            if (straight && flush)
            {
                if (values[4] == 14 && values[3] == 13)
                {
                    return HandTypes.RoyalFlush;
                }
                else
                {
                    return HandTypes.StraightFlush;
                }
            }

            int[] valueGroupSizes = values.GroupBy(t => t).Select(t => t.Count()).ToArray();
            Array.Sort(valueGroupSizes);

            if (valueGroupSizes.Length == 2)
            {
                if (valueGroupSizes[0] == 1)
                {
                    return HandTypes.FourOfAKind;
                }
                else
                {
                    Debug.Assert(valueGroupSizes[0] == 2);
                    return HandTypes.FullHouse;
                }
            }

            if (flush)
            {
                return HandTypes.Flush;
            }

            if (straight)
            {
                return HandTypes.Straight;
            }

            if (valueGroupSizes.Length == 3)
            {
                if (valueGroupSizes[2] == 3)
                {
                    return HandTypes.ThreeOfAKind;
                }
                else
                {
                    Debug.Assert(valueGroupSizes[2] == 2);
                    return HandTypes.TwoPairs;
                }
            }
            if (valueGroupSizes.Length == 4)
            {
                return HandTypes.OnePair;
            }

            return HandTypes.HighCard;
        }        
    }
}
