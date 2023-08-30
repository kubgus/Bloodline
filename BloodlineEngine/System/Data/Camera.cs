namespace BloodlineEngine
{
    public class Camera
    {
        public Vector2 Position { get => m_Transform.Position; set => m_Transform.Position = value; }
        public Vector2 Scale { get => m_Transform.Scale; set => m_Transform.Scale = value; }
        public float Rotation { get => m_Transform.Rotation; set => m_Transform.Rotation = value; }
        public Vector2 WindowSize { get; set; }
        
        public Vector2 Center
        { 
            get => m_Transform.Position + WindowSize / 2f;
            set => m_Transform.Position = value - WindowSize / 2f;
        }

        private Transform m_Transform { get; set; } = new();

        public Camera()
        {
            WindowSize = 512f;
        }

        public void Move(Vector2 direction)
        { Position += direction; }
        public void MoveRotated(Vector2 direction)
        { Center = Vector2.MoveInDirection(Center, direction.Direction - Rotation, direction.Magnitude); }
    }
}
