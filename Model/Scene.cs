using System;
using System.Collections.Generic;
using System.Linq;

namespace PseudoWolfenstein.Model
{
    public partial class Scene
    {
        private static SceneBuilder builder;

        public int Width { get; private set; }
        public int Height { get; private set; }

        public Player Player { get; private set; }

        public IReadOnlyCollection<Polygon> Obstacles { get; private set; }
        public IReadOnlyCollection<Polygon> Walls { get; private set; }
        public IReadOnlyCollection<Polygon> Panes { get; private set; }

        public static SceneBuilder Builder => builder ??= new SceneBuilder();

        private Scene(Player player, IReadOnlyCollection<Polygon> walls, IReadOnlyCollection<Polygon> panes, int width, int height)
        {
            Player = player;
            Walls = walls;
            Panes = panes;
            Obstacles = Walls.Concat(Panes).ToList();
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

            internal const string Temp =
                "BBB\r\n" +
                "BBB\r\n" +
                "  P\r\n";

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

                var shapes = ParseShapes(width, height, lines).Cast<Shape>();
                var walls = shapes.Where(shape => shape is Wall).Cast<Polygon>().ToList();
                var panes = shapes.Where(shape => shape is Pane).Cast<Polygon>().ToList();
                var player = (Player)shapes.First(shape => shape is Player);
                //var merger = new ShapeUnifier(shapes);
                //var isles = merger.GetAdjecentShapeIsles();
                //var borders = isles
                //    .Select(isle => PolygonMerger.GetShapesCommonBorderOrDefault(isle.Item2))
                //    .Where(border => border is object)
                //    .ToList();

                //var player = (Player)isles.First(isle => isle.Item1 == 'P').Item2.First();
                //var walls = borders.Select(border =>
                //{
                //    var p = new Polygon('B', border)
                //    {
                //        Texture = Core.Repository.Textures.BlueWall
                //    };
                //    return p;
                //}).ToArray();
                //var panes = shapes.Cast<Shape>().Where(shape => shape is Pane).Cast<Polygon>().ToArray();
                return new Scene(player, walls, panes, width, height);
            }

            private Shape[,] ParseShapes(int width, int height, string[] lines)
            {
                var shapes = new Shape[width, height];

                for (var y = 0; y < height; y++)
                    for (var x = 0; x < width; x++)
                    {
                        var c = lines[y][x];
                        shapes[x, y] = ShapeFactory.InstantiateShapeAtOrDefault(x, y, c);
                    }

                return shapes;
            }
        }
    }
}