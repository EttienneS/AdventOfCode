namespace Advent.Common.Extensions
{
    public static class GridExtensions
    {
        public static List<(int x, int y, int value)> GetNeighbours(this int[,] grid, int x, int y, bool cardinalOnly = false)
        {
            var maxX = grid.GetLength(0) - 1;
            var maxY = grid.GetLength(1) - 1;
            var neighbours = new List<(int x, int y, int value)>();
            if (x > 0)
            {
                neighbours.Add((x - 1, y, grid[x - 1, y]));
            }
            if (x < maxX)
            {
                neighbours.Add((x + 1, y, grid[x + 1, y]));
            }

            if (y > 0)
            {
                neighbours.Add((x, y - 1, grid[x, y - 1]));

                if (!cardinalOnly)
                {
                    if (x > 0)
                    {
                        neighbours.Add((x - 1, y - 1, grid[x - 1, y - 1]));
                    }
                    if (x < maxX)
                    {
                        neighbours.Add((x + 1, y - 1, grid[x + 1, y - 1]));
                    }
                }

            }

            if (y < maxY)
            {
                neighbours.Add((x, y + 1, grid[x, y + 1]));

                if (!cardinalOnly)
                {
                    if (x > 0)
                    {
                        neighbours.Add((x - 1, y + 1, grid[x - 1, y + 1]));
                    }
                    if (x < maxX)
                    {
                        neighbours.Add((x + 1, y + 1, grid[x + 1, y + 1]));
                    }
                }
            }

            return neighbours;
        }
    }
}