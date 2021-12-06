namespace Advent.Day1
{
    /// <summary>
    /// https://adventofcode.com/2021/day/2
    /// </summary>
    public class Day2 : ISolution
    {
        public void Part1()
        {
            var inputs = File.ReadAllLines("inputs/day2.txt").Select(i => i.Split(' '));

            var horizontal = 0;
            var depth = 0;
            foreach (var input in inputs)
            {
                var value = int.Parse(input[1]);
                switch (input[0])
                {
                    case "forward":
                        horizontal += value;
                        break;
                    case "down":
                        depth += value;
                        break;
                    case "up":
                        depth -= value;
                        break;
                }
            }

            Console.WriteLine($"Total Distance: {horizontal * depth}");
        }

        public void Part2()
        {
            var inputs = File.ReadAllLines("inputs/day2.txt").Select(i => i.Split(' '));

            var horizontal = 0;
            var depth = 0;
            var aim = 0;
            foreach (var input in inputs)
            {
                var value = int.Parse(input[1]);
                switch (input[0])
                {
                    case "forward":
                        horizontal += value;
                        depth += aim * value;
                        break;
                    case "down":
                        aim += value;
                        break;
                    case "up":
                        aim -= value;
                        break;
                }
            }

            Console.WriteLine($"Total Distance: {horizontal * depth}");
        }
    }
}