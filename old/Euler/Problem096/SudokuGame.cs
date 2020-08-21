namespace SudokuSolver
{
    using System;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;

    internal sealed class SudokuGame
    {
        public const int SudokuSize = 9;
        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Member", Justification = "We are using all spaces.")]
        public int[,] boardData;

        public SudokuGame(int[,] initialBoardData)
        {
            if (initialBoardData == null)
            {
                throw new ArgumentNullException("initialBoardData");
            }

            if (initialBoardData.GetLowerBound(0) != 0 || initialBoardData.GetUpperBound(0) != (SudokuSize - 1) ||
                initialBoardData.GetLowerBound(1) != 0 || initialBoardData.GetUpperBound(1) != (SudokuSize - 1))
            {
                throw new ArgumentException("initialBoardData must be a 9 x 9 array");
            }

            this.CloneBoardData(initialBoardData);
        }

        public int GetValue(Location location)
        {
            return this.boardData[location.Row - 1, location.Column - 1];
        }

        public void SetValue(Location location, int value)
        {
            if (this.GetValue(location) != 0)
            {
                throw new InvalidOperationException("The cell is already occupied");
            }

            if (value < 1 || value > 9)
            {
                throw new ArgumentException("1 <= value <= 9");
            }

            this.boardData[location.Row - 1, location.Column - 1] = value;
        }

        public bool IsFilled(Location location)
        {
            return !this.IsEmpty(location);
        }

        public bool IsEmpty(Location location)
        {
            return this.GetValue(location) == 0;
        }

        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "0#", Justification = "We are using all spaces.")]
        [SuppressMessage("Microsoft.Performance", "CA1814:PreferJaggedArraysOverMultidimensional", MessageId = "Body", Justification = "We are using all spaces.")]
        private void CloneBoardData(int[,] initialBoardData)
        {
            Debug.Assert(initialBoardData != null, "Checked by caller.");
            this.boardData = new int[9, 9];
            for (int i = 0; i < SudokuSize; i++)
            {
                for (int j = 0; j < SudokuSize; j++)
                {
                    this.boardData[i, j] = initialBoardData[i, j];
                }
            }
        }
    }
}
