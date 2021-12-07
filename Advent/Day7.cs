using System.Diagnostics;

namespace Advent
{
    /// <summary>
    /// https://adventofcode.com/2021/day/7
    /// </summary>
    internal class Day7 : ISolution
    {
        private readonly List<int> _values;

        public Day7()
        {
            _values = File.ReadAllText("./inputs/day7.txt").Split(',').Select(int.Parse).ToList();
            //_values = "16,1,2,0,4,2,7,1,2,14".Split(',').Select(int.Parse).ToList();
        }

        public void Part1()
        {
            var min = _values.Min();
            var max = _values.Max();

            var answer = int.MaxValue;
            for (int i = min; i <= max; i++)
            {
                var total = 0;
                foreach (var v in _values)
                {
                    total += Math.Abs(v - i);
                }

                if (total < answer)
                {
                    answer = total;
                }
            }

            Console.WriteLine($"Convergence point: {answer}");
        }

        public void Part2()
        {
            var min = _values.Min();
            var max = _values.Max();

            var answer = int.MaxValue;
            for (int i = min; i <= max; i++)
            {
                var total = 0;
                foreach (var v in _values)
                {
                    for (int x = 0; x <= Math.Abs(v - i); x++)
                    {
                        total += x;
                    }
                }

                if (total < answer)
                {
                    answer = total;
                }
            }

            Console.WriteLine($"Convergence point: {answer}");
        }

    }
}