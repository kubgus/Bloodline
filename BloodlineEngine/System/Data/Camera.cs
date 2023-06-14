namespace BloodlineEngine
{
    public class Camera
    {
        public Transform Transform { get; set; } = new();

        public void Move(Vector2 position) { Transform.Center += position; }
        public void GoTo(Vector2 position) { Transform.Center = position; }

        // Effects and other properties go here!
    }
}
