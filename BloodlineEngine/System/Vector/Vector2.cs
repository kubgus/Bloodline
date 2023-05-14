using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodlineEngine
{
    public class Vector2 : IVector
    {
        public float X { get; set; }
        public float Y { get; set; }

        public override string ToString()
        {
            return "Vector2(" + X + ", " + Y + ")";
        }

        public float Magnituded => Magnitude(this);
        public Vector2 Normalized => Normalize(this);
        public Vector2 Absoluted => Abs(this);

        public static Vector2 Zero()
            { return new Vector2(0, 0); }
        public static float Magnitude(Vector2 value)
            { return (float)Math.Sqrt(value.X * value.X + value.Y * value.Y); }
        public static Vector2 Normalize(Vector2 value) 
            { return value / Magnitude(value); }
        public static Vector2 Abs(Vector2 value)
            { return new Vector2(Math.Abs(value.X), Math.Abs(value.Y)); }
        public static Vector2 Pow(Vector2 value, double power) 
            { return new Vector2((float)Math.Pow(value.X, power), (float)Math.Pow(value.Y, power)); }
        public static float GetDistance(Vector2 value, Vector2 target)
            { return Magnitude(target - value); }
        public static float GetRelativeAngle(Vector2 value, Vector2 target)
            { return (float)Math.Atan2(target.Y - value.Y, target.X - value.X); }
        public static Vector2 MoveInDirection(Vector2 value, Vector2 direction, float distance)
            { return value + Normalize(direction) * distance; }
        public static Vector2 MoveInDirection(Vector2 value, float angle, float distance)
        {
            float x = (float)Math.Cos(angle) * distance;
            float y = (float)Math.Sin(angle) * distance;
            return value + new Vector2(x, y);
        }
        public static Vector2 MoveTowards(Vector2 value, Vector2 target, float maxDistanceDelta)
        {
            Vector2 direction = target - value;
            float magnitude = Magnitude(direction);
            if (magnitude <= maxDistanceDelta || magnitude == 0f) { return target; }
            return value + direction / magnitude * maxDistanceDelta;
        }

        public Vector2 Copy() { return new Vector2(X, Y); }

        public Vector2(float x, float y) {  X = x; Y = y; }
        public Vector2(float xy) { X = xy; Y = xy; }
        public Vector2() { X = Zero().X; Y = Zero().Y; }

        public static Vector2 operator +(Vector2 left, Vector2 right) { return new Vector2(left.X + right.X, left.Y + right.Y); }
        public static Vector2 operator +(Vector2 left, float right) { return left + new Vector2(right); }
        public static Vector2 operator +(float left, Vector2 right) { return new Vector2(left) + right; }

        public static Vector2 operator -(Vector2 left, Vector2 right) { return new Vector2(left.X - right.X, left.Y - right.Y); }
        public static Vector2 operator -(Vector2 left, float right) { return left - new Vector2(right); }
        public static Vector2 operator -(float left, Vector2 right) { return new Vector2(left) - right; }

        public static Vector2 operator *(Vector2 left, Vector2 right) { return new Vector2(left.X * right.X, left.Y * right.Y); }
        public static Vector2 operator *(Vector2 left, float right) { return left * new Vector2(right); }
        public static Vector2 operator *(float left, Vector2 right) { return new Vector2(left) * right; }

        public static Vector2 operator /(Vector2 left, Vector2 right) { return new Vector2(left.X / right.X, left.Y / right.Y); }
        public static Vector2 operator /(Vector2 left, float right) { return left / new Vector2(right); }
        public static Vector2 operator /(float left, Vector2 right) { return new Vector2(left) / right; }
    }
}
