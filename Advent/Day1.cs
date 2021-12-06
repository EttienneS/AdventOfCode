namespace Advent
{
    /// <summary>
    /// https://adventofcode.com/2021/day/1
    /// </summary>
    public class Day1 : ISolution
    {
        public void Part1()
        {
            // given a list of items find out how many times an item in
            // the list is bigger than the previous item
            Console.WriteLine($"Total raw increases: {CountIncreases(GetInput())}");
        }

        public void Part2()
        {
            // given a list of items find out how many times a sliding list of
            // 3 items is bigger than the previous 3 item window
            var input = GetInput();

            var windows = new List<int>();
            for (int i = 0; i < input.Count - 2; i++)
            {
                windows.Add(input.GetRange(i, 3).Sum());
            }

            Console.WriteLine($"Total sliding window increases: {CountIncreases(windows)}");
        }

        private static int CountIncreases(List<int> input)
        {
            var increases = 0;
            for (int i = 0; i < input.Count; i++)
            {
                if (i > 0 && input[i - 1] < input[i])
                {
                    increases++;
                }
            }

            return increases;
        }

        private static List<int> GetInput()
        {
            return File.ReadAllLines("inputs/day1.txt")
                       .Select(int.Parse)
                       .ToList();
        }
    }
}