using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AdventOfCode._2021
{
    public class Day4 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            var drawnNumbers = Input[0].Split(",").Select(x => int.Parse(x)).ToList();
            var boards = CreateBoards();

            var remainingBoards = boards.Count;
            foreach (var num in drawnNumbers)
            {
                if (remainingBoards == 0)
                    break;

                foreach (var board in boards)
                {
                    if (board.AlreadyWon)
                        continue;

                    if (board.Wins(num))
                    {
                        // print the results of the first and last boards to win
                        if (remainingBoards == boards.Count || remainingBoards == 1)
                            yield return ResultString(board.SumOfUnmarked * num);

                        remainingBoards--;
                    }
                }
            }
        }

        private List<Board> CreateBoards()
        {
            var boards = new List<Board>();
            var boardStr = string.Empty;

            for (int i = 2; i < Input.Length; i++)
            {
                if (string.IsNullOrEmpty(Input[i]))
                {
                    boards.Add(new Board(boardStr));
                    boardStr = string.Empty;
                }
                else
                    boardStr += Input[i] + " ";
            }
            if (!string.IsNullOrWhiteSpace(boardStr))
                boards.Add(new Board(boardStr));

            return boards;
        }

        class Board
        {
            private const int SequenceSize = 5;

            public Board(string str)
            {
                Cells = new List<Cell>();

                var nums = str.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (var num in nums)
                    Cells.Add(new Cell(int.Parse(num)));
            }

            private List<Cell> Cells { get; set; }

            public bool AlreadyWon { get; set; }

            public int SumOfUnmarked => Cells.Where(x => !x.Marked).Sum(x => x.Value);

            public bool Wins(int value)
            {
                var cell = Cells.Find(x => x.Value == value);
                if (cell != null)
                    cell.Marked = true;

                return CheckRowsAndColumns();
            }

            private bool CheckRowsAndColumns()
            {
                for (int i = 0; i < Cells.Count; i++)
                {
                    // if the index is not on the first layer (top or left), it will already have been checked
                    // columns start in the top layer, where i < SequenceSize, rows start on the left, where numbers are evenly divisible by SequenceSize
                    // 0, 1, 2, 3, 4, 5, 10, 15, 20
                    if (i < SequenceSize || i % SequenceSize == 0)
                    {
                        var isRow = i % SequenceSize == 0;
                        var allInSequenceMarked = AllInSequenceMarked(isRow, i);

                        // index 0 needs to be checked twice, once as a row, once as a column
                        if (i == 0 && !allInSequenceMarked)
                            allInSequenceMarked = AllInSequenceMarked(isRow: false, i); // forcing isRow to false to check it as a column

                        if (allInSequenceMarked)
                            return true;
                    }
                }

                return false;
            }

            private bool AllInSequenceMarked(bool isRow, int currentIndex)
            {
                var lastIndex = isRow ? currentIndex + SequenceSize : Cells.Count;

                while (currentIndex < lastIndex)
                {
                    if (!Cells[currentIndex].Marked)
                        return false;
                    currentIndex += isRow ? 1 : SequenceSize; // if it's a column, add the sequence size to go down
                }
                AlreadyWon = true;
                return true;
            }

            private class Cell
            {
                public Cell(int value)
                {
                    Value = value;
                    Marked = false;
                }

                public int Value { get; set; }
                public bool Marked { get; set; }
            }
        }
    }
}
