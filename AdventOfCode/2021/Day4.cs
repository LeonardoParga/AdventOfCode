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

            var running = true;
            foreach (var num in drawnNumbers)
            {
                if (!running)
                    break;

                foreach (var board in boards)
                {
                    if (board.Wins(num))
                    {
                        yield return ResultString(board.SumOfUnmarked * num);
                        running = false;
                        break;
                    }
                }
            }
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

            public int SumOfUnmarked => Cells.Where(x => !x.Marked).Sum(x => x.Value);

            public bool Wins(int value)
            {
                var cell = Cells.Find(x => x.Value == value);
                if (cell != null)
                    cell.Marked = true;

                for (int i = 0; i <= Cells.Count - SequenceSize; i += SequenceSize)
                {
                    var allMarked = true;
                    for (int j = i; j < i + SequenceSize; j++)
                    {
                        if (!Cells[j].Marked)
                        {
                            allMarked = false;
                            break;
                        }
                    }

                    if (allMarked)
                        return true;
                }

                for (int i = 0; i < SequenceSize; i++)
                {
                    var allMarked = true;
                    for (int j = i; j < SequenceSize * (SequenceSize - 1); j++)
                    {
                        if (!Cells[j].Marked)
                        {
                            allMarked = false;
                            break;
                        }
                    }

                    if (allMarked)
                        return true;
                }

                return false;
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
