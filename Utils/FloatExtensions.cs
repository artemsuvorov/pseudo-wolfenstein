using System;

namespace PseudoWolfenstein.Utils
{
    public static class FloatExtensions
    {
        private const float DefaultPrecision = 1e-5f;

        public static bool IsEqual(this float self, float other, float precision = DefaultPrecision)
        {
            return MathF.Abs(self - other) < precision;
        }

        public static bool IsNotEqual(this float self, float other, float precision = DefaultPrecision)
        {
            return !self.IsEqual(other, precision);
        }
    }
}