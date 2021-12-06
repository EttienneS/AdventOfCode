using Advent;
using System.Diagnostics;

internal class Day6 : ISolution
{
    /// <summary>
    /// https://adventofcode.com/2021/day/6
    /// </summary>
    public void Part1()
    {
        // naive approach:
        var input = File.ReadAllText("./inputs/day6.txt");
        var values = input.Split(',').Select(int.Parse).ToList();

        CalculateSpawn(values, 80);
    }


    public void Part2()
    {
        var input = File.ReadAllText("./inputs/day6.txt");
        var values = input.Split(',').Select(int.Parse).ToList();

        CalculateSpawn(values, 256);
    }

    private static void CalculateSpawn(List<int> values, int days)
    {
        var sw = new Stopwatch();
        sw.Start();
        var fish = new Dictionary<int, long>();
        var lifespan = 9;
        foreach (var value in values)
        {
            IncrementDictionary(fish, value, 1);
        }

        for (int i = 0; i < days; i++)
        {
            var newFish = new Dictionary<int, long>();
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

        Console.WriteLine($"{days} days calculated in {sw.ElapsedMilliseconds}ms\nTotal: {fish.Select(f => f.Value).Sum()}");
    }

    public static void IncrementDictionary(Dictionary<int, long> dict, int index, long amount)
    {
        if (!dict.ContainsKey(index))
        {
            dict.Add(index, 0);
        }
        dict[index] += amount;
    }
}