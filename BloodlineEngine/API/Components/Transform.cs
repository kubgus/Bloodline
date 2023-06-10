namespace BloodlineEngine
{
    public class Transform : Component
    {
        public Vector2 Position { get; set; } = new Vector2();
        public Vector2 Scale { get; set; } = new Vector2();
        public float Rotation { get; set; }
        public float Z { get; set; }

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
            set => Position = (value.X + Scale.X, value.Y - Scale.Y);
        }

        public override string ToString() { return $"Transform2D({Position},{Scale},{Rotation})"; }
    }
}
