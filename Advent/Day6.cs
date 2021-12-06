using System.Diagnostics;

namespace Advent
{
    /// <summary>
    /// https://adventofcode.com/2021/day/6
    /// </summary>
    internal class Day6 : ISolution
    {
        private readonly Stopwatch _sw;
        private readonly List<int> _values;

        public Day6()
        {
            _values = File.ReadAllText("./inputs/day6.txt").Split(',').Select(int.Parse).ToList();
            _sw = new Stopwatch();
        }

        public void Part1()
        {
            CalculateSpawn(_values, 80);
        }

        public void Part2()
        {
            CalculateSpawn(_values, 256);
        }

        private static void IncrementDictionary(Dictionary<int, long> dict, int index, long amount)
        {
            if (!dict.ContainsKey(index))
            {
                dict.Add(index, 0);
            }
            dict[index] += amount;
        }

        private void CalculateSpawn(List<int> values, int days)
        {
            _sw.Restart();

            var fish = new Dictionary<int, long>();
            foreach (var value in values)
            {
                IncrementDictionary(fish, value, 1);
            }

            for (int i = 0; i < days; i++)
            {
                var newFish = new Dictionary<int, long>();
                const int lifespan = 9;
                for (int j = 0; j < lifespan; j++)
                {
                    if (!fish.ContainsKey(j)) continue;
                    var current = fish[j];
                    if (j == 0)
                    {
                        IncrementDictionary(newFish, 6, current);
                        IncrementDictionary(newFish, 8, current);
                    }
                    else
                    {
                        IncrementDictionary(newFish, j - 1, current);
                    }
                }

                fish = newFish;
            }

            Console.WriteLine($"{days} days calculated in {_sw.ElapsedMilliseconds}ms");
            Console.WriteLine($"Total: {fish.Select(f => f.Value).Sum()}");
        }
    }
}