using System;

namespace PseudoWolfenstein.Utils
{
    public static class FloatExtension
    {
        public static float ToRadians(this float degrees)
        {
            return MathF.PI / 180.0f * degrees;
        }

        public static float ToDegrees(this float radians)
        {
            return 180.0f / MathF.PI * radians;
        }
    }
}