using System;
using System.Numerics;

namespace PseudoWolfenstein.Core
{
    public static class MathF2D
    {
        // returns z value of the resulting vector3
        public static float CrossProduct(Vector2 first, Vector2 second)
        {
            return first.X * second.Y - first.Y * second.X;
        }

        // source : https://habr.com/ru/post/267037/
        // author : https://habr.com/ru/users/vladvic/
        public static bool AreSegmentsCrossing(Vector2 v11, Vector2 v12, Vector2 v21, Vector2 v22, out Vector2 crossingPoint)
        {
            crossingPoint = default;

            Vector2 segment1 = v12 - v11, segment2 = v22 - v21;
            float prod1, prod2;

            prod1 = MathF2D.CrossProduct(segment1, v21 - v11);
            prod2 = MathF2D.CrossProduct(segment1, v22 - v11);

            if (MathF.Sign(prod1) == MathF.Sign(prod2) || prod1 == 0 || prod2 == 0)
                return false;

            prod1 = MathF2D.CrossProduct(segment2, v11 - v21);
            prod2 = MathF2D.CrossProduct(segment2, v12 - v21);

            if (MathF.Sign(prod1) == MathF.Sign(prod2) || prod1 == 0 || prod2 == 0)
                return false;

            var cx = v11.X + segment1.X * MathF.Abs(prod1) / MathF.Abs(prod2 - prod1);
            var cy = v11.Y + segment1.Y * MathF.Abs(prod1) / MathF.Abs(prod2 - prod1);
            crossingPoint = new Vector2(cx, cy);

            return true;
        }
    }
}