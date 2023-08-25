namespace BloodlineEngine
{
    public class BLWindow : Form
    {
        public bool Opened { get; private set; }
        public bool Active { get; private set; }
        public bool Hovered { get; private set; }

        public BLRenderer Renderer { get; private set; }

        public BLWindow(Vector2 size, string title, bool resizable = false)
        {
            DoubleBuffered = true;

            Size = (Size)size;
            Text = title;
            if (!resizable) { FormBorderStyle = FormBorderStyle.FixedToolWindow; }

            BackColor = Color.Magenta;
            try { Icon = new Icon(File.OpenRead(".BLAssets/BloodlineLogo512.ico")); }
            catch { Debug.BLWarn("Could not load render window icon!"); }

            Opened = true;
            Active = true;

            Renderer = new(this);

            Paint += Renderer.Render;

            KeyPreview = true;

            CreateEventHandlers();
        }

        private void CreateEventHandlers()
        {
            FormClosing += Close;

            GotFocus += Focus;
            LostFocus += Blur;

            MouseEnter += Hover;
            MouseLeave += Unhover;

            PreviewKeyDown += Input.Press;
            KeyUp += Input.Release;

            MouseDown += Input.Press;
            MouseUp += Input.Release;
            MouseMove += Input.BLModifyMousePosition;
        }

        private void Close(object? sender, FormClosingEventArgs e) { Opened = false; }

        private void Focus(object? sender, EventArgs e) { Active = true; }
        private void Blur(object? sender, EventArgs e) { Active = false; }

        private void Hover(object? sender, EventArgs e) { Hovered = true; }
        private void Unhover(object? sender, EventArgs e) { Hovered = false; }
    }
}
