namespace Problem096
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Common;
    using SudokuSolver;

    class Program
    {
        static void Main(string[] args)
        {
            string input = EulerUtil.ReadResourceAsString("Problem096.sudoku.txt");
            string[] inputLines = input.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);

            List<int[,]> puzzles = ParsePuzzles(inputLines);
            int gameSum = 0;
            foreach (int[,] puzzle in puzzles)
            {
                int[,] result = Solve(puzzle);
                gameSum += result[0, 0] * 100 + result[0, 1] * 10 + result[0, 2];
            }

            Console.WriteLine(gameSum);
        }

        private static int[,] Solve(int[,] puzzle)
        {
            SudokuGame game = new SudokuGame(puzzle);
            SudokuSolver solver = new SudokuSolver(game);
            if (solver.Solve())
            {
                return game.boardData;
            }
            else
            {
                // Backtracking need to start here
                IEnumerable<KeyValuePair<Location, HashSet<int>>> options = solver.OptionsForUnitTest.Options;
                // We always find the cell with least options to avoid wrong choice, and pick the one with least row, then least column, just to make a single choice
                var guessCell = options.ArgMin(t => t.Value.Count * 100 + t.Key.Row * 10 + t.Key.Column);
                // We always pick the smallest value
                foreach (int guessValue in guessCell.Value)
                {
                    SudokuGame gameClone = new SudokuGame(game.boardData);
                    gameClone.SetValue(guessCell.Key, guessValue);
                    int[,] gameResult = Solve(gameClone.boardData);
                    if (gameResult != null)
                    {
                        return gameResult;
                    }
                }

                return null;
            }
        }

        private static void PrintPuzzle(int[,] puzzle)
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    Console.Write(puzzle[i, j]);
                }
                Console.WriteLine();
            }
        }

        private static List<int[,]> ParsePuzzles(string[] inputLines)
        {
            List<int[,]> puzzles = new List<int[,]>();
            int j = 0;
            for (int i = 0; i < 50; i++)
            {
                j++;
                int[,] puzzle = new int[9, 9];
                for (int k = 0; k < 9; k++)
                {
                    for (int l = 0; l < 9; l++)
                    {
                        puzzle[k, l] = inputLines[j][l] - '0';
                    }
                    j++;
                }
                puzzles.Add(puzzle);
            }
            return puzzles;
        }
    }
}
