using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PseudoWolfenstein.Model
{
    public class Scene
    {
        private static SceneBuilder builder;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Player Player { get; private set; }
        public IEnumerable<Polygon> Obstacles { get; private set; }

        public static SceneBuilder Builder => builder ??= new SceneBuilder();

        private Scene(Player player, IEnumerable<Polygon> obstacles, int width, int height)
        {
            Player = player;
            Obstacles = obstacles;
            Width = width;
            Height = height;
        }

        public class SceneBuilder
        {
            private const float wallSize = Settings.WorldWallSize;

            private const string DefaultSceneStr =
                "#######\r\n" +
                "#     #\r\n" +
                "#     #\r\n" +
                "#  P  #\r\n" +
                "#     #\r\n" +
                "#     #\r\n" +
                "#######\r\n";

            private const string SingleBlockSceneStr =
                "           \r\n" +
                "           \r\n" +
                "           \r\n" +
                "  P    #   \r\n" +
                "           \r\n" +
                "           \r\n" +
                "           \r\n";

            private readonly IReadOnlyDictionary<char, Func<int, int, Shape>> shapeCtorByChar;

            public SceneBuilder()
            {
                shapeCtorByChar = new Dictionary<char, Func<int, int, Shape>>
                {
                    {
                        '#', (x, y) =>  {
                            var position = new Vector2(x * wallSize, y * wallSize);
                            return new Square(position, wallSize);
                        }
                    },
                    {
                        'P', (x, y) => new Player(x * wallSize, y * wallSize)
                    }
                };
            }

            public Scene Default => FromString(DefaultSceneStr);
            public Scene SingleBlockScene => FromString(SingleBlockSceneStr);

            public Scene FromString(string source)
            {
                var lines = source.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                var height = lines.Length;
                var width = lines[0].Length;

                var shapes = ParseShapes(lines);
                var obstacles = shapes.Where(shape => shape is Polygon).Cast<Polygon>();
                var player = (Player)shapes.First(shape => shape is Player);
                return new Scene(player, obstacles, width, height);
            }

            private IEnumerable<Shape> ParseShapes(string[] lines)
            {
                for (var y = 0; y < lines.Length; y++)
                    for (var x = 0; x < lines[y].Length; x++)
                    {
                        var c = lines[y][x];
                        if (shapeCtorByChar.ContainsKey(c))
                            yield return shapeCtorByChar[c](x, y);
                    }
            }
        }
    }
}