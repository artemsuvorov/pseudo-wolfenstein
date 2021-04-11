using System;
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
    }
}