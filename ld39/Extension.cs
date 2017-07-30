using System;
using Microsoft.Xna.Framework;

namespace ld39
{
    static class Extension
    {
        public static Point GetIndexPosition(this Vector2 vector)
        {
            int indexX = (int)Math.Floor(vector.X / 64) - 5;
            int indexY = (int)Math.Floor(vector.Y / 64);
            Point result = new Point(indexX, indexY);
            return result.Clamp(0, 9, 0, 9);
        }

        public static Vector2 GetPosition(this Point point)
        {
            return new Vector2((point.X * 64) + 320, point.Y * 64);
        }

        public static Point Clamp(this Point point, int xMin, int xMax, int yMin, int yMax)
        {
            if (point.X < xMin) point.X = xMin;
            if (point.X > xMax) point.X = xMax;
            if (point.Y < yMin) point.Y = yMin;
            if (point.Y > yMax) point.Y = yMax;

            return point;
        }

        public static float GetRange(this Point point, Point otherPoint)
        {
            Point result = point - otherPoint;
            return (float)Math.Abs(result.X) + Math.Abs(result.Y);
        }
    }
}
