using Advent.Common.Extensions;
using System.Diagnostics;

namespace Advent
{
    /// <summary>
    /// https://adventofcode.com/2021/day/9
    /// </summary>
    internal class Day9 : ISolution
    {
        private readonly int[,] _values;
        private readonly int _maxX;
        private readonly int _maxY;

        public Day9()
        {
            var lines = File.ReadAllLines("./inputs/day9.txt");

            //lines = new[]
            //{
            //    "2199943210",
            //    "3987894921",
            //    "9856789892",
            //    "8767896789",
            //    "9899965678"
            //};

            _values = new int[lines[0].Length, lines.Length];

            for (int y = 0; y < lines.Length; y++)
            {
                for (int x = 0; x < lines[0].Length; x++)
                {
                    _values[x, y] = lines[y][x] - '0';
                }
            }

            _maxX = _values.GetLength(0) - 1;
            _maxY = _values.GetLength(1) - 1;
        }

        public void Part1()
        {
            var lowPoints = GetLowPoints();

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"Risk level: {lowPoints.Sum(p => p.value + 1)}"); // 603
        }

        private List<(int x, int y, int value)> GetLowPoints()
        {
            var lowPoints = new List<(int x, int y, int value)>();
            for (int y = 0; y <= _maxY; y++)
            {
                for (int x = 0; x <= _maxX; x++)
                {
                    Console.ForegroundColor = ConsoleColor.White;

                    var thisValue = _values[x, y];
                    if (_values.GetNeighbours(x, y).All(n => n.value >= thisValue))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;

                        //Console.WriteLine($"Found low point: {x}:{y} {value}");
                        lowPoints.Add((x, y, thisValue));

                    }

                    //Console.Write(thisValue);
                }
                //Console.WriteLine();
            }

            return lowPoints;
        }

        

        public void Part2()
        {
            var lowPoints = GetLowPoints();

            var basins = new List<int>();
            foreach (var lowPoint in lowPoints)
            {
                var closed = new List<(int x, int y)>();
                var frontier = new Queue<(int x, int y)>();
                frontier.Enqueue((lowPoint.x, lowPoint.y));

                var size = 0;
                while (frontier.Count > 0)
                {
                    var current = frontier.Dequeue();
                    closed.Add(current);

                    foreach (var neighbor in _values.GetNeighbours(current.x, current.y, true).Where(n => n.value < 9 && !closed.Contains((n.x, n.y))).Select(p => (p.x, p.y)))
                    {
                        if (!frontier.Contains(neighbor))
                        {
                            frontier.Enqueue(neighbor);
                        }
                    }
                    size++;

                    //Console.WriteLine($"Frontier: {frontier.Count}");
                    //Console.WriteLine($"Closed: {closed.Count}");
                }
                basins.Add(size);

                Console.WriteLine($"Basin size: {size}");

            }
            var sortedBasins = basins.OrderByDescending(d => d).ToList();

            Console.WriteLine($"Answer: {sortedBasins[0] * sortedBasins[1] * sortedBasins[2]}");
        }
    }
}