using PseudoWolfenstein.Utils;
using System.Numerics;

namespace PseudoWolfenstein.Core
{
    public class Ray
    {
        private const float Length = 4096f;

        public Vector2 Start { get; private set; }
        public Vector2 End { get; private set; }
        public Vector2 Direction { get; private set; }

        // in radians
        public float Rotation { get; private set; }


        public Ray(Vector2 start, float rotation)
        {
            Start = start;
            Rotation = rotation;
            Direction = Vector2.UnitX.RotateClockwise(rotation);
            End = start + Direction * Length;
        }

        public bool IsCrossing(Vector2 v11, Vector2 v12, out Vector2 crossingPoint)
        {
            return MathF2D.AreSegmentsCrossing(Start, End, v11, v12, out crossingPoint);
        }
    }
}