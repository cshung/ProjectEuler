namespace SudokuSolver
{
    using System;
    using System.Collections.Generic;

    internal class Combination
    {
        private HashSet<int> choices;

        internal Combination(IEnumerable<int> choices)
        {
            if (choices == null)
            {
                throw new ArgumentNullException("choices");
            }

            this.choices = new HashSet<int>();
            foreach (int choice in choices)
            {
                CheckValue(choice);
                this.choices.Add(choice);
            }
        }

        internal bool Contains(int choice)
        {
            CheckValue(choice);
            return this.choices.Contains(choice);
        }

        private static void CheckValue(int choice)
        {
            if (choice < 1 || choice > 9)
            {
                throw new ArgumentException("choice value out of range");
            }
        }
    }
}
