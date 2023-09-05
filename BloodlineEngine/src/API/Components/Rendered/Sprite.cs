namespace BloodlineEngine
{
    public class Sprite : RenderedComponent
    {
        public string? Path;

        public Sprite Src(string path) { Path = path; return this; }
    }
}
