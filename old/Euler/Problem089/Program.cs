using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

namespace Problem089
{
    class Program
    {
        static void Main(string[] args)
        {
            string inputRomanString = EulerUtil.ReadResourceAsString("Problem089.roman.txt");
            string[] inputRomans = inputRomanString.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            Dictionary<char, int> charValues = new Dictionary<char, int>
            {
                { 'M', 1000 },
                { 'D', 500  },
                { 'C', 100  },
                { 'L', 50   },
                { 'X', 10   },
                { 'V', 5    },
                { 'I', 1    },
            };
            int saved = 0;
            foreach (string inputRoman in inputRomans)
            {
                int value = ReadRoman(charValues, inputRoman);
                string roman = ToRoman(value);
                saved += inputRoman.Length - roman.Length;
                //Console.WriteLine(inputRoman + "\t" + value + "\t" + roman);
            }
            Console.WriteLine(saved);
        }

        private static int ReadRoman(Dictionary<char, int> charValues, string romanValue)
        {
            int value = 0;
            for (int i = 0; i < romanValue.Length; i++)
            {
                int charValue = charValues[romanValue[i]];
                // check if this is a subtractive pair
                if (i + 1 != romanValue.Length)
                {
                    if (charValues[romanValue[i + 1]] > charValue)
                    {
                        charValue = -charValue;
                    }
                }
                value += charValue;
            }
            return value;
        }

        private static string ToRoman(int romanValue)
        {
            StringBuilder result = new StringBuilder();
            if (romanValue >= 5000)
            {
                throw new Exception("boom!");
            }

            switch (romanValue / 1000)
            {
                case 0: break;
                case 1: result.Append('M'); break;
                case 2: result.Append("MM"); break;
                case 3: result.Append("MMM"); break;
                case 4: result.Append("MMMM"); break;
            }

            romanValue = romanValue % 1000;

            switch (romanValue / 100)
            {
                case 0: break;
                case 1: result.Append("C"); break;
                case 2: result.Append("CC"); break;
                case 3: result.Append("CCC"); break;
                case 4: result.Append("CD"); break;
                case 5: result.Append("D"); break;
                case 6: result.Append("DC"); break;
                case 7: result.Append("DCC"); break;
                case 8: result.Append("DCCC"); break;
                case 9: result.Append("CM"); break;
            }

            romanValue = romanValue % 100;

            switch (romanValue / 10)
            {
                case 0: break;
                case 1: result.Append("X"); break;
                case 2: result.Append("XX"); break;
                case 3: result.Append("XXX"); break;
                case 4: result.Append("XL"); break;
                case 5: result.Append("L"); break;
                case 6: result.Append("LX"); break;
                case 7: result.Append("LXX"); break;
                case 8: result.Append("LXXX"); break;
                case 9: result.Append("XC"); break;
            }

            romanValue = romanValue % 10;

            switch (romanValue)
            {
                case 0: break;
                case 1: result.Append("I"); break;
                case 2: result.Append("II"); break;
                case 3: result.Append("III"); break;
                case 4: result.Append("IV"); break;
                case 5: result.Append("V"); break;
                case 6: result.Append("VI"); break;
                case 7: result.Append("VII"); break;
                case 8: result.Append("VIII"); break;
                case 9: result.Append("IX"); break;
            }

            return result.ToString();
        }
    }
}
