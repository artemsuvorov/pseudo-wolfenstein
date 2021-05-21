using System;
using System.Drawing;
using System.Numerics;

namespace PseudoWolfenstein.Utils
{
    public static class VectorExtensions
    {
        public static Vector2 SafeNormalize(this Vector2 vector)
        {
            if (vector == Vector2.Zero) return Vector2.Zero;
            return Vector2.Normalize(vector);
        }

        public static bool IsOrthogonal(this Vector2 self, Vector2 other)
        {
            return Vector2.Dot(self, other).IsEqual(0.0f);
        }

        public static float AngleTo(this Vector2 self, Vector2 other)
        {
            return MathF.Atan2(other.Y-self.Y, other.X-self.X);
        }

        public static Vector2 RotateClockwise(this Vector2 self, float angle)
        {
            var x =   self.X * MathF.Cos(angle) + self.Y * MathF.Sin(angle);
            var y = - self.X * MathF.Sin(angle) + self.Y * MathF.Cos(angle);
            return new Vector2(x, y);
        }

        public static Vector2 RotateCounterClockwise(this Vector2 self, float angle)
        {
            var x = self.X * MathF.Cos(angle) - self.Y * MathF.Sin(angle);
            var y = self.X * MathF.Sin(angle) + self.Y * MathF.Cos(angle);
            return new Vector2(x, y);
        }

        // todo: refactor it
        public static Vector2 Lengthen(this Vector2 self, float offset, Vector2 direction)
        {
            //var magnitude = direction.Length();
            var translation = direction.SafeNormalize() * offset;
            //direction.X /= magnitude;
            //direction.Y /= magnitude;
            //var translation = new PointF(offset * direction.X, offset * direction.Y);

            using var m = new System.Drawing.Drawing2D.Matrix();
            m.Translate(translation.X, translation.Y);
            var pts = new PointF[] { new PointF(self.X, self.Y) };
            m.TransformPoints(pts);
            return new Vector2(pts[0].X, pts[0].Y);
        }

    }
}