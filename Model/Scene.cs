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

        public IEnumerable<Polygon> Obstacles => Walls.Concat(Panes);
        public IReadOnlyCollection<Polygon> Walls { get; private set; }
        public IReadOnlyCollection<Polygon> Panes { get; private set; }

        public static SceneBuilder Builder => builder ??= new SceneBuilder();

        private Scene(Player player, IReadOnlyCollection<Polygon> walls, IReadOnlyCollection<Polygon> panes, int width, int height)
        {
            Player = player;
            Walls = walls;
            Panes = panes;
            Width = width;
            Height = height;
        }


        public void Update()
        {
            foreach (Pane pane in Panes)
                pane.UpdateTransform(Player);

            Player.Update(this);
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
                "SSSBSSSBBBBBBBS\r\n" +
                "S     B       S\r\n" +
                "S     B   C   S\r\n" +
                "S     B       S\r\n" +
                "S  P       O  S\r\n" +
                "S     B       S\r\n" +
                "S     B       S\r\n" +
                "S     B    F  S\r\n" +
                "S     B       S\r\n" +
                "SSSSSSSSSSSSSSS\r\n";

            internal const string SingleBlockSceneStr =
                "            \r\n" +
                "            \r\n" +
                "      C     \r\n" +
                "            \r\n" +
                "            \r\n" +
                "     P      \r\n" +
                "            \r\n" +
                "            \r\n" +
                "            \r\n";

            internal const string Level_1 =
                "                             OOOOOOOOOOOOOOOOOOOOOOO               \r\n" +
                "   SSSSSSSS                 O           O           O              \r\n" +
                "  S        S                O OOOO OOOO O OOOOO OOOO               \r\n" +
                "  S        S                 O S     S     S        S              \r\n" +
                "  S SSSSSSS                    S     S     SSSSSSSSSS              \r\n" +
                "  S S                  SSSSSSSSSSS SSS     S        S              \r\n" +
                "  S S                 S              S     S        S              \r\n" +
                "  S S                 S              S              S              \r\n" +
                "  S S                 S              S     S        S              \r\n" +
                "  S S                 S              S     SSSS SSSS               \r\n" +
                "  S S                 S              S     S       S               \r\n" +
                "  S S                 S              SBB BBS       S               \r\n" +
                "  B BB               B                B   B         B              \r\n" +
                " B    BBBBBBBBBBBBBBBB                B   B         B              \r\n" +
                " B                                    B   B         B              \r\n" +
                " B    BBBBBBBBBBBBBBBB                B   B         B              \r\n" +
                "  B BB               B                B   B         B              \r\n" +
                "  B B                 BBBBBBBBBBBBBBBBB   BBBBBBBBBBBBB            \r\n" +
                "  B B                    B        B           B        B           \r\n" +
                "  B B                    B                             B           \r\n" +
                "  B B                    B        B           B        B           \r\n" +
                "  B BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB           BBBBBBBBB            \r\n" +
                "  B                      B        B           B        B           \r\n" +
                "  B                      B  P                          B           \r\n" +
                "  B                      B        B           B        B           \r\n" +
                "  B                      BBBBBBBBBB           BBBBBBBBB            \r\n" +
                "  B                      B                             B           \r\n" +
                "  B                      B                             B           \r\n" +
                "  B                      B                             B           \r\n" +
                "   BBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBBB            \r\n";

            public Scene Default => FromString(DefaultSceneStr);
            public Scene SingleBlockScene => FromString(SingleBlockSceneStr);

            public Scene FromString(string source)
            {
                var lines = source.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                var height = lines.Length;
                var width = lines[0].Length;

                var shapes = ParseShapes(lines);
                var walls = shapes.Where(shape => shape is Wall).Cast<Polygon>().ToArray();
                var panes = shapes.Where(shape => shape is Pane).Cast<Polygon>().ToArray();
                var player = (Player)shapes.First(shape => shape is Player);
                return new Scene(player, walls, panes, width, height);
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