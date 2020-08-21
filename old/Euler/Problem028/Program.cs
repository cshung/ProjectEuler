namespace Problem028
{
    using System;
    using Common;

    class Program
    {
        static void Main(string[] args)
        {
            int size = 1001;
            int[,] spiral = EulerUtil.CreateSpiral(size);
            Print(size, spiral);
            long sum = 0;
            for (int w = 0; w < size; w++)
            {
                sum += spiral[w, w];
                sum += spiral[size - 1 - w, w];
            }
            sum -= spiral[size / 2, size / 2];
            Console.WriteLine(sum);
        }

        private static void Print(int size, int[,] sprial)
        {
            for (int h = 0; h < size; h++)
            {
                for (int w = 0; w < size; w++)
                {
                    Console.Write(sprial[h, w].ToString().PadLeft(2, '0') + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
