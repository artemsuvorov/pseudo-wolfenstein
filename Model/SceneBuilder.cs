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
                "SSSSSSSSSSSSSSSSSSSSSS\r\n" +
                "S         F          S\r\n" +
                "S                    S\r\n" +
                "S        BBB         S\r\n" +
                "S                    S\r\n" +
                "S                    S\r\n" +
                "S         P          S\r\n" +
                "S                    S\r\n" +
                "S                    S\r\n" +
                "S                    S\r\n" +
                "S                    S\r\n" +
                "S                    S\r\n" +
                "SSSSSSSSSSSSSSSSSSSSSS\r\n";

            internal const string Level_1 =
                //Real Prison
                "                                               WWW           \r\n" +
                "                                              W   W          \r\n" +
                "                                              W W W          \r\n" +
                "   SSSSSSSSS                 WWWWWWWWWWWWWWWWWWgW W          \r\n" +
                "  Sx    A   f               W     A     W       WdW          \r\n" +
                "  S        NS               W WWW   WWWbWDWWWWWDWWWWWW       \r\n" +
                "  S     H   L               WGWSx   xS     Se       F S      \r\n" +
                "  SDSSSSSSSS                 W Sx   xS     SSSSSSSSSS S      \r\n" +
                "  S S                  SSSSSSSSSSSDSSS     S        SqS      \r\n" +
                "  S S                 S              S     S     H  SS       \r\n" +
                "  S S                 S              S     D        S        \r\n" +
                "  S S                 S              S     S        S        \r\n" +
                "  S S                 S      F   F   S     S        S        \r\n" +
                "  S S                 S              S     S        S        \r\n" +
                "  S S                 j              jBBDBBSSSSDSSSS         \r\n" +
                "  BoBB                S     F   C    SB   B         B        \r\n" +
                " B    BBBBBBBBBBBBBBBBSC             SB   B         J        \r\n" +
                " T HA                         H      SB   B   F    nB        \r\n" +
                " B    BBBBBBBBBBBBBBBBSC             SB   B     F   J        \r\n" +
                "  BDBB                SSSSSSSjSSSSSSSSB   B         B        \r\n" +
                "  B B                 BBBBBBBBBBBBBBBBB   BBBBBBBBBBBBB      \r\n" +
                "  BAB                    BO       B           B        B     \r\n" +
                "  B B                    J  A     D           D     CH J     \r\n" +
                "  B B                    B        B           B        B     \r\n" +
                "  BDBBBBJBBBBBJBBBBBJBBBB BBBBBBBBB           BBBBBBBBB      \r\n" +
                "  B                     OB        B           B        B     \r\n" +
                "  B       C       C      J  C     D           D      P J     \r\n" +
                "  B                  F   B        B           B        B     \r\n" +
                "  B            F      w  BBBBBBBBBB           BBBBBBBBB      \r\n" +
                "  B                     dB                             B     \r\n" +
                "  B     F C       C      J  F                       A  J     \r\n" +
                "  B              n       B                             B     \r\n" +
                "   BJBBBBJBBBBJBBBBBJBBBBBBBBBBBJBBBBBBBJBBBBBBBJBBBBBB      \r\n";

            internal const string Level_2 =
                //Hell Lab
                " kkkkkkkkkkkkkkkkk  SSSSSSSSSSSSSS                                                                          \r\n" +
                " k       D       k S              S   SSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSS                           \r\n" +
                " k m G   kkkkkkSDSS        F       S S                      S                    S                          \r\n" +
                " k      sk A kSS Sx              CSSSS      H              CSC     F       F     SSS                        \r\n" +
                " k d G   D 4 kS1 D   F    F          g  P               7   D          F         b GS                       \r\n" +
                " k      sk A kSS Sx              CSSSS      A              CSC      F            SSS                        \r\n" +
                " k m G   kkkkkkSDSS                S S                      S                    S                          \r\n" +
                " k       D       k S     F        S   SSSSSSSSSSSSSSSSSSSSSSSRRRRRRRRRRRRRRRRDRRR                           \r\n" +
                " kkkkkkkkkkkkkkk k  SSSSSSSSSSSSSS   R                                           R                          \r\n" +
                "                                   RRR    wC               RAR                   RRR                        \r\n" +
                "                                  RF r           F         RHR     A        F    o AR                       \r\n" +
                "      SSSSSSSSSSSSSSSSSSS          RRR    AC               RAR               e   RRR                        \r\n" +
                "     Sx m               xS           R                                           R                          \r\n" +
                "     S                   S            SSSSSSSSRDRSSSSSSSSSSSRRRRRRRRRRRRRRiRRRRRR                           \r\n" +
                "     SG         a        S           R                      R                   CBB                         \r\n" +
                "     S          F       CSSSSSSSSSSSSRC                  H CRC         H    q     CRR                       \r\n" +
                "     S    F              D  H F      D       F              p                      NGR                      \r\n" +
                "     S    M             CSSSSSSSSSSSSRC        t         A CRC         A          CRR                       \r\n" +
                "     S    F              S           R                      R                   CBB                         \r\n" +
                "     S                   S            RRRRRRRRRRRRRRRRRRRRRRRRR      RRRRRiRRRRRR                           \r\n" +
                "     Sx                 xS                                    RO     R                                      \r\n" +
                "      SSS             SSS                                     R      R                                      \r\n" +
                "         R           R                                        R      R                                      \r\n" +
                "          RR       RR                                         R     OR                                      \r\n" +
                "            RC   CR                                           R      R                                      \r\n" +
                "             RR RR                                  RRRRRRRRRRR      RRRRRRRRR                              \r\n" +
                "              R R                                   i   O   D          D   F  R                             \r\n" +
                "              R R                                   RG      R          R    A i                             \r\n" +
                "              R R                                   RRRRRRRRR          RRRRRRR                              \r\n" +
                "              R R                                   i F     D          D    H R                             \r\n" +
                "              R R                                   RO      R          R    n i                             \r\n" +
                "              R R                                   RRiRRRRRR          RRRRRRR                              \r\n" +
                "              R R                                  R                  R                                     \r\n" +
                "              R R                                 R                  R                                      \r\n" +
                "              R R                                R                  R                                       \r\n" +
                "              R RRRRRRRRRRRRRRiRRRRRiRRRRRRRR RRR        F        OR   R   R                                \r\n" +
                "              R R          R     R    AR     R                   RRXR RXR RXR                               \r\n" +
                "              R D                            D      F      F     R  AR  HR  GR                              \r\n" +
                "              R R             R    OR       nR                   D           R                              \r\n" +
                "              RHRRRRRRRRRRRRRRRRRiRRRRRiRRRRR RRRiRRRRRRRRRiRRRRRRRRRRRRRRRRR                               \r\n" +
                "               R                                                                                            \r\n";

            internal const string Level_4 =
                //Citadel
                "           KKKKKKKKKKKKKKKKKKKKKKKKKKKKKK            SSSSjSSSSSSSSSSSSSSSjSSSSSSSSSSSSSSjSSSSSSSS           \r\n" +
                "          Kx         O      n           xK          S           O          F  H  A   n          HS          \r\n" +
                "          K    C                 C       K          S F  C    F    F     C                 F     QSSSS      \r\n" +
                "          K        F            F        K          S                                C           D X S      \r\n" +
                "          K   F                    F  H  QRRRRRRRRRRQ  A H    O  C  F          C           F   H QSS S      \r\n" +
                "          K  q          C     F          D   F      D                                 F          Skk kk     \r\n" +
                "          K                      F    A  QRRRRRRRRRRQ  A H           F       n     F      C    A QA   Ak    \r\n" +
                "          Kn      F     F                K          Q     C     F        C                       p     k    \r\n" +
                "          K    C             F   C       K          p               C           F            F   Qd   dk    \r\n" +
                "          Kx         O                  xK          Q  O                   n                     Skkkkk     \r\n" +
                "           KKKKKKKKKKKQDQKKKKKKKKKKKKKKKK            SSSSjSSSSSSSQDQSSSSSjSSSSSSSSSSSSSSQDQSSSSSS           \r\n" +
                "                      R R                                        R R             Sx     C C     xS          \r\n" +
                "                     RR RR                                      RR RR            S               S          \r\n" +
                "                    R s swR                                    RF   HR           S    F          S          \r\n" +
                "                     RR RR                                      RR RR            S         F   A S          \r\n" +
                "                      R R                                        R R             Sd     H       GS          \r\n" +
                "             KKKKKKKKKQoQKKKKKKKKKKSSS               KKKKKKKKKKKKQDQKKKKKKKKKKKK  SSSSSjSSSjSSSSS           \r\n" +
                "            S               n    sk  Ak             Kx           C C           xK                           \r\n" +
                "            S   n   n     O  n    D 9 k             K         H                 QKK                         \r\n" +
                "            S dn  n     n  n     sk  Ak             K   MF           A      F F DFXK                        \r\n" +
                "         SSSS O  n   On    O      SSSS              K         F                 QKK                         \r\n" +
                "        Kx  xS     n        n    ShhhhK             K         aF               CK                           \r\n" +
                "        K     QQ      n        QQh h hK            K               F            QKK                         \r\n" +
                "        K  P   p   O           D    hhK           K                M          F DHXK                        \r\n" +
                "        K      QQ      n      QQn h   K          K                             QKK                          \r\n" +
                "        K        SSSSSQDQSSSSShhhh h hKS        K                         A    xK                           \r\n" +
                "        K       OSS         SS        K S      K  H  KKKKKKKKKKKKKKKKKKKKKKKKKKK                            \r\n" +
                "        K        S S  H A  SnS  A  F  K  S    K     K                                                       \r\n" +
                "        K        S  S     S  S        K   QQKK  A  K                                                        \r\n" +
                "        K        S  OS   S  AS   F    K    D      K                                                         \r\n" +
                "        K       CQ  F SjSC   Q        Q    QQKKKKK                     KKKKKKK                              \r\n" +
                "        K        D    j j    b      F D    sS              KKKKKKKK   KGC N C K                             \r\n" +
                "        K       CQ  F SjSC   Q   F    Q    QQKKKKK        K        K  KC     CK                             \r\n" +
                "        K        S   SCACQQ  S        K    g      K       K DimART K  K       K                             \r\n" +
                "        K        S nS     D  S      F K   QQKK     K      K        K  K       K                             \r\n" +
                "        K        SGS   F  QQSS        K  S    K     K     K        K  K       K                             \r\n" +
                "        K        SSm      SSSS   F    K S      K     KKKKKKKKKQDQKKKKKKKKQDQKKK                             \r\n" +
                "        K       ASSSSSQDQSSSSS        KS        K                            xK                             \r\n" +
                "        K      QQ A       A   QQ      K          K                           xK                             \r\n" +
                "        K      D            F  p A  H K           KKKKKKKKKKKKKKKKKKKKKKKKKKKK                              \r\n" +
                "        K HA  QQ     F         QQ     K                                                                     \r\n" +
                "        Kx  xS           F       Sx  xK                                                                     \r\n" +
                "         SSSS     F          H   xSSSS                                                                      \r\n" +
                "        KH AS          F          S                                                                         \r\n" +
                "        K   S                A    S                                                                         \r\n" +
                "        Ke  D       F            xS                                                                         \r\n" +
                "         KKKKKKKKKKKKKKKKKKKKKKKKK                                                                          \r\n" +
                "                                                                                                            \r\n"; 
            internal const string Level_5 =
                //Last chance
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                    BBBBBBBBBBBBBBBBBBBB                                                        \r\n" +
                "                                                   B                    B                                                       \r\n" +
                "                                                  B                      B                                                      \r\n" +
                "                                                 B                        B                                                     \r\n" +
                "                                                B          B B B B B B B   B                                                    \r\n" +
                "                                               B          B B B B B B B B   B                                                   \r\n" +
                "                                              B          B B B B B B B B B   B                                                  \r\n" +
                "                                             B          B B B B B B B B B B   B                                                 \r\n" +
                "                                            B          B B B B B B B B B B B   B                                                \r\n" +
                "                                            Q         B B B B B B B B B B B B  B                                                \r\n" +
                "                                            D        B B B B B B B B B B B B B B                                                \r\n" +
                "                                            Q         B B B B B B B B B B B B  B                                                \r\n" +
                "                                            B          B B B B B B B B B B B   B                                                \r\n" +
                "                                             B          B B B B B B B B B B   B                                                 \r\n" +
                "                                              B          B B B B B B B B B   B                                                  \r\n" +
                "                                               B          B B B B B B B B   B                                                   \r\n" +
                "                                                B          B B B B B B B   B                                                    \r\n" +
                "                                                 B                        B                                                     \r\n" +
                "                                                  B                      B                                                      \r\n" +
                "                                                   B                    B                                                       \r\n" +
                "                                                    BBBBBBBBBBBBBBBBBBBB                                                        \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                      BBBBBBBB                                                            BBBQBBBB                              \r\n" +
                "                  BBBB        BBBB                                                    BBBBB       BBBB                          \r\n" +
                "               BBB                BBB                  SSSQDQSSS                   BBB                BBB                       \r\n" +
                "             BB                      BB               S         S                BB                      BB                     \r\n" +
                "            B                          B             S           S              B                          B                    \r\n" +
                "           B                            B           S             S            B                            B                   \r\n" +
                "          B                              B         S               S          B                              B                  \r\n" +
                "         B                                B        S               S         B                                B                 \r\n" +
                "        B                                  B       S               S        B                                  B                \r\n" +
                "        Q                                  SSSSSSSSQ               QSSSSSSSSQ                                  B                \r\n" +
                "        D                                  D       D       P       D        D                                  B                \r\n" +
                "        Q                                  SSSSSSSSQ               QSSSSSSSSQ                                  B                \r\n" +
                "        B                                  B       S               S        B                                  B                \r\n" +
                "         B                                B        S               S         B                                B                 \r\n" +
                "          B                              B         S               S          B                              B                  \r\n" +
                "           B                            B           S             S            B                            B                   \r\n" +
                "            B                          B             S           S              B                          B                    \r\n" +
                "             BB                      BB               S         S                BB                      BB                     \r\n" +
                "               BBB                BBB                  SSSQDQSSS                   BBB                BBB                       \r\n" +
                "                  BBBB        BBBB                                                    BBBBB       BBBB                          \r\n" +
                "                      BBBBBBBB                                                            BBBQDQBB                              \r\n" +
                "                                                                                             B B                                \r\n" +
                "                       BBBBBBBB                                                              B B                                \r\n" +
                "                   BBBB        BBBB                                                          B B                                \r\n" +
                "                BBB                BBB                                                       B B                                \r\n" +
                "              BB                      BB                                                     B B                                \r\n" +
                "             B            BBBBBBBBB     B                                                    B B                                \r\n" +
                "            B          BBB         BBB   B                                                   B B                                \r\n" +
                "           B          B               B   B                                                  B B                                \r\n" +
                "          B          B                 B   B                                                 QDQ                                \r\n" +
                "         B          B                   B   B                                                                                   \r\n" +
                "         Q         B                     B  B                                                                                   \r\n" +
                "         D         B                     B  B                                                                                   \r\n" +
                "         Q         B                     B  B                                                                                   \r\n" +
                "         B          B                   B   B                                                                                   \r\n" +
                "          B          B                 B   B                                                                                    \r\n" +
                "           B          B               B   B                                                                                     \r\n" +
                "            B          BBB         BBB   B                                                                                      \r\n" +
                "             B            BBBBBBBBB     B                                                                                       \r\n" +
                "              BB                      BB                                                                                        \r\n" +
                "                BBB                BBB                                                                                          \r\n" +
                "                   BBBB        BBBB                                                                                             \r\n" +
                "                       BBBBBBBB                                                                                                 \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                \r\n" +
                "                                                                                                                                    ";

            public Scene FromString(string source, string name)
            {
                var lines = source.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
                var height = lines.Length;
                var width = lines[0].Length;

                var shapes = ParseShapes(width, height, lines);
                RotateDoors(width, height, shapes);

                //var walls = shapes.Cast<Shape>().Where(shape => shape is Wall).Cast<Wall>().ToList();
                //var panes = shapes.Cast<Shape>().Where(shape => shape is Pane).Cast<Pane>().ToList();
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
                return new Scene(name, playerPosition, shapes, width, height);
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