namespace Problem051
{
    using Common;
    using System.Linq;
    using System;
    using System.Collections.Generic;

    class Program
    {
        static void Main(string[] args)
        {
            var primes = EulerUtil.Primes(1000000).ToList();
            var primeSet = new HashSet<int>(primes);

            foreach (var prime in primes)
            {
                var primeDigits = prime.ToString().Select(c => c - '0').ToList();
                //Console.WriteLine("Prime: " + primeDigits.ToConcatString());
                foreach (var mask in EulerUtil.MultiRadixNumbers(Enumerable.Range(1, primeDigits.Count).Select(x => 1).ToList()))
                {
                    if (mask.ToArray().Distinct().Count() != 1)
                    {   
                        var primeDigitMasked = EulerUtil.StackLists(primeDigits, mask);
                        var maskingDigits = primeDigitMasked.Where(t => t.Item2 == 1).Select(t => t.Item1).ToArray();
                        //Console.WriteLine("  Mask: " + mask.ToConcatString() + ", Masking digits = " + maskingDigits.ToConcatString());
                        var distinctMaskingDigits = maskingDigits.Distinct().ToArray();
                        if (distinctMaskingDigits.Length == 1)
                        {
                            List<int> primeFamily = new List<int> { prime };
                            // Always replace by at least one larger
                            int startingDigit = distinctMaskingDigits[0] + 1;
                            foreach (int maskValue in Enumerable.Range(startingDigit, 10 - startingDigit))
                            {
                                var maskedPrime = primeDigitMasked.Select(t => t.Item2 == 0 ? t.Item1 : maskValue).Aggregate((x, y) => x * 10 + y);
                                if (primeSet.Contains(maskedPrime))
                                {
                                    primeFamily.Add(maskedPrime);
                                }
                            }
                            if (primeFamily.Count == 8)
                            {
                                Console.WriteLine("    Family: " + primeFamily.ToConcatString(" "));
                                return;
                            }
                        }
                    }

                    //Console.WriteLine();
                }
            }
        }
    }
}
