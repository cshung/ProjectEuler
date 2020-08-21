namespace SudokuSolver
{
    using System;

    internal sealed class SolutionCandidate
    {
        public SolutionCandidate(Location location, int value)
        {
            if (location == null)
            {
                throw new ArgumentNullException("location");
            }

            if (value < 1)
            {
                throw new ArgumentException("value too small.");
            }

            if (value > 9)
            {
                throw new ArgumentException("value too large.");
            }

            this.Location = location;
            this.Value = value;
        }

        public Location Location { get; private set; }

        public int Value { get; private set; }
    }
}
