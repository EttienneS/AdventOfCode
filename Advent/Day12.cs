namespace Advent
{
    internal class Day12 : ISolution
    {
        private const string END = "end";
        private const string START = "start";
        private readonly Node _map;
        private readonly Dictionary<string, Node> _nodeLookup;
        private bool _extraLog = false;

        public Day12()
        {
            var _values = File.ReadAllLines("./inputs/day12.txt");

            // debug data should return 10 for part 1 and 36 for part 2
            //_values = new[]
            //{
            //    "start-A",
            //    "start-b",
            //    "A-c",
            //    "A-b",
            //    "b-d",
            //    "A-end",
            //    "b-end",
            //};

            _map = new Node(START);
            _nodeLookup = new Dictionary<string, Node>();
            _nodeLookup.Add(START, _map);

            foreach (var value in _values)
            {
                var parts = value.Split('-');

                var nodeA = GetOrAddNode(parts[0]);
                var nodeB = GetOrAddNode(parts[1]);

                if (!nodeA.Children.Contains(nodeB))
                {
                    nodeA.Children.Add(nodeB);
                }
                if (!nodeB.Children.Contains(nodeA))
                {
                    nodeB.Children.Add(nodeA);
                }
            }
        }

        public void Part1()
        {
            var routes = 0;
            foreach (var route in Explore(_map, false))
            {
                LogRoute(route);
                routes++;
            }

            Console.WriteLine($"Found {routes} valid routes");
        }

        public void Part2()
        {
            var routes = 0;
            foreach (var route in Explore(_map, true))
            {
                LogRoute(route);
                routes++;
            }

            Console.WriteLine($"Found {routes} valid routes");
        }

        private IEnumerable<string> Explore(Node root, bool twiceEnabled)
        {
            var queue = new Queue<(string path, Node node)>();
            queue.Enqueue((root.Name, root));

            while (queue.Count > 0)
            {
                var route = queue.Dequeue();

                if (route.node.Children.Count > 0)
                {
                    foreach (var child in route.node.Children)
                    {
                        if (child.IsBig || HasVisitedTwice(twiceEnabled, route, child))
                        {
                            if (child.Name == END)
                            {
                                yield return route.path + "," + END;
                            }
                            else
                            {
                                queue.Enqueue((route.path + "," + child.Name, child));
                            }
                        }
                        else
                        {
                            LogRoute($"Dead end: {route}");
                        }
                    }
                }
                else
                {
                    if (route.path.EndsWith(END))
                    {
                        yield return route.path;
                    }
                    else
                    {
                        LogRoute($"Dead end: {route}");
                    }
                }
            }
        }

        private Node GetOrAddNode(string a)
        {
            if (!_nodeLookup.ContainsKey(a))
            {
                _nodeLookup.Add(a, new Node(a));
            }
            return _nodeLookup[a];
        }

        private bool HasVisitedTwice(bool twiceEnabled, (string path, Node node) route, Node child)
        {
            if (!twiceEnabled)
            {
                return !route.path.Contains(child.Name, StringComparison.Ordinal);
            }
            else
            {
                var parts = route.path.Split(',');

                var set = new HashSet<string>();
                var dupe = false;
                foreach (var x in parts)
                {
                    if (x.ToLower() == x && !set.Add(x))
                    {
                        // start can only happen once
                        if (x == START) return false;

                        // only 1 dupe is allowed
                        if (dupe) return false;

                        dupe = true;
                    }
                }

                return true;
            }
        }

        private void LogRoute(string route)
        {
            if (_extraLog) Console.WriteLine($"-> {route} -> Dead end");
        }

        internal class Node
        {
            public Node(string name)
            {
                Name = name;
                IsBig = name.ToUpper() == name;
                Children = new List<Node>();
            }

            public List<Node> Children { get; set; }

            public bool IsBig { get; }
            public string Name { get; set; }

            public override string ToString()
            {
                return Name;
            }
        }
    }
}