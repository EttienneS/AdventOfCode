namespace Advent
{
    /// <summary>
    /// https://adventofcode.com/2021/day/4
    /// </summary>
    public class Day4 : ISolution
    {
        public void Part1()
        {
            var inputs = File.ReadAllLines("./inputs/day4.txt");
            var numbers = inputs[0].Split(',').Select(int.Parse);
            var boards = GetBoards(inputs);

            var done = false;
            foreach (var number in numbers)
            {
                Console.WriteLine($"Call: {number}");
                foreach (var board in boards)
                {
                    if (board.Winner(number))
                    {
                        Console.WriteLine($"Winner: {board.CalculateScore(number)}");
                        done = true;
                        break;
                    }
                }
                if (done)
                    break;
            }
        }

        public void Part2()
        {
            var inputs = File.ReadAllLines("./inputs/day4.txt");
            var numbers = inputs[0].Split(',').Select(int.Parse);
            var boards = GetBoards(inputs);

            var done = false;
            foreach (var number in numbers)
            {
                Console.WriteLine($"Call: {number}");
                foreach (var board in boards.ToList())
                {
                    if (board.Winner(number))
                    {
                        boards.Remove(board);
                        Console.WriteLine("Winner, removing.");
                    }

                    if (boards.Count == 0)
                    {
                        Console.WriteLine($"Final board: {board.CalculateScore(number)}");
                        done = true;
                    }
                }
                if (done)
                    break;
            }
        }

        private List<Board> GetBoards(string[] inputs)
        {
            var rawBoard = new string[5];
            var boardIndex = 0;

            var boards = new List<Board>();

            for (int i = 2; i < inputs.Length; i++)
            {
                rawBoard[boardIndex] = inputs[i];
                boardIndex++;

                if (boardIndex == 5)
                {
                    boards.Add(new Board(rawBoard));
                    boardIndex = 0;
                    i++;
                }
            }

            return boards;
        }

        public class Board
        {
            private readonly List<int[]> _columns = new();
            private readonly List<int[]> _lines = new();
            private List<int> _input = new List<int>();

            public Board(string[] lines)
            {
                foreach (var line in lines)
                {
                    _lines.Add(line.Trim().Replace("  ", " ").Split(' ').Select(int.Parse).ToArray());
                    _columns.Add(new int[5]);
                }

                var j = 0;
                foreach (var line in _lines)
                {
                    for (int i = 0; i < 5; i++)
                    {
                        _columns[i][j] = line[i];
                    }
                    j++;
                }
            }

            public bool IsWinner { get; set; }

            public int CalculateScore(int lastNumber)
            {
                return _lines.SelectMany(b => b).Except(_input).Sum() * lastNumber;
            }

            public bool Winner(int number)
            {
                _input.Add(number);

                if (_input.Count < 5)
                {
                    return false;
                }

                if (_lines.Any(line => line.Intersect(_input).Count() == 5))
                {
                    return true;
                }

                if (_columns.Any(col => col.Intersect(_input).Count() == 5))
                {
                    return true;
                }

                return false;
            }
        }
    }
}