using System.Drawing.Drawing2D;

namespace BloodlineEngine
{
    public class BLRenderer
    {
        public Camera Camera { get; set; } = new();

        public Vector2 ScreenSize { get; private set; } = 0f;
        public Color4 ClearColor { get; set; } = Color.White;
        public InterpolationMode InterpolationMode { get; set; } = InterpolationMode.NearestNeighbor;
        public PixelOffsetMode PixelOffsetMode { get; set; } = PixelOffsetMode.Half;

        private static List<RenderedComponent> m_ActiveRenderedComponents = new();

        public BLRenderer(BLWindow window)
        {
            ScreenSize = (Vector2)window.Size;

            m_ActiveRenderedComponents.Clear();
        }

        public void Render(object? sender, PaintEventArgs e)
        {
            Input.BLSetWorldProperties(cameraPosition: Camera.Position);

            Graphics g = e.Graphics;

            g.InterpolationMode = InterpolationMode;
            g.PixelOffsetMode = PixelOffsetMode;

            g.Clear(ClearColor);

            List<RenderedComponent> queue = GetOrdered();

            foreach (RenderedComponent renderedComponent in queue)
            {
                if (!renderedComponent.IsActive) { continue; }

                Vector2 screenOrigin = ScreenSize / 2f;
                g.TranslateTransform(screenOrigin.X, screenOrigin.Y);
                g.RotateTransform(Camera.Rotation);
                g.ScaleTransform(
                    Camera.Scale.X + 1f == 0f ? 1f / 1000000f : Camera.Scale.X + 1f,
                    Camera.Scale.Y + 1f == 0f ? 1f / 1000000f : Camera.Scale.Y + 1f);
                g.TranslateTransform(-Camera.Position.X - screenOrigin.X, -Camera.Position.Y - screenOrigin.Y);

                Vector2 componentOrigin = renderedComponent.Transform.Position + renderedComponent.Transform.Scale / 2f;
                g.TranslateTransform(componentOrigin.X, componentOrigin.Y);
                g.RotateTransform(renderedComponent.Transform.Rotation);
                g.TranslateTransform(-componentOrigin.X, -componentOrigin.Y);

                switch (renderedComponent)
                {
                    case Quad r:
                        g.FillRectangle(new SolidBrush(r.Color), r.Transform.X, r.Transform.Y, r.Transform.Scale.X, r.Transform.Scale.Y);
                        break;
                    case Sprite r:
                        g.DrawImage(r.Bitmap, r.Transform.Position.X, r.Transform.Position.Y, r.Transform.Scale.X, r.Transform.Scale.Y);
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
