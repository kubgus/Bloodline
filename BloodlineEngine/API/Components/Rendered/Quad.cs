namespace BloodlineEngine
{
    public class Quad : RenderedComponent
    {
        public Color4 Color { get; set; } = 0;

        public Quad Col(Color4 color) { Color =  color; return this; }
    }
}
