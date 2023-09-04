using SDL2;

namespace BloodlineEngine
{
    public class Color4 : IVector
    {
        public int R { get => r; set => r = Math.Clamp(value, 0, 255); }
        public int G { get => g; set => g = Math.Clamp(value, 0, 255); }
        public int B { get => b; set => b = Math.Clamp(value, 0, 255); }
        public int A { get => a; set => a = Math.Clamp(value, 0, 255); }

        private int r;
        private int g;
        private int b;
        private int a;

        public static Color4 Red { get; private set; } = new Color4(255, 0, 0, 255);
        public static Color4 Green { get; private set; } = new Color4(0, 255, 0, 255);
        public static Color4 Blue { get; private set; } = new Color4(0, 0, 255, 255);
        public static Color4 White { get; private set; } = new Color4(255, 255, 255, 255);
        public static Color4 Magenta { get; private set; } = new Color4(255, 0, 255, 255);

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
        public Color4(string hex)
        {
            Color4 color = FromHex(hex);
            R = color.R;
            G = color.G;
            B = color.B;
            A = color.A;
        }
        public Color4()
        {
            Color4 zero = Zero();
            R = zero.R;
            G = zero.G;
            B = zero.B;
            A = zero.A;
        }

        public Color4 Copy() { return new Color4(R, G, B, A); }

        public static Color4 Zero() { return new Color4(0, 0, 0, 0); }
        /// <param name="hex">Hexadecimal value without "#". (i.e.: "000000")</param>
        public static Color4 FromHex(string hex)
        {
            try
            {
                if (hex.StartsWith("#")) hex = hex.Substring(1);
                if (hex.Length != 6 || hex.Length != 8) throw new ArgumentException("Hex string must be 6 or 8 charachers long! (RGB[A])");
                return new SDL.SDL_Color()
                {
                    r = Convert.ToByte(hex[..2], 16),
                    g = Convert.ToByte(hex.Substring(2, 2), 16),
                    b = Convert.ToByte(hex.Substring(4, 2), 16),
                    a = hex.Length == 8 ? Convert.ToByte(hex.Substring(6, 2), 16) : Convert.ToByte("FF", 16)
                };
            }
            catch { Debug.BLWarn("Hex color parameter incorrect! Defaulting to magenta."); return Magenta; }
        }

        public override string ToString() { return $"Color4({R},{G},{B},{A})"; }

        public static implicit operator Color4(int value)
        { return new Color4(value, 255); }
        public static implicit operator Color4((int, int, int) value)
        { return new Color4(value.Item1, value.Item2, value.Item3, 255); }
        public static implicit operator Color4((int, int, int, int) value)
        { return new Color4(value.Item1, value.Item2, value.Item3, value.Item4); }
        public static implicit operator (int, int, int)(Color4 value)
        { return (value.R, value.G, value.B); }
        public static implicit operator (int, int, int, int)(Color4 value)
        { return (value.R, value.G, value.B, value.A); }
        public static implicit operator string(Color4 value)
        { return value.ToString(); }

        public static implicit operator SDL.SDL_Color(Color4 value)
        {
            return new SDL.SDL_Color()
            {
                r = (byte)value.R,
                g = (byte)value.G,
                b = (byte)value.B,
                a = (byte)value.A
            };
        }
        public static implicit operator Color4(SDL.SDL_Color value)
        {
            return new Color4()
            {
                R = value.r,
                G = value.g,
                B = value.b,
                A = value.a
            };
        }
        public static implicit operator Color4(string hex)
        { return FromHex(hex); }
    }
}
