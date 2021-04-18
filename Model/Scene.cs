using System;
using System.Collections.Generic;
using System.Linq;

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
            internal const string DefaultSceneStr =
                "SSSSSSS\r\n" +
                "S     S\r\n" +
                "S     S\r\n" +
                "S  P  S\r\n" +
                "S     S\r\n" +
                "S     S\r\n" +
                "SSSSSSS\r\n";

            internal const string DebugSceneStr =
                "SSSBSSSBBBBBS\r\n" +
                "S     B     S\r\n" +
                "S     B     S\r\n" +
                "S  P        S\r\n" +
                "S     B     S\r\n" +
                "S     B     S\r\n" +
                "SSSSSSSSSSSSS\r\n";

            internal const string SingleBlockSceneStr =
                "           \r\n" +
                "           \r\n" +
                "           \r\n" +
                "    P  S   \r\n" +
                "           \r\n" +
                "           \r\n" +
                "           \r\n";

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
                        var shape = ShapeFactory.InstantiateShapeAtOrDefault(x, y, c);
                        if (shape is object) yield return shape;
                    }
            }
        }
    }
}