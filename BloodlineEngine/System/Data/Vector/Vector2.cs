namespace BloodlineEngine
{
    public class Vector2 : IVector
    {
        public float X { get; set; }
        public float Y { get; set; }

        public Vector2(float x, float y) { X = x; Y = y; }
        public Vector2(float xy) { X = xy; Y = xy; }
        public Vector2() { X = Zero().X; Y = Zero().Y; }

        public Vector2 Copy() { return new Vector2(X, Y); }

        public float Magnitude => Mag(this);
        public Vector2 Normalized => Normalize(this);
        public float Direction => Dir(this);
        public Vector2 Absolute => Abs(this);

        public static Vector2 Zero()
        { return new Vector2(0, 0); }
        public static float Mag(Vector2 value)
        { return (float)Math.Sqrt(value.X * value.X + value.Y * value.Y); }
        public static Vector2 Normalize(Vector2 value)
        { return value / Mag(value); }
        public static float GetDistance(Vector2 value, Vector2 target)
        { return Mag(target - value); }
        public static float Dir(Vector2 value)
        { return (float)Math.Atan2(value.Y, value.X) * Numf.RadToDeg; }
        public static float RelativeAngle(Vector2 value, Vector2 target)
        { return Dir(target - value); }
        public static Vector2 Abs(Vector2 value)
        { return new Vector2(Math.Abs(value.X), Math.Abs(value.Y)); }
        public static Vector2 Pow(Vector2 value, double power)
        { return new Vector2((float)Math.Pow(value.X, power), (float)Math.Pow(value.Y, power)); }
        public static float Dot(Vector2 left, Vector2 right)
        { return left.X * right.X  + left.Y * right.Y; }
        public static bool XorY(Vector2 left, Vector2 right)
        { return left.X == right.X || left.Y == right.Y; }
        // TODO: Might be broken:
        public static Vector2 MoveInDirection(Vector2 value, Vector2 direction, float distance)
        { return value + Normalize(direction) * distance; }
        public static Vector2 MoveInDirection(Vector2 value, float angle, float distance)
        {
            float radians = angle * Numf.DegToRad;
            float x = (float)Math.Cos(radians) * distance;
            float y = (float)Math.Sin(radians) * distance;
            return value + new Vector2(x, y);
        }
        public static Vector2 MoveTowards(Vector2 value, Vector2 target, float maxDistanceDelta)
        {
            Vector2 direction = target - value;
            float magnitude = Mag(direction);
            if (magnitude <= maxDistanceDelta || magnitude == 0f) { return target; }
            return value + direction / magnitude * maxDistanceDelta;
        }
        public static Vector2 RotateVertex(Vector2 vertex, Vector2 center, float angle)
        {
            float rotation = angle * Numf.DegToRad;
            float cos = MathF.Cos(rotation);
            float sin = MathF.Sin(rotation);
            float translatedX = vertex.X - center.X;
            float translatedY = vertex.Y - center.Y;
            float rotatedX = translatedX * cos - translatedY * sin;
            float rotatedY = translatedX * sin + translatedY * cos;
            return new Vector2(rotatedX + center.X, rotatedY + center.Y);
        }

        public override string ToString() { return "Vector2(" + X + "," + Y + ")"; }

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

        public static Vector2 operator %(Vector2 left, Vector2 right) { return new Vector2(left.X % right.X, left.Y % right.Y); }
        public static Vector2 operator %(Vector2 left, float right) { return left % new Vector2(right); }
        public static Vector2 operator %(float left, Vector2 right) { return new Vector2(left) % right; }

        public static implicit operator Vector2(float value) { return new Vector2(value); }
        public static implicit operator Vector2((float, float) value) { return new Vector2(value.Item1, value.Item2); }
        public static implicit operator (float, float)(Vector2 value) { return (value.X, value.Y); }
        public static implicit operator string(Vector2 value) { return value.ToString(); }

        public static explicit operator Size(Vector2 value) { return new Size((int)value.X, (int)value.Y); }
        public static explicit operator Vector2(Size value) { return new Vector2(value.Width, value.Height); }
        public static explicit operator Point(Vector2 value) { return new Point((int)value.X, (int)value.Y); }
        public static explicit operator Vector2(Point value) { return new Vector2(value.X, value.Y); }
    }
}
