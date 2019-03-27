using System;

namespace TestApp
{
    public static class DoubleExtensions
    {
        public const double Epsilon = 1.0e-6;

        public static bool IsNearTo(this double val1, double val2, int digits = 3)
        {
            var difference = 1.0d / Math.Pow(10, digits);

            return Math.Abs(val1 - val2) < difference;
        }

        public static bool IsAlmostEqual(this double val1, double val2, double epsilon = DoubleExtensions.Epsilon)
        {
            return Math.Abs(val1 - val2) < epsilon;
        }

        public static bool IsBetween(this double val, double from, double to)
        {
            return val > from && val < to;
        }

        public static decimal ToDecimal(this double val)
        {
            return Convert.ToDecimal(val);
        }

        public static int ToRoundedInt(this double val)
        {
            return (int)Math.Round(val);
        }

        public static double ToRadians(this double val)
        {
            return val * Math.PI / 180d;
        }

        public static double Clamp(this double val, double min, double max)
        {
            return Math.Min(max, Math.Max(val, min));
        }
    }
}
