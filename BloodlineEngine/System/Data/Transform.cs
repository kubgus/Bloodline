namespace BloodlineEngine
{
    public class Transform
    {
        public Vector2 Position { get; set; } = new Vector2();
        public Vector2 Scale { get; set; } = new Vector2();
        public float Rotation { get; set; }
        public float Z { get; set; }

        public Transform() { }

        public Transform(Vector2? position, Vector2? scale, float? rotation, float? z)
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

        public Vector2 TopLeft
        {
            get => Position;
            set => Position = value;
        }
        public Vector2 TopRight
        {
            get => (Position.X + Scale.X, Position.Y);
            set => Position = (value.X + Scale.X, value.Y);
        }
        public Vector2 BottomLeft
        {
            get => (Position.X, Position.Y - Scale.Y);
            set { Position = (value.X, value.Y - Scale.Y); }
        }
        public Vector2 BottomRight
        {
            get => (Position.X + Scale.X, Position.Y - Scale.Y);
            set => Position = value - Scale;
        }
        public Vector2 Center
        {
            get => Position + Scale / 2;
            set => Position = value - Scale / 2;
        }

        public Vector2[] Vertices => CalculateVertices(this);

        public static Vector2[] CalculateVertices(Transform transform)
        {
            Vector2[] vertices = new Vector2[4]
            {
                transform.Position,
                transform.Position + (transform.Scale.X, 0f),
                transform.Position + transform.Scale,
                transform.Position + (0f, transform.Scale.Y),
            };

            for (int i = 0; i < vertices.Length; i++)
            {
                vertices[i] = Vector2.RotateVertex(vertices[i], transform.Center, transform.Rotation);
            }

            return vertices;
        }

        public override string ToString() { return $"Transform2D({Position},{Scale},{Rotation})"; }
    }
}
