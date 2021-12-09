// advent of code 2021 solutions
// https://adventofcode.com/2021

using Advent;
using System.Diagnostics;

Console.WriteLine("Merry Advent of code 2021!!");
Console.WriteLine();

var debug = true;

var solutions = new List<ISolution>()
{
    new Day1(),
    new Day2(),
    new Day3(),
    new Day4(),
    new Day5(),
    new Day6(),
    new Day7(),
    new Day8(),
    new Day9(),
};

if (debug)
{
    // remove all but last
    solutions.RemoveRange(0, solutions.Count - 1);
}


var sw = Stopwatch.StartNew();
foreach (var solution in solutions)
{
    sw.Restart();
    Console.WriteLine();
    Console.WriteLine("===========================================================");
    Console.WriteLine();
    Console.WriteLine($"                        - {solution.GetType().Name} -");
    Console.WriteLine();
    Console.WriteLine("===========================================================");
    Console.WriteLine();

    Console.WriteLine("+++++  PART 1  +++++");
    Console.WriteLine();
    solution.Part1();
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Part complete in {sw.ElapsedMilliseconds}");
    Console.ForegroundColor = ConsoleColor.Gray;

    Console.WriteLine("-----  PART 1  -----");
    Console.WriteLine();


    sw.Restart();

    Console.WriteLine("+++++  +PART 2  +++++");
    Console.WriteLine();
    solution.Part2();
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine($"Part complete in {sw.ElapsedMilliseconds}");
    Console.ForegroundColor = ConsoleColor.Gray;
    Console.WriteLine("-----  PART 2  -----");
    Console.WriteLine();
    Console.WriteLine("===========================================================");
}



