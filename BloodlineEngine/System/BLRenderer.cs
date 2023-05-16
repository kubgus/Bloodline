using System.Drawing.Drawing2D;

namespace BloodlineEngine
{
    public class BLRenderer
    {
        public Color ClearColor { get; set; } = Color.White;
        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.NearestNeighbor;
        public PixelOffsetMode PixelOffsetMode { get; set; } = PixelOffsetMode.Half;

        public BLRenderer() { }

        public void Render(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.InterpolationMode = InterpolationMode;
            g.PixelOffsetMode = PixelOffsetMode;

            g.Clear(ClearColor);
        }
    }
}
