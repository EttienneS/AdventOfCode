using System.Diagnostics;

namespace Advent
{
    /// <summary>
    /// https://adventofcode.com/2021/day/8
    /// </summary>
    internal class Day8 : ISolution
    {
        private readonly string[] _values;

        public Day8()
        {
            _values = File.ReadAllLines("./inputs/day8.txt");
        }

        public void Part1()
        {
            var counter = 0;
            foreach (var line in _values)
            {
                var parts = line.Split('|');
                var options = new[] { 2, 3, 4, 7, };
                foreach (var digit in parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    if (options.Contains(digit.Length))
                    {
                        counter++;
                    }
                }
            }

            Console.WriteLine($"Answer: {counter}");
        }

        public void Part2()
        {
            var total = 0;
            foreach (var line in _values)
            {
                var parts = line.Split('|');

                var keys = parts[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
                var key = GetLookup(keys.ToList()); // copy the list before sending to reuse later

                var number = "";

                foreach (var digit in parts[1].Split(' ', StringSplitOptions.RemoveEmptyEntries))
                {
                    var match = keys.First(k => k.Length == digit.Length && k.Intersect(digit).Count() == digit.Length);
                    number += key[match];
                }

                total += int.Parse(number);
            }

            Console.WriteLine($"Answer: {total}");
        }

        private static Dictionary<string, string> GetLookup(List<string> parts)
        {
            var lookup = new Dictionary<string, string>();

            // constants
            AddKey(1, (p) => p.Length == 2, parts, lookup);
            AddKey(4, (p) => p.Length == 4, parts, lookup);
            AddKey(7, (p) => p.Length == 3, parts, lookup);
            AddKey(8, (p) => p.Length == 7, parts, lookup);

            // 6 segments
            AddKey(9, (p) => p.Length == 6 && p.Intersect(lookup["4"]).Count() == 4, parts, lookup);
            AddKey(0, (p) => p.Length == 6 && p.Intersect(lookup["7"]).Count() == 3, parts, lookup);
            AddKey(6, (p) => p.Length == 6, parts, lookup);

            // 5 segments
            AddKey(3, (p) => p.Length == 5 && p.Intersect(lookup["1"]).Count() == 2, parts, lookup);
            AddKey(5, (p) => p.Length == 5 && p.Intersect(lookup["6"]).Count() == 5, parts, lookup);
            AddKey(2, (p) => p.Length == 5, parts, lookup);

            if (parts.Count > 0)
            {
                throw new Exception("Decode failed!");
            }

            // switch key and value types around for easier lookup
            return lookup.ToDictionary(key => key.Value, value => value.Key);

        }

        private static void AddKey(int key, Func<string, bool> eval, List<string> parts, Dictionary<string, string> lookup)
        {
            lookup.Add(key.ToString(), parts.First(eval));
            parts.Remove(lookup[key.ToString()]);
        }
    }
}