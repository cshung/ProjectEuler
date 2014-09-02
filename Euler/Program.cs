using System;
using System.Reflection;
namespace Euler
{
    internal static partial class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Problem number: ");
            var problemNumber = int.Parse(Console.ReadLine());
            var problemMethodName = "Problem" + string.Format("{0:000}", problemNumber);
            MethodInfo problemMethod = typeof(Program).GetMethod(problemMethodName);
            if (problemMethod != null)
            {
                problemMethod.Invoke(null, null);
            }
            else
            {
                Console.WriteLine("Solution not found yet.");
            }
        }
    }
}
