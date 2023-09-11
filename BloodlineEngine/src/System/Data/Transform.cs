namespace BloodlineEngine
{
    public class Transform
    {
        /// <summary>
        /// Transform origin. (center)
        /// </summary>
        public Vector2 Position { get; set; } = new();
        /// <summary>
        /// Distance of parallel edges.
        /// </summary>
        public Vector2 Scale { get; set; } = new();
        /// <summary>
        /// Rotation around origin. (angle)
        /// </summary>
        public float Rotation { get; set; }
        public float Z { get; set; }

        public Transform() { }

        public Transform(Vector2? position = null, Vector2? scale = null, float? rotation = null, float? z = null)
        {
            Position = position ?? new Vector2();
            Scale = scale ?? new Vector2();
            Rotation = rotation ?? 0f;
            Z = z ?? 0f;
        }

        public float X
        {
            get => Position.X;
            set => Position.X = value;
        }
        public float Y
        {
            get => Position.Y;
            set => Position.Y = value;
        }

        public Vector2 Center
        {
            get => Position;
            set => Position = value;
        }

        public Vector2 DefaultTopLeft
        {
            get => Position - Scale / 2f;
            set => Position = value + Scale / 2f;
        }
        public Vector2 DefaultTopRight
        {
            get => (Position.X + Scale.X / 2f, Position.Y - Scale.Y / 2f);
            set => Position = (value.X - Scale.X / 2f, value.Y + Scale.Y / 2f);
        }
        public Vector2 DefaultBottomLeft
        {
            get => (Position.X - Scale.X / 2f, Position.Y + Scale.Y / 2f);
            set => Position = (value.X + Scale.X / 2f, value.Y - Scale.Y / 2f);
        }
        public Vector2 DefaultBottomRight
        {
            get => Position + Scale / 2f;
            set => Position = value - Scale / 2f;
        }

        public Vector2 TopLeft => Vector2.RotateVertex(DefaultTopLeft, Center, Rotation);
        public Vector2 TopRight => Vector2.RotateVertex(DefaultTopRight, Center, Rotation);
        public Vector2 BottomLeft => Vector2.RotateVertex(DefaultBottomLeft, Center, Rotation);
        public Vector2 BottomRight => Vector2.RotateVertex(DefaultBottomRight, Center, Rotation);

        public Vector2[] Vertices => CalculateVertices(this);

        public static Vector2[] CalculateVertices(Transform transform)
        { return new Vector2[4] { transform.TopLeft, transform.TopRight, transform.BottomRight, transform.BottomLeft }; }

        public override string ToString() => $"Transform2D({Position},{Scale},{Rotation},{Z})";

        public static Transform operator +(Transform left, Transform right) { return new Transform(
            left.Position + right.Position,
            left.Scale + right.Scale,
            left.Rotation + right.Rotation,
            left.Z + right.Z
        ); }
    }
}
