using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Problem019
{
    class Program
    {
        static void Main(string[] args)
        {
            int sum = 0;
            for (int year = 1901; year <= 2000; year++)
            {
                for (int month = 1; month <= 12; month++)
                {
                    if (new DateTime(year, month, 1).DayOfWeek == DayOfWeek.Sunday)
                    {
                        sum++;
                    }
                }
            }
            Console.WriteLine(sum);
            
        }
    }
}
