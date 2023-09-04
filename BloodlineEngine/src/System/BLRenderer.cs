using SDL2;

namespace BloodlineEngine
{
    public class BLRenderer
    {
        public IntPtr SDLRenderer { get; private set; }

        public BLWindow Window { get; private set; }
        public Camera Camera { get; set; } = new();

        public Color4 ClearColor { get; set; } = Color4.White;

        private static List<RenderedComponent> m_ActiveRenderedComponents = new();

        public BLRenderer(BLWindow window)
        {
            Window = window;

            m_ActiveRenderedComponents.Clear();

            SDLRenderer = SDL.SDL_CreateRenderer(Window.SDLWindow, index: -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED);
            Debug.Assert(SDLRenderer != IntPtr.Zero, "SDL Renderer could not be created!");
        }

        ~BLRenderer()
        {
            SDL.SDL_DestroyRenderer(SDLRenderer);
        }

        public void Render()
        {

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
