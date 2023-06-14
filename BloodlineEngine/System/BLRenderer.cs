using System.Drawing.Drawing2D;

namespace BloodlineEngine
{
    public class BLRenderer
    {
        public Color4 ClearColor { get; set; } = Color.White;
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

            List<RenderedComponent> queue = GetOrdered();

            foreach (RenderedComponent renderedComponent in queue)
            {
                if (!renderedComponent.IsActive) { continue; }

                Vector2 origin = renderedComponent.Transform.Position + renderedComponent.Transform.Scale / 2f;
                g.TranslateTransform(origin.X, origin.Y);
                g.RotateTransform(renderedComponent.Transform.Rotation);
                g.TranslateTransform(-origin.X, -origin.Y);

                switch (renderedComponent)
                {
                    case Quad r:
                        g.FillRectangle(new SolidBrush(r.Color), r.Transform.X, r.Transform.Y,
                            r.Transform.Scale.X, r.Transform.Scale.Y);
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

        private static List<RenderedComponent> GetOrdered()
        {
            return m_ActiveRenderedComponents.OrderBy(renderedComponent => renderedComponent.Transform.Z).ToList();
        }
    }
}
