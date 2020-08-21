using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem038
{
    class Program
    {
        static void Main(string[] args)
        {
            List<long> concatenatedProducts = new List<long>();
            for (long i = 1; i < 10000; i++)
            {
                HashSet<int> digits = new HashSet<int>(Enumerable.Range(1, 9));
                List<long> productList = new List<long>();
                for (int n = 1; n <= 9; n++)
                {
                    long product = i * n;
                    productList.Add(product);
                    IEnumerable<int> productDigits = product.ToString().Select(c => c - '0');
                    bool broken = false;
                    foreach (int productDigit in productDigits)
                    {
                        if (digits.Contains(productDigit))
                        {
                            digits.Remove(productDigit);
                        }
                        else
                        {
                            // It must be zero or repeated
                            broken = true;
                            break; // No point to try anymore digits                            
                        }
                    }
                    if (broken)
                    {
                        break; // No point to try longer concatenation
                    }
                    if (digits.Count == 0 && n >= 2)
                    {
                        long concatenatedProduct = long.Parse(productList.Aggregate("", (s, x) => s + x));
                        concatenatedProducts.Add(concatenatedProduct);
                        //Console.WriteLine(i + " * (1.." + n + ") = " + concatenatedProduct);
                        break; // No point to try longer concatenation
                    }
                }
            }
            Console.WriteLine(concatenatedProducts.Max());
        }
    }
}
