namespace BloodlineEngine
{
    public class Quad : RenderedComponent
    {
        public Color4 Color { get; set; }

        public Vector2[] Vertices => Transform.Vertices;

        public Quad()
        {
            Color = 0;
        }
    }
}
