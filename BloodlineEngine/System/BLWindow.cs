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
            Icon = new Icon(File.OpenRead("Content/BloodlineLogo512.ico"));

            Opened = true;
            Active = true;

            Renderer = new BLRenderer();
            Paint += Renderer.Render;

            CreateEventHandlers();
        }

        private void CreateEventHandlers()
        {
            FormClosing += Close;

            GotFocus += Focus;
            LostFocus += Blur;

            MouseEnter += Hover;
            MouseLeave += Unhover;

            KeyDown += Input.Press;
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
