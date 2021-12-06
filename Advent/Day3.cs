namespace Advent
{
    /// <summary>
    /// https://adventofcode.com/2021/day/3
    /// </summary>
    public class Day3 : ISolution
    {
        public void Part1()
        {
            var input = File.ReadAllLines("./inputs/day3.txt");

            var gamma = "";
            var epsilon = "";

            var lenght = input[0].Length;
            for (int i = 0; i < lenght; i++)
            {
                var count = 0;
                foreach (var line in input)
                {
                    if (line[i] == '1')
                    {
                        count++;
                    }
                }
                if (count > input.Length / 2)
                {
                    gamma += "1";
                    epsilon += "0";
                }
                else
                {
                    gamma += "0";
                    epsilon += "1";
                }
            }

            var gammaDec = Convert.ToInt32(gamma, 2);
            var epsilonDec = Convert.ToInt32(epsilon, 2);

            Console.WriteLine($"Gamma:    {gamma} >> {gammaDec}");
            Console.WriteLine($"Episilon: {epsilon} >> {epsilonDec}");

            Console.WriteLine($"Power: {gammaDec * epsilonDec}");
        }

        public void Part2()
        {
            var input = File.ReadAllLines("./inputs/day3.txt").ToList();

            var oxygen = GetInput(input, evaluate: (candidates1, candidates0) => candidates1 >= candidates0);
            var co2 = GetInput(input, evaluate: (candidates1, candidates0) => candidates1 < candidates0);

            var oxygenDec = Convert.ToInt32(oxygen, 2);
            var co2Dec = Convert.ToInt32(co2, 2);

            Console.WriteLine($"Oxygen:    {oxygen} >> {oxygenDec}");
            Console.WriteLine($"CO2:       {co2} >> {co2Dec}");

            Console.WriteLine($"Life Support: {oxygenDec * co2Dec}");
        }

        private static string GetInput(List<string> input, Func<int, int, bool> evaluate)
        {
            var result = "";
            var lenght = input[0].Length;
            for (int i = 0; i < lenght; i++)
            {
                var candidates0 = new List<string>();
                var candidates1 = new List<string>();
                foreach (var line in input)
                {
                    if (line[i] == '1')
                    {
                        candidates1.Add(line);
                    }
                    else
                    {
                        candidates0.Add(line);
                    }
                }

                if (evaluate.Invoke(candidates1.Count, candidates0.Count))
                {
                    input = candidates1;
                }
                else
                {
                    input = candidates0;
                }

                if (input.Count == 1)
                {
                    result = input[0];
                    break;
                }
            }

            return result;
        }
    }
}