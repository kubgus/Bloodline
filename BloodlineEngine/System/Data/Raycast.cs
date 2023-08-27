namespace BloodlineEngine
{
    public class Raycast
    {
        public Vector2 Start { get; private set; }
        public Vector2 End { get; private set; }

        public Raycast(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }

        public Vector2[] Vertices => CalculateVertices(this);

        public static Vector2[] CalculateVertices(Raycast raycast)
        { return new Vector2[2] { raycast.Start, raycast.End }; }

        public bool IsColliding(Raycast other) { return LineAndLine.Check(Vertices, other.Vertices); }

        public bool IsColliding(Vector2[] polygon) { return LineAndPolygon.Check(Vertices, polygon); }
    }
}
