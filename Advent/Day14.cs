namespace Advent
{
    /// https://adventofcode.com/2021/day/14
    internal class Day14 : ISolution
    {
        private readonly List<string> _rawInput;
        private Dictionary<string, (string, string)> _rules;
        private Dictionary<char, ulong> _elements;
        private ulong _length = 0;
        private string _polymer;

        public Day14()
        {
            _rawInput = File.ReadAllLines("./inputs/day14.txt").ToList();

            // should return 1588
            // _rawInput = new List<string>
            // {
            //     "NNCB",
            //     "",
            //     "CH -> B",
            //     "HH -> N",
            //     "CB -> H",
            //     "NH -> C",
            //     "HB -> C",
            //     "HC -> B",
            //     "HN -> C",
            //     "NN -> C",
            //     "BH -> H",
            //     "NC -> B",
            //     "NB -> B",
            //     "BN -> B",
            //     "BB -> N",
            //     "BC -> B",
            //     "CC -> N",
            //     "CN -> C",
            // };

            _polymer = _rawInput[0];
            _rawInput.RemoveRange(0, 2);

            _length = (ulong)_polymer.Length;

            _rules = new Dictionary<string, (string, string)>();
            foreach (var line in _rawInput)
            {
                var parts = line.Split(" -> ");

                var p1 = parts[0][0] + parts[1];
                var p2 = parts[1] + parts[0][1];

                _rules.Add(parts[0], (p1, p2));
            }
        }

        public Dictionary<string, ulong> LoadInput()
        {
            var pairs = new Dictionary<string, ulong>();

            _elements = new();

            for (int k = 0; k < _polymer.Length - 1; k++)
            {
                var p = _polymer.Substring(k, 2);
                pairs.TryAdd(p, 0);
                pairs[p]++;

                _elements.TryAdd(_polymer[k], 0);
                _elements[_polymer[k]]++;
            }

            _elements.TryAdd(_polymer[_polymer.Length - 1], 0);
            _elements[_polymer[_polymer.Length - 1]]++;

            return pairs;
        }

        private void PrintMinMax()
        {
            var min = _elements.Min(e => e.Value);
            var max = _elements.Max(e => e.Value);

            Console.WriteLine($"Answer: {max - min}");
        }
        public void Part1()
        {
            var pairs = LoadInput();
            for (int i = 0; i < 10; i++)
            {
                pairs = RunRules(pairs);
            }

            PrintMinMax();
        }

        private Dictionary<string, ulong> RunRules(Dictionary<string, ulong> pairs)
        {
            var newpairs = new Dictionary<string, ulong>();
            foreach (var pair in pairs)
            {
                var p1 = _rules[pair.Key].Item1;
                var p2 = _rules[pair.Key].Item2;

                newpairs.TryAdd(p1, 0);
                newpairs[p1] += pair.Value;

                newpairs.TryAdd(p2, 0);
                newpairs[p2] += pair.Value;

                _length += pair.Value;

                _elements.TryAdd(p1[1], 0);
                _elements[p1[1]] += pair.Value;
            }
            return newpairs;
        }

        public void Part2()
        {
            var pairs = LoadInput();
            for (int i = 0; i < 40; i++)
            {
                pairs = RunRules(pairs);
            }

            PrintMinMax();
        }
    }
}
