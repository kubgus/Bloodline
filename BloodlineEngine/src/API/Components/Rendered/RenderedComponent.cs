namespace BloodlineEngine
{
    public abstract class RenderedComponent : Component
    {
        public bool HorizontalFlip { get; set; } = false;
        public bool VerticalFlip { get; set; } = false;

        public RenderedComponent Hof(bool horizontalFlip) { HorizontalFlip = horizontalFlip; return this; }
        public RenderedComponent Vef(bool verticalFlip) { VerticalFlip = verticalFlip; return this; }

        public RenderedComponent()
        {
            BLRenderer.AddGlobalRenderedComponent(this);
        }

        ~RenderedComponent()
        {
            BLRenderer.RemoveGlobalRenderedComponent(this);
        }
    }
}
