namespace BloodlineEngine
{
    public class Raycast
    {
        public Vector2 Start { get; private set; } = 0f;
        public Vector2 End { get; private set; } = 0f;

        public Raycast(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }
    }
}
