using System;
using System.Linq;

namespace PseudoWolfenstein.Model
{
    public partial class Scene
    {
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
                "S  A  B       S\r\n" +
                "S     B    F  S\r\n" +
                "S     B       S\r\n" +
                "SSSSSSSSSSSSSSS\r\n";

            internal const string SingleBlockSceneStr =
                "              \r\n" +
                "              \r\n" +
                "        S     \r\n" +
                "        D     \r\n" +
                "        S     \r\n" +
                "   P          \r\n" +
                "              \r\n" +
                "              \r\n" +
                "              \r\n";

            internal const string Level_1 =
                "                                               WWW                 \r\n" +
                "                                              W   W                \r\n" +
                "                                              W W W                \r\n" +
                "                            WWWWWWWWWWWWWWWWWWWDW W                \r\n" +
                "   SSSSSSSS                 W           W       СHW                \r\n" +
                "  S        N                WUWWW   WWWDWDWWWWWDWWWWWW             \r\n" +
                "  S        S                 W S     S     SU       F S            \r\n" +
                "  SDSSSSSSS                    S     S     SSSSSSSSSSUS            \r\n" +
                "  S S                  SSSSSSSSSSSDSSS     S       HSS             \r\n" +
                "  S S                 S              S     S        S              \r\n" +
                "  S S                 S              S     D        S              \r\n" +
                "  S S                 S    F         S     S        S              \r\n" +
                "  S S                 S              S     SSSSDSSSS               \r\n" +
                "  S S                 S              S     S       S               \r\n" +
                "  S S                 S              SBBDBBS       S               \r\n" +
                "  BDBB               B          C     B   B         B              \r\n" +
                " B    BBBBBBBBBBBBBBBBC               B   B         J              \r\n" +
                " B H                      F     F     B   B         B              \r\n" +
                " B    BBBBBBBBBBBBBBBBC               B   B       F J              \r\n" +
                "  1D1B               BH              AB   B         B              \r\n" +
                "  B B                 BBBBBBBBBBBBBBBBB   BBBBBBBBBBBBB            \r\n" +
                "  B B                    B        B           B        B           \r\n" +
                "  B B                    B HC     D           D     C  B           \r\n" +
                "  B B                    B        B           B        B           \r\n" +
                "  BDBBBBJBBBBBJBBBBBJBBBB BBBBBBBBB           BBBBBBBBB            \r\n" +
                "  B                      B        B           B        B           \r\n" +
                "  B       C       C  F   B  C     D           D     P  B           \r\n" +
                "  B                      B        B           B        B           \r\n" +
                "  B            F       U BBBBBBBBBB           BBBBBBBBB            \r\n" +
                "  B                      B                             B           \r\n" +
                "  B     F C       C  F   B                          A  B           \r\n" +
                "  B                      B                             B           \r\n" +
                "   BBBBBJBBBBBJBBBBBJBBBBBBBBBBBJBBBBBBBJBBBBBBBJBBBBBB            \r\n";

            internal const string Level_2 =
                "                                                                             \r\n" +
                "                    SSSSSSSSSSSSSS                                           \r\n" +
                "                   S  F           S   SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS        \r\n" +
                "                S S        F       S S                               S                              S       \r\n" +
                "               SGS   SSS      CSSSSSSS                              CSC                             SSS       \r\n" +
                "              S1 D   DUS             D  P                            D                              D GS     \r\n" +
                "               SGS   SSS      CSSSSSSS                              CSC                             SSS       \r\n" +
                "                S S                S S                               S                              S       \r\n" +
                "                   S     F     F  S   SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSRRRRRRRRRRRRRRRRRRRRRRRRRDRRRR        \r\n" +
                "                    SSSSSSSSSSSSSS   R                              S                               R       \r\n" +
                "                                   RRR    U                         S  R                            RRR       \r\n" +
                "                                  RF D                              S  R                            D AR     \r\n" +
                "      SSSSSSSSSSSSSSSSSSS          RRR                              S  R                       U    RRR       \r\n" +
                "     S                   S           R                                 R                            R       \r\n" +
                "     S          F        S            RRRRDRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR        \r\n" +
                "     S         FM        S           R                               R                              RR        \r\n" +
                "     S          F       CSSSSSSSSSSSSRC                           H CRC                        U      RR      \r\n" +
                "     S    F              D    F      D                               D                                  N    \r\n" +
                "     S   FM             CSSSSSSSSSSSSRC   U                       A CRC                               RR      \r\n" +
                "     S    F              S           R                               R                              RR       \r\n" +
                "     S                   S            RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR        RRRRRRRRRRRRRRRRRRRR        \r\n" +
                "     S                   S                                              R   R   R                           \r\n" +
                "      SSS             SSS                                              RR  RO  R                                   \r\n" +
                "         R   F   F   R                                               R     RRGR                                    \r\n" +
                "          RR       RR                                             R R   R  R R                                     \r\n" +
                "            R     R                                              R        R                                        \r\n" +
                "             RR RR                                        RRRRRRR    R  RR                                         \r\n" +
                "              R R                                        R       R   R R                                           \r\n" +
                "              R R                                       R              R                                           \r\n" +
                "              R R                                      R       RA    ROR                    \r\n" +
                "              R R                                     R    R    R      R                                           \r\n" +
                "              R R                                    R            R   R                                            \r\n" +
                "              R R                                   R    R           R                                             \r\n" +
                "              R R                                  R     O    R     R                                              \r\n" +
                "              R R                                 R      R         R                                               \r\n" +
                "              R R                                R                R                                                \r\n" +
                "              R R                               R       R    R   R                                                 \r\n" +
                "              R R                              R      R      O  R                                                  \r\n" +
                "              R RRRRRRRRRRRRRRRRRRRRRRRRRRRRR R         R      R   R   R   R                               \r\n" +
                "              R R          R     R  O  R     R                  R RXR RXR RXR                                     \r\n" +
                "              R D                               R                R   R   R  LR                                     \r\n" +
                "              R R             R     R  A  R  R                               R                                  \r\n" +
                "              RHRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR                               \r\n" +
                "              RRR                                                                                                  \r\n";

            public Scene Default => FromString(DefaultSceneStr);
            public Scene SingleBlockScene => FromString(SingleBlockSceneStr);

            public Scene FromString(string source)
            {
                var lines = source.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                var height = lines.Length;
                var width = lines[0].Length;

                var shapes = ParseShapes(width, height, lines);
                RotateDoors(width, height, shapes);

                var walls = shapes.Cast<Shape>().Where(shape => shape is Wall).Cast<Wall>().ToList();
                var panes = shapes.Cast<Shape>().Where(shape => shape is Pane).Cast<Pane>().ToList();
                var player = (Player)shapes.Cast<Shape>().First(shape => shape is Player);
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

            private void RotateDoors(int width, int height, Shape[,] shapes)
            {
                for (int y = 0; y < height; y++)
                    for (var x = 0; x < width; x++)
                        if (shapes[x,y] is Door door)
                            if ((x-1 >= 0 && shapes[x-1, y] is null) || (x+1 < width && shapes[x+1, y] is null))
                                door.Rotate();
            }
        }
    }
}