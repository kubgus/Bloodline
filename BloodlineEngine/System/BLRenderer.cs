using System.Drawing.Drawing2D;

namespace BloodlineEngine
{
    public class BLRenderer
    {
        public Color ClearColor { get; set; } = Color.White;
        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.NearestNeighbor;
        public PixelOffsetMode PixelOffsetMode { get; set; } = PixelOffsetMode.Half;

        private static List<RenderedComponent> m_ActiveRenderedComponents = new();

        public BLRenderer()
        {
            m_ActiveRenderedComponents.Clear();
        }

        public void Render(object? sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            g.InterpolationMode = InterpolationMode;
            g.PixelOffsetMode = PixelOffsetMode;

            g.Clear(ClearColor);

            List<RenderedComponent> queue = Get();

            foreach (RenderedComponent renderedComponent in queue)
            {
                switch (renderedComponent)
                {
                    case Quad r:
                        g.FillRectangle(new SolidBrush(r.Color), r.Root.Transform.X, r.Root.Transform.Y,
                            r.Root.Transform.Scale.X, r.Root.Transform.Scale.Y);
                        break;
                }

                g.ResetTransform();
            }
        }

        public static void AddGlobalRenderedComponent(RenderedComponent component)
        {
            m_ActiveRenderedComponents.Add(component);
        }

        public static void RemoveGlobalRenderedComponent(RenderedComponent component)
        {
            m_ActiveRenderedComponents.Remove(component);
        }

        private static List<RenderedComponent> Get()
        {
            return m_ActiveRenderedComponents.ToList();
        }
    }
}
