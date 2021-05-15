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
                "        SF    \r\n" +
                "        D     \r\n" +
                "        S     \r\n" +
                "   P          \r\n" +
                "              \r\n" +
                "              \r\n" +
                "              \r\n";

            internal const string Level_1 =
                //Real Prison
                "                                               WWW                 \r\n" +
                "                                              W   W                \r\n" +
                "                                              W W W                \r\n" +
                "   SSSSSSSSS                 WWWWWWWWWWWWWWWWWWgW W                \r\n" +
                "  Sx        f               W           W       WdW                \r\n" +
                "  S        NS               WGWWW   WWWbWDWWWWWDWWWWWW             \r\n" +
                "  S         L                W Sx   xS     Se       F S            \r\n" +
                "  SDSSSSSSSS                   Sx   xS     SSSSSSSSSSqS            \r\n" +
                "  S S                  SSSSSSSSSSSDSSS     S        SS             \r\n" +
                "  S S                 S              S     S     H  S              \r\n" +
                "  S S                 S              S     D        S              \r\n" +
                "  S S                 S    F         S     S        S              \r\n" +
                "  S S                 S              S     S        S              \r\n" +
                "  S S                 S              S     S        S              \r\n" +
                "  S S                 j              jBBDBBSSSSDSSSS               \r\n" +
                "  BoBB                S         C    SB   B         B              \r\n" +
                " B    BBBBBBBBBBBBBBBBSC             SB   B         J              \r\n" +
                " T H                      F     F    SB   B   F    nB              \r\n" +
                " B    BBBBBBBBBBBBBBBBSC             SB   B      F  J              \r\n" +
                "  BDBB                SSSSSSSjSSSSSSSSB   B         B              \r\n" +
                "  B B                 BBBBBBBBBBBBBBBBB   BBBBBBBBBBBBB            \r\n" +
                "  B B                    BO       B           B        B           \r\n" +
                "  B B                    J        D           D     CH J           \r\n" +
                "  B B                    B        B           B        B           \r\n" +
                "  BDBBBBJBBBBBJBBBBBJBBBB BBBBBBBBB           BBBBBBBBB            \r\n" +
                "  B                     OB        B           B        B           \r\n" +
                "  B       C       C  F   J  C     D           D      P J           \r\n" +
                "  B                      B        B           B        B           \r\n" +
                "  B            F      w  BBBBBBBBBB           BBBBBBBBB            \r\n" +
                "  B                     dB                             B           \r\n" +
                "  B     F C       C  F   J  F                       A  J           \r\n" +
                "  B              n       B                             B           \r\n" +
                "   BJBBBBJBBBBJBBBBBJBBBBBBBBBBBJBBBBBBBJBBBBBBBJBBBBBB            \r\n";

            internal const string Level_2 =
                " kkkkkkkkkkkkkkkkk  SSSSSSSSSSSSSS                                                                          \r\n" +
                " k       D       k S  F           S   SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS        \r\n" +
                " k m G   kkkkkkSDSS        F       S S                               S                              S       \r\n" +
                " k      sk A kSS Sx              CSSSS          5     7     9       CSC          F                  SSS     \r\n" +
                " k d G   D   kS1 D   F               g  P                            D                    F         b GS    \r\n" +
                " k      sk A kSS Sx        F     CSSSS          6     8     0       CSC              F              SSS     \r\n" +
                " k m G   kkkkkkSDSS                S S                               S                              S       \r\n" +
                " k       D       k S     F     F  S   SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSRRRRRRRRRRRRRRRRRRRRRRRRRRDRRR        \r\n" +
                " kkkkkkkkkkkkkkk k  SSSSSSSSSSSSSS   R                                                              R       \r\n" +
                "                                   RRR    wC                        R R        F                    RRR     \r\n" +
                "                                  RF r                F             RSR                             o AR    \r\n" +
                "      SSSSSSSSSSSSSSSSSSS          RRR     C                        RGR            F            e   RRR     \r\n" +
                "     Sx m               xS           R                                                              R       \r\n" +
                "     S          F        S            SSSSSSSSSSSSSSSSSRDRSSSSSSSSSSSRRRRRRRRRRRRiRRRRRRRRRRRiRRRRRR        \r\n" +
                "     SG        Fa        S           R                               R                             CBB      \r\n" +
                "     S          F       CSSSSSSSSSSSSRC                       F   H CRC                        q     CRR    \r\n" +
                "     S    F              D    F      D                               p                                NGR   \r\n" +
                "     S   FM             CSSSSSSSSSSSSRC                 t   F     A CRC                              CRR    \r\n" +
                "     S    F              S           R                               R                             CBB      \r\n" +
                "     S                   S            RRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRRR     RRRRiRRRRRRRRRRRiRRRRRR        \r\n" +
                "     Sx        F        xS                                            R     R                               \r\n" +
                "      SSS             SSS                                            R     R                                \r\n" +
                "         R           R                                              R    OR                                 \r\n" +
                "          RR       RR                                              R     R                                  \r\n" +
                "            RC   CR                                               R     R                                   \r\n" +
                "             RR RR                                  RRRRRRRRRRRRRR     RRRRRRR                              \r\n" +
                "              R R                                   i   O   D          D   F  R                             \r\n" +
                "              R R                                   RG      R          R      i                             \r\n" +
                "              R R                                   RRRRRRRRR          RRRRRRR                              \r\n" +
                "              R R                                   i F     D          D      R                             \r\n" +
                "              R R                                   RO      R          R    n i                             \r\n" +
                "              R R                                   RRiRRRRRR          RRRRRRR                              \r\n" +
                "              R R                                    RORRRRRR          R                                    \r\n" +
                "              R R                                    RARRsRR          R                                     \r\n" +
                "              R R                                RRRRRDRRDR          R                                      \r\n" +
                "              R R                               R           F      OR                                       \r\n" +
                "              R R                              RO  F               R                                        \r\n" +
                "              R RRRRRRRRRRRRRRiRRRRRiRRRRRRRR R           F       RR   R   R                                \r\n" +
                "              R R          R     R    AR     R                   RRXR RXR RXR                               \r\n" +
                "              R D                            D      F        F   R   R  HR  GR                              \r\n" +
                "              R R             R    OR       nR                   D           R                              \r\n" +
                "              RHRRRRRRRRRRRRRRRRRiRRRRRiRRRRR RRRiRRRRRRRRRiRRRRRRRRRRRRRRRRR                               \r\n" +
                "               R                                                                                            \r\n";

            internal const string Level_4 =
                "                                                                                                            \r\n" +
                "                                                                                                            \r\n" +
                "                                                                                                            \r\n" +
                "                                                                                                            \r\n" +
                "           KKKKKKKKKKKKKKKKKKKKKKKKKKKKKK            SSSSjSSSSSSSSSSSSSSSjSSSSSSSSSSSSSSjSSSSSSSS           \r\n" +
                "          Kx         O      n           xK          S           O          F         n          HS          \r\n" +
                "          K    C        F        C       K          S F  C    F    F     C     F           F     QSSSS      \r\n" +
                "          K        F            F        K          S                                C           D X S      \r\n" +
                "          K   F                    F     QRRRRRRRRRRQ         O  C  F   F      C           F     QSS S      \r\n" +
                "          K  q    F     C     F          D   F      D     F                           F          Skk kk     \r\n" +
                "          K                      F       QRRRRRRRRRRQ                F       n     F      C      QA   Ak    \r\n" +
                "          Kn      F     F                K          Q     C     F        C                       p     k    \r\n" +
                "          K    C             F   C       K          p               C           F      F     F   Qd   dk    \r\n" +
                "          Kx         O                  xK          Q  O                   n                     Skkkkk     \r\n" +
                "           KKKKKKKKKKKQDQKKKKKKKKKKKKKKKK            SSSSjSSSSSSSQDQSSSSSjSSSSSSSSSSSSSSQDQSSSSSS           \r\n" +
                "                      R R                                        R R             Sx     C C     xS          \r\n" +
                "                     RR RR                                      RR RR            S               S          \r\n" +
                "                    R s swR                                    RF    R           S    F          S          \r\n" +
                "                     RR RR                                      RR RR            S         F     S          \r\n" +
                "                      R R                                        R R             Sd     H       GS          \r\n" +
                "             KKKKKKKKKQoQKKKKKKKKKKSSS               KKKKKKKKKKKKQDQKKKKKKKKKKKK  SSSSSjSSSjSSSSS           \r\n" +
                "            S               n    sk   k             Kx           C C           xK                           \r\n" +
                "            S   n   n     O  n    D   k             K   F                       QKK                         \r\n" +
                "            S dn  n     n  n     sk   k             K  FMF                F F F DFXK                        \r\n" +
                "         SSSS O  n   On    O      SSSS              K         F                 QKK                         \r\n" +
                "        Kx  xS     n        n    ShhhhK             K        FaF               CK                           \r\n" +
                "        K     QQ      n        QQh h hK            K          F    F            QKK                         \r\n" +
                "        K  P   p   O           D    hhK           K                MF       F F DHXK                        \r\n" +
                "        K      QQ      n      QQn h   K          K          F      F            QKK                         \r\n" +
                "        K        SSSSSQDQSSSSShhhh h hKS        K                              xK                           \r\n" +
                "        K       OSS         SS        K S      K     KKKKKKKKKKKKKKKKKKKKKKKKKKK                            \r\n" +
                "        K        S S       SnS     F  K  S    K     K                                                       \r\n" +
                "        K        S  S     S  S        K   QQKK     K                                                        \r\n" +
                "        K        S  OS   S   S   F    K    D      K                                                         \r\n" +
                "        K       CQ  F SjSC   Q        Q    QQKKKKK                     KKKKKKK                              \r\n" +
                "        K        D    j j    b      F D    sS              KKKKKKKK   KGC N C K                             \r\n" +
                "        K       CQ  F SjSC   Q   F    Q    QQKKKKK        K        K  KC     CK                             \r\n" +
                "        K        S   SC CQQ  S        K    g      K       K DimART K  K       K                             \r\n" +
                "        K        S nS     D  S      F K   QQKK     K      K        K  K       K                             \r\n" +
                "        K        SGS   F  QQSS        K  S    K     K     K        K  K       K                             \r\n" +
                "        K        SSm      SSSS   F    K S      K     KKKKKKKKKQDQKKKKKKKKQDQKKK                             \r\n" +
                "        K        SSSSSQDQSSSSS        KS        K                            xK                             \r\n" +
                "        K      QQ             QQ      K          K                           xK                             \r\n" +
                "        K      D            F  p      K           KKKKKKKKKKKKKKKKKKKKKKKKKKKK                              \r\n" +
                "        K HA  QQ     F         QQ     K                                                                     \r\n" +
                "        Kx  xS           F       Sx  xK                                                                     \r\n" +
                "         SSSS     F              xSSSS                                                                      \r\n" +
                "        KH AS          F          S                                                                         \r\n" +
                "        K   S                     S                                                                         \r\n" +
                "        Ke  D       F            xS                                                                         \r\n" +
                "         KKKKKKKKKKKKKKKKKKKKKKKKK                                                                          \r\n" +
                "                                                                                                            \r\n" +
                "                                                                                                            \r\n" +
                "                                                                                                            \r\n" +
                "                                                                                                            \r\n" +
                "                                                                                                                ";

            public Scene Default => FromString(DefaultSceneStr, nameof(DebugSceneStr));         

            public Scene SingleBlockScene => FromString(SingleBlockSceneStr, nameof(SingleBlockScene));

            public Scene FromString(string source, string name)
            {
                var lines = source.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                var height = lines.Length;
                var width = lines[0].Length;

                var shapes = ParseShapes(width, height, lines);
                RotateDoors(width, height, shapes);

                var walls = shapes.Cast<Shape>().Where(shape => shape is Wall).Cast<Wall>().ToList();
                var panes = shapes.Cast<Shape>().Where(shape => shape is Pane).Cast<Pane>().ToList();
                var playerPosition = shapes.Cast<Shape>().First(shape => shape is PlaceHolder).Position;
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
                return new Scene(name, playerPosition, walls, panes, width, height);
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