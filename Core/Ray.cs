using PseudoWolfenstein.Utils;
using System.Numerics;

namespace PseudoWolfenstein.Core
{
    public class Ray
    {
        private const float Length = 65536;

        public readonly Vector2 Start;
        public readonly Vector2 End;

        // in radians
        public readonly float Rotation;

        public Ray(Vector2 start, float rotation)
        {
            Start = start;
            Rotation = rotation;

            var directingVector = start + Vector2.UnitX * Length;
            End = directingVector.RotateClockwise(rotation);
        }

        public bool IsCrossing(Vector2 v11, Vector2 v12, out Vector2 crossingPoint)
        {
            return MathF2D.AreSegmentsCrossing(Start, End, v11, v12, out crossingPoint);
        }
    }
}