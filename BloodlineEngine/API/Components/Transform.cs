namespace BloodlineEngine
{
    public class Transform : Component
    {
        public Vector2 Position { get; set; } = new Vector2();
        public Vector2 Scale { get; set; } = new Vector2();
        public float Rotation { get; set; }

        public float X
        {
            get { return Position.X; }
            set { Position.X = value; }
        }
        public float Y
        {
            get { return Position.Y; }
            set { Position.Y = value; }
        }

        public override string ToString() { return $"Transform2D({Position},{Scale},{Rotation})"; }
    }
}
