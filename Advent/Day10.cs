namespace Advent
{
    /// <summary>
    /// https://adventofcode.com/2021/day/10
    /// </summary>
    internal class Day10 : ISolution
    {
        private readonly (char open, char closed, int score, int completionScore)[] _pairs = new (char, char, int, int)[]
        {
            ('(',')', 3, 1),
            ('[',']', 57,2),
            ('{','}', 1197,3),
            ('<','>', 25137,4)
        };

        private readonly string[] _values;

        public Day10()
        {
            _values = File.ReadAllLines("./inputs/day10.txt");

            //// debug data should return 26397 for part 1 and 288957 for part 2
            //_values = new[]
            //{
            //    "[({(<(())[]>[[{[]{<()<>>",
            //    "[(()[<>])]({[<{<<[]>>(",
            //    "{([(<{}[<>[]}>{[]{[(<()>",
            //    "(((({<>}<{<{<>}{[]{[]{}",
            //    "[[<[([]))<([[{}[[()]]]",
            //    "[{[{({}]{}}([{[{{{}}([]",
            //    "{<[[]]>}<{[{[{[]{()[[[]",
            //    "[<(<(<(<{}))><([]([]()",
            //    "<{([([[(<>()){}]>(<<{{",
            //    "<{([{{}}[<[[[<>{}]]]>[]]"
            //};
        }

        public void Part1()
        {
            GetValidLines();
        }

        public void Part2()
        {
            var lines = GetValidLines();

            var options = _pairs.Select(p => p.closed).ToArray();

            var scores = new List<long>();
            foreach (var value in lines)
            {
                Console.Write($"Value: {value}");
                var stream = new Stack<char>();
                var open = _pairs.Select(p => p.open).ToArray();
                for (int i = 0; i < value.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    var current = value[i];
                    if (open.Contains(current))
                    {
                        stream.Push(current);
                    }
                    else
                    {
                        stream.Pop();
                    }
                }

                var completion = "";

                while (stream.Count > 0)
                {
                    completion += GetClosedTag(stream.Pop());
                }

                Console.ForegroundColor = ConsoleColor.Green;
                Console.Write($"{completion}");
                long score = 0;
                for (int i = 0; i < completion.Length; i++)
                {
                    score *= 5;
                    score += GetCompletionScore(completion[i]);
                }

                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine($"   - Score: {score}");
                scores.Add(score);
            }
            scores.Sort();

            Console.WriteLine($"Winner: {scores[scores.Count / 2]}");
        }

        private char GetClosedTag(char open)
        {
            return _pairs.First(p => p.open == open).closed;
        }

        private int GetCompletionScore(char closed)
        {
            return _pairs.First(p => p.closed == closed).completionScore;
        }

        private char GetOpenTag(char closed)
        {
            return _pairs.First(p => p.closed == closed).open;
        }

        private int GetScore(char closed)
        {
            return _pairs.First(p => p.closed == closed).score;
        }

        private List<string> GetValidLines()
        {
            var validLines = new List<string>();

            var illegalChars = new List<char>();
            foreach (var value in _values)
            {
                var open = _pairs.Select(p => p.open).ToArray();

                var stream = new Stack<char>();
                var ok = true;
                Console.WriteLine(value);
                for (int i = 0; i < value.Length; i++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    var current = value[i];
                    Console.Write(current);
                    if (open.Contains(current))
                    {
                        stream.Push(current);
                    }
                    else
                    {
                        var expectedTag = GetOpenTag(current);
                        if (stream.Peek() != expectedTag)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($" ERROR found '{current}' expected '{GetClosedTag(expectedTag)}'");

                            illegalChars.Add(current);
                            ok = false;
                            break;
                        }
                        else
                        {
                            stream.Pop();
                        }
                    }
                }

                if (ok)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine(" - OK");
                    validLines.Add(value);
                }
                Console.ForegroundColor = ConsoleColor.White;
            }

            var score = 0;
            foreach (var value in illegalChars)
            {
                score += GetScore(value);
            }

            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine();
            Console.WriteLine($"Score: {score}");

            return validLines;
        }
    }
}