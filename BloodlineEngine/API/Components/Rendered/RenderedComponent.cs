namespace BloodlineEngine
{
    public abstract class RenderedComponent : Component
    {
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
