using Advent.Common.Extensions;

namespace Advent
{
    internal class Day11 : ISolution
    {
        private int _maxX;
        private int _maxY;
        private string[] _raw;
        private int[,] _values;

        public Day11()
        {
            _raw = File.ReadAllLines("./inputs/day11.txt");

            // debug data should return 1656 for part 1 and 194 for part 2
            //_raw = new[]
            //{
            //    "5483143223",
            //    "2745854711",
            //    "5264556173",
            //    "6141336146",
            //    "6357385478",
            //    "4167524645",
            //    "2176841721",
            //    "6882881134",
            //    "4846848554",
            //    "5283751526",
            //};
        }

        public void Part1()
        {
            GetValues(_raw);

            var totalFlashes = 0;
            for (int i = 0; i < 100; i++)
            {
                Console.WriteLine($"Step {i}");
                IncreaseEnergy();
                var flashes = FlashGrid();
                ResetGrid(false);
                Console.WriteLine($"Flashes: {flashes}");
                totalFlashes += flashes;
            }
            Console.WriteLine($"Total flashes: {totalFlashes}");
        }

        public void Part2()
        {
            GetValues(_raw);
            var i = 0;
            while (true)
            {
                i++;
                IncreaseEnergy();
                FlashGrid();
                ResetGrid(false);

                var total = 0;
                for (int x = 0; x < _maxX; x++)
                {
                    for (int y = 0; y < _maxY; y++)
                    {
                        total += _values[x, y];
                    }
                }
                if (total == 0)
                {
                    Console.WriteLine($"Mega flash found at step {i}");
                    break;
                }

                if (i > 10000)
                {
                    // infinite loop protection
                    Console.WriteLine($"Ininite loop break");
                    break;
                }
            }
        }

        private int FlashGrid()
        {
            var flashes = 0;
            var previousFlashes = -1;
            while (previousFlashes != flashes)
            {
                previousFlashes = flashes;
                for (int x = 0; x < _maxX; x++)
                {
                    for (int y = 0; y < _maxY; y++)
                    {
                        if (_values[x, y] == 10)
                        {
                            flashes++;

                            _values[x, y]++;
                            foreach (var value in _values.GetNeighbours(x, y))
                            {
                                if (_values[value.x, value.y] < 10)
                                {
                                    _values[value.x, value.y]++;
                                }
                            }
                        }
                    }
                }
            }

            return flashes;
        }

        private void GetValues(string[] raw)
        {
            _maxX = raw[0].Length;
            _maxY = raw.Length;
            _values = new int[raw[0].Length, raw.Length];

            for (int x = 0; x < _maxX; x++)
            {
                for (int y = 0; y < _maxY; y++)
                {
                    _values[x, y] = raw[y][x] - '0';
                }
            }
        }
        private void IncreaseEnergy()
        {
            // step 1 increase energy for all squares
            for (int x = 0; x < _maxX; x++)
            {
                for (int y = 0; y < _maxY; y++)
                {
                    _values[x, y]++;
                }
            }
        }

        private void ResetGrid(bool draw)
        {
            for (int y = 0; y < _maxY; y++)
            {
                for (int x = 0; x < _maxX; x++)
                {
                    if (_values[x, y] > 9)
                    {
                        _values[x, y] = 0;
                    }
                    if (draw)
                        Console.Write(_values[x, y]);
                }
                if (draw)
                    Console.WriteLine();
            }
        }
    }
}