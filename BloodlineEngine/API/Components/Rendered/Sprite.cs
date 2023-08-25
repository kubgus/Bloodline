namespace BloodlineEngine
{
    public class Sprite : RenderedComponent
    {
        public Bitmap Bitmap { get; set; } = new Bitmap(1, 1);

        public Sprite Bmp(Bitmap bitmap) { Bitmap = bitmap; return this; }
    }
}
