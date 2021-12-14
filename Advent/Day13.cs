namespace Advent
{
    internal class Day13 : ISolution
    {
        private readonly string[] _rawInput;

        public Day13()
        {
            _rawInput = File.ReadAllLines("./inputs/day13.txt");

            // should result in 17
            //_rawInput = new[]
            //{
            //    "6,10",
            //    "0,14",
            //    "9,10",
            //    "0,3",
            //    "10,4",
            //    "4,11",
            //    "6,0",
            //    "6,12",
            //    "4,1",
            //    "0,13",
            //    "10,12",
            //    "3,4",
            //    "3,0",
            //    "8,4",
            //    "1,10",
            //    "2,14",
            //    "8,10",
            //    "9,0",
            //    "",
            //    "fold along y=7",
            //    "fold along x=5",
            //};
        }

        public void Part1()
        {
            var (commands, grid) = GetInput(_rawInput);

            grid = Fold(grid, commands[0]);

            var total = 0;
            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    if (grid[x, y])
                    {
                        total++;
                    }
                }
            }
            Console.WriteLine($"Answer: {total}");
        }

        public void Part2()
        {
            var (commands, grid) = GetInput(_rawInput);

            foreach (var command in commands)
            {
                Console.WriteLine();
                grid = Fold(grid, command);
                DrawGrid(grid);
                Console.WriteLine();
            }
        }

        private static bool[,] CreateSmallerGrid(bool[,] grid, int height, int width)
        {
            var newGrid = new bool[width, height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    newGrid[x, y] = grid[x, y];
                }
            }

            return newGrid;
        }

        private static void DrawGrid(bool[,] grid)
        {
            if (grid.GetLength(0) > 100 || grid.GetLength(1) > 100)
            {
                Console.WriteLine("Grid too big");
                return;
            }

            for (int y = 0; y < grid.GetLength(1); y++)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    Console.Write(grid[x, y] ? "X" : ".");
                }

                Console.WriteLine();
            }
        }

        private static bool[,] Fold(bool[,] grid, string command)
        {
            var parts = command.Split('=');

            Console.WriteLine($"Fold along {command}");

            var foldPoint = int.Parse(parts[1]);

            return parts[0] == "x" ? FoldLeft(grid, foldPoint) : FoldUp(grid, foldPoint);
        }

        private static bool[,] FoldLeft(bool[,] grid, int foldPoint)
        {
            var newGrid = CreateSmallerGrid(grid, grid.GetLength(1), foldPoint);
            var i = 0;
            for (int x = grid.GetLength(0) - 1; x > foldPoint; x--)
            {
                for (int y = 0; y < grid.GetLength(1); y++)
                {
                    newGrid[i, y] |= grid[x, y];
                }
                i++;
            }
            return newGrid;
        }

        private static bool[,] FoldUp(bool[,] grid, int foldPoint)
        {
            var newGrid = CreateSmallerGrid(grid, foldPoint, grid.GetLength(0));
            var i = 0;
            for (int y = grid.GetLength(1) - 1; y > foldPoint; y--)
            {
                for (int x = 0; x < grid.GetLength(0); x++)
                {
                    newGrid[x, i] |= grid[x, y];
                }
                i++;
            }

            return newGrid;
        }
        private bool[,] GetEmptyGrid(IEnumerable<string> input)
        {
            var maxX = int.MinValue;
            var maxY = int.MinValue;
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("fold")) continue;

                var lineMax = line.Split(',').Select(int.Parse).ToArray();
                if (lineMax[0] > maxX)
                {
                    maxX = lineMax[0];
                }
                if (lineMax[1] > maxY)
                {
                    maxY = lineMax[1];
                }
            }
            return new bool[maxX + 1, maxY + 1];
        }

        private (List<string> commands, bool[,] grid) GetInput(string[] input)
        {
            var commands = new List<string>();

            var grid = GetEmptyGrid(input);
            foreach (var line in input)
            {
                if (!string.IsNullOrWhiteSpace(line) && !line.StartsWith("fold"))
                {
                    var xy = line.Split(',').Select(int.Parse).ToArray();
                    grid[xy[0], xy[1]] = true;
                }
                else
                {
                    if (string.IsNullOrWhiteSpace(line)) continue;
                    commands.Add(line.Replace("fold along ", string.Empty));
                }
            }

            return (commands, grid);
        }
    }
}