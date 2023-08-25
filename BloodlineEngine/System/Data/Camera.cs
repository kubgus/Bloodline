// TODO: Set Transform.Scale property based on ScreenSize.

namespace BloodlineEngine
{
    public class Camera
    {
        public Vector2 Position { get => m_Transform.Position; set => m_Transform.Position = value; }
        public Vector2 Scale { get => m_Transform.Scale; set => m_Transform.Scale = value; }
        public float Rotation { get => m_Transform.Rotation; set => m_Transform.Rotation = value; }
        
        public Vector2 Center
        { 
            get => m_Transform.Position + m_ScreenSize / 2f;
            set => m_Transform.Position = value - m_ScreenSize / 2f;
        }

        private Transform m_Transform { get; set; } = new();
        private Vector2 m_ScreenSize;

        public Camera()
        {
            m_ScreenSize = 512f;
        }

        public void Move(Vector2 direction)
        { Position += direction; }
        public void MoveRotated(Vector2 direction)
        { Center = Vector2.MoveInDirection(Center, direction.Direction - Rotation, direction.Magnitude); }

        // Effects and other properties go here!
    }
}
