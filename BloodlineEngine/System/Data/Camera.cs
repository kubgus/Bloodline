// TODO: Set Transform.Scale property based on ScreenSize.

namespace BloodlineEngine
{
    public class Camera
    {
        public Transform Transform { get; set; } = new();

        public void MoveTo(Vector2 position)
        { Transform.Center = position; }
        public void Move(Vector2 direction)
        { Transform.Position += direction; }

        // Effects and other properties go here!
    }
}
