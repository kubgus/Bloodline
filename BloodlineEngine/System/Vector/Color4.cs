namespace BloodlineEngine
{
    public class Color4 : IVector
    {
        public int R { get => R; set => Math.Clamp(value, 0, 255); }
        public int G { get => G; set => Math.Clamp(value, 0, 255); }
        public int B { get => B; set => Math.Clamp(value, 0, 255); }
        public int A { get => A; set => Math.Clamp(value, 0, 255); }

        public Color4(int r, int g, int b, int a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
        public Color4(int rgb, int a)
        {
            R = rgb;
            G = rgb;
            B = rgb;
            A = a;
        }
        public Color4()
        {
            R = Zero().R;
            G = Zero().G;
            B = Zero().B;
            A = Zero().A;
        }

        public Color4 Copy() { return new Color4(R, G, B, A); }
        
        public static Color4 Zero() { return new Color4(0, 0, 0, 0); }

        public override string ToString() { return $"Color4({R},{G},{B},{A})"; }

        public static implicit operator Color4(int value)
        { return new Color4(value, 1); }
        public static implicit operator Color4((int, int, int) value)
        { return new Color4(value.Item1, value.Item2, value.Item3, 1); }
        public static implicit operator Color4((int, int, int, int) value)
        { return new Color4(value.Item1, value.Item2, value.Item3, value.Item4); }
        public static implicit operator (int, int, int)(Color4 value)
        { return (value.R, value.G, value.B); }
        public static implicit operator (int, int, int, int)(Color4 value)
        { return (value.R, value.G, value.B, value.A); }
        public static implicit operator string(Color4 value)
        { return value.ToString(); }

        public static implicit operator Color(Color4 value)
        { return Color.FromArgb(value.A, value.R, value.G, value.B); }
        public static implicit operator Color4(Color value)
        { return new Color4(value.R, value.G, value.B, value.A); }
    }
}
