namespace BloodlineEngine
{
    public class Quad : RenderedComponent
    {
        public Color4 Color { get; set; }

        public Quad()
        {
            Color = 0;
        }

        public Quad Col(Color4 color) { Color =  color; return this; }
    }
}
