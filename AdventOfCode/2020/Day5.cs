using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode._2020
{
    public class Day5 : Day, IChallenge
    {
        public IEnumerable<string> Run()
        {
            var maxRows = 128;
            var maxColumns = 8;

            var passes = Input.Select(x => GetBoardingPass(x, maxRows, maxColumns)).ToList();

            yield return ResultString(Step1(passes));
            yield return ResultString(Step2(passes, maxRows, maxColumns));
        }

        private int Step1(List<BoardingPass> passes)
        {
            return passes.OrderBy(x => x.SeatId).Last().SeatId;
        }

        private int Step2(List<BoardingPass> passes, int maxRows, int maxColumns)
        {
            maxColumns = maxColumns - 1;

            passes = passes.OrderBy(x => x.Row).ThenBy(x => x.Column).Where(x => x.Row > 0 && x.Row < maxRows).ToList();

            var currentRow = 1;
            var currentColumn = 0;

            foreach (var pass in passes)
            {
                if (pass.Row == currentRow && pass.Column == currentColumn)
                {
                    if (currentColumn == maxColumns)
                    {
                        currentRow++;
                        currentColumn = 0;
                    }
                    else
                        currentColumn++;
                }
                else
                {
                    if (currentColumn == 0)
                        return new BoardingPass(currentRow - 1, maxColumns).SeatId;
                    else
                        return new BoardingPass(currentRow, currentColumn).SeatId;
                }
            }
            return 0;
        }

        private BoardingPass GetBoardingPass(string input, int maxRows, int maxColumns)
        {
            return new BoardingPass(input, maxRows, maxColumns);
        }

        private class BoardingPass
        {
            public BoardingPass(string input, int maxRows, int maxColumns)
            {
                RowPartition = new Partition(maxRows);
                ColumnPartition = new Partition(maxColumns);
                SetRowAndColumn(input);
            }

            public BoardingPass(int row, int column)
            {
                _row = row;
                _column = column;
            }

            public Partition RowPartition { get; set; }
            public Partition ColumnPartition { get; set; }

            private int _row;
            public int Row { get { return _row; } }

            private int _column;
            public int Column { get { return _column; } }
            public int SeatId { get { return Row * 8 + Column; } }

            private void SetRowAndColumn(string input)
            {
                foreach (var letter in input)
                {
                    switch (letter)
                    {
                        case 'F':
                            RowPartition.KeepLowerHalf();
                            break;
                        case 'B':
                            RowPartition.KeepUpperHalf();
                            break;
                        case 'L':
                            ColumnPartition.KeepLowerHalf();
                            break;
                        case 'R':
                            ColumnPartition.KeepUpperHalf();
                            break;
                        default:
                            break;
                    }
                }

                _row = RowPartition.FinalValue;
                _column = ColumnPartition.FinalValue;
            }
        }

        private class Partition
        {
            public Partition(int maxNumber)
            {
                MaxNumber = maxNumber - 1;
            }

            public int MinNumber { get; private set; }
            public int MaxNumber { get; private set; }
            public int FinalValue { get; private set; }

            public void KeepUpperHalf()
            {
                if (Math.Abs(MinNumber - MaxNumber) == 1)
                    FinalValue = MaxNumber;

                var addAmount = (int)Math.Ceiling((MaxNumber - MinNumber) / 2d);
                MinNumber += addAmount;
            }

            public void KeepLowerHalf()
            {
                if (Math.Abs(MinNumber - MaxNumber) == 1)
                    FinalValue = MinNumber;

                var subtractAmount = (int)Math.Ceiling((MaxNumber - MinNumber) / 2d);
                MaxNumber -= subtractAmount;
            }
        }
    }
}
