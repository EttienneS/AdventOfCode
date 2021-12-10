// advent of code 2021 solutions
// https://adventofcode.com/2021

using Advent;
using System.Diagnostics;
using System.Reflection;

Console.WriteLine("Merry Advent of code 2021!!");
Console.WriteLine();

var debug = true;

var solutions = GetSolutions();

if (debug)
{
    // only get the highest numbered solution by removing the Day part from the name and selecting the one with the biggest number
    solutions = new List<ISolution>
    {
        solutions.First(s => s.GetType().Name.EndsWith("Day" + solutions.Max(s => int.Parse(s.GetType().Name.Replace("Day", string.Empty)))))
    };
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


static List<ISolution> GetSolutions()
{
    AppDomain app = AppDomain.CurrentDomain;
    Assembly[] ass = app.GetAssemblies();
    Type[] types;
    Type targetType = typeof(ISolution);

    var solutions = new List<ISolution>();
    foreach (Assembly a in ass)
    {
        types = a.GetTypes();
        foreach (Type t in types)
        {
            if (t.IsInterface) continue;
            if (t.IsAbstract) continue;
            foreach (Type iface in t.GetInterfaces())
            {
                if (!iface.Equals(targetType)) continue;
                solutions.Add((ISolution)Activator.CreateInstance(t));
            }
        }
    }
    return solutions;
}
