namespace Euler
{
    using System;
    using System.Linq;
    using System.Numerics;

    internal static partial class Program
    {
        public static void Problem017()
        {   
            Console.WriteLine(Enumerable.Range(1, 1000).Select(i => ToEnglishString(i).Replace(" ", string.Empty).Length).Aggregate((x, y) => x + y));
        }

        private static string ToEnglishString(int i)
        {
            string ONE = "one";
            string TWO = "two";
            string THREE = "three";
            string FOUR = "four";
            string FIVE = "five";
            string SIX = "six";
            string SEVEN = "seven";
            string EIGHT = "eight";
            string NINE = "nine";
            string TEN = "ten";
            string ELEVEN = "eleven";
            string TWELVE = "twelve ";
            string THIRTEEN = "thirteen";
            string FOURTEEN = "fourteen";
            string FIFTEEN = "fifteen";
            string SIXTEEN = "sixteen";
            string SEVENTEEN = "seventeen";
            string EIGHTEEN = "eighteen";
            string NINETEEN = "nineteen";
            string TWENTY = "twenty";
            string THIRTY = "thirty";
            string FORTY = "forty";
            string FIFTY = "fifty";
            string SIXTY = "sixty";
            string SEVENTY = "seventy";
            string EIGHTY = "eighty";
            string NINETY = "ninety";
            string HUNDRED = "hundred";
            string THOUSAND = "thousand";
            switch (i)
            {
                case 1000: return ONE + " " + THOUSAND;
                case 1: return ONE;
                case 2: return TWO;
                case 3: return THREE;
                case 4: return FOUR;
                case 5: return FIVE;
                case 6: return SIX;
                case 7: return SEVEN;
                case 8: return EIGHT;
                case 9: return NINE;
                case 10: return TEN;
                case 11: return ELEVEN;
                case 12: return TWELVE;
                case 13: return THIRTEEN;
                case 14: return FOURTEEN;
                case 15: return FIFTEEN;
                case 16: return SIXTEEN;
                case 17: return SEVENTEEN;
                case 18: return EIGHTEEN;
                case 19: return NINETEEN;
                case 20: return TWENTY;
                default:
                    string result = string.Empty;
                    int remaining;
                    int hundredthDigit = i / 100;
                    remaining = i - hundredthDigit * 100;
                    if (hundredthDigit != 0)
                    {
                        result += ToEnglishString(hundredthDigit) + " " + HUNDRED;
                        if (remaining == 0)
                        {
                            return result;
                        }
                        else
                        {
                            result = result + " and ";
                        }
                    }

                    int tenthDigit = remaining / 10;
                    remaining = remaining - tenthDigit * 10;

                    switch (tenthDigit)
                    {
                        case 1:
                            switch (remaining)
                            {
                                case 0: result = result + TEN; break;
                                case 1: result = result + ELEVEN; break;
                                case 2: result = result + TWELVE; break;
                                case 3: result = result + THIRTEEN; break;
                                case 4: result = result + FOURTEEN; break;
                                case 5: result = result + FIFTEEN; break;
                                case 6: result = result + SIXTEEN; break;
                                case 7: result = result + SEVENTEEN; break;
                                case 8: result = result + EIGHTEEN; break;
                                case 9: result = result + NINETEEN; break;

                            };
                            remaining = 0;
                            break;
                        case 2: result = result + TWENTY; break;
                        case 3: result = result + THIRTY; break;
                        case 4: result = result + FORTY; break;
                        case 5: result = result + FIFTY; break;
                        case 6: result = result + SIXTY; break;
                        case 7: result = result + SEVENTY; break;
                        case 8: result = result + EIGHTY; break;
                        case 9: result = result + NINETY; break;
                    }
                    if (tenthDigit != 0)
                    {
                        result = result + " ";
                    }
                    if (remaining != 0)
                    {
                        result = result + ToEnglishString(remaining);
                    }
                    return result;
            }

        }
    }
}
