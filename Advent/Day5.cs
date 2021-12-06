using System.Drawing;

namespace Advent
{
    /// <summary>
    /// https://adventofcode.com/2021/day/5
    /// </summary>
    public class Day5 : ISolution
    {
        public void Part1()
        {
            var (lines, maxX, maxY) = GetLines(File.ReadAllLines("./inputs/day5.txt"));
            var buffer = DrawLines(lines, maxX, maxY, true);
            RenderLines(maxX, maxY, buffer);
        }

        public void Part2()
        {
            var (lines, maxX, maxY) = GetLines(File.ReadAllLines("./inputs/day5.txt"));
            var buffer = DrawLines(lines, maxX, maxY, false);
            RenderLines(maxX, maxY, buffer);
        }

        private static void RenderLines(int maxX, int maxY, int[,] buffer)
        {
            var bmp = new Bitmap(maxX, maxY);
            var counter = 0;
            for (int i = 0; i < maxX; i++)
            {
                for (int j = 0; j < maxY; j++)
                {
                    if (buffer[i, j] >= 1)
                    {
                        bmp.SetPixel(i, j, Color.Black);
                        if (buffer[i, j] > 1)
                        {
                            counter++;
                            bmp.SetPixel(i, j, Color.Red);
                        }
                    }
                    else
                    {
                        bmp.SetPixel(i, j, Color.White);
                    }

                }
            }
            Console.WriteLine();
            Console.WriteLine($"Interceptions: {counter}");

            bmp.Save("day5.bmp");
        }

        private int[,] DrawLines(List<Line>? lines, int maxX, int maxY, bool onlyVertical)
        {
            var buffer = new int[maxX, maxY];
            foreach (var line in lines)
            {
                if (onlyVertical && !(line.IsVertical || line.IsHorizontal))
                {
                    continue;
                }
                DrawLine(buffer, line);
            }

            return buffer;
        }

        private static (List<Line> lines, int maxX, int maxY) GetLines(string[] input)
        {
            var lines = new List<Line>();
            int maxX = 0, maxY = 0;

            foreach (var line in input)
            {
                // 0,9 -> 5,9
                var points = line.Replace(" -> ", ",").Split(',').Select(int.Parse).ToArray();

                if (points[0] > maxX || points[2] > maxX)
                {
                    maxX = Math.Max(points[0], points[2]);
                }

                if (points[1] > maxY || points[3] > maxY)
                {
                    maxY = Math.Max(points[0], points[3]);
                }

                lines.Add(new Line(points[0], points[1], points[2], points[3]));
            }

            maxY++;
            maxX++;

            return (lines, maxX, maxY);
        }

        private static void DrawLine(int[,] buffer, Line line)
        {
            // Bresenham's line algorithm
            int x = line.X1;
            int y = line.Y1;
            int x2 = line.X2;
            int y2 = line.Y2;

            int w = x2 - x;
            int h = y2 - y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (longest <= shortest)
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                buffer[x, y]++;
                numerator += shortest;
                if (numerator >= longest)
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }
        }

        public class Line
        {
            public int X1 { get; set; }
            public int Y1 { get; set; }
            public int X2 { get; set; }
            public int Y2 { get; set; }
            public bool IsHorizontal
            {
                get { return X1 == X2; }
            }

            public bool IsVertical
            {
                get { return Y1 == Y2; }
            }

            public Line(int x1, int y1, int x2, int y2)
            {
                X1 = x1;
                Y1 = y1;
                X2 = x2;
                Y2 = y2;
            }
        }
    }
}