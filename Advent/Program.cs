// advent of code 2021 solutions
// https://adventofcode.com/2021

using Advent;

Console.WriteLine("Merry Advent of code 2021!!");
Console.WriteLine();

var solutions = new List<ISolution>()
{
    new Advent.Day1.Day1(),
    new Advent.Day1.Day2(),
    new Advent.Day1.Day3(),
};

foreach (var solution in solutions)
{
    Console.WriteLine(solution.GetType().Name);
    Console.WriteLine();

    Console.WriteLine("=========  PART 1  =========");
    Console.WriteLine();
    solution.Part1();
    Console.WriteLine();

    Console.WriteLine("=========  PART 2  =========");
    Console.WriteLine();
    solution.Part2();
    Console.WriteLine();
}



