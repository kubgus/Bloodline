namespace BloodlineEngine
{
    public class BLRenderer
    {
        public IntPtr SDLRenderer { get; private set; }

        public BLWindow Window { get; private set; }
        public Camera Camera { get; set; } = new();

        public Color4 ClearColor { get; set; } = Color4.White;

        private readonly static List<RenderedComponent> m_ActiveRenderedComponents = new();

        public BLRenderer(BLWindow window)
        {
            Window = window;

            m_ActiveRenderedComponents.Clear();

            SDLRenderer = SDL.SDL_CreateRenderer(Window.SDLWindow, index: -1,
                SDL.SDL_RendererFlags.SDL_RENDERER_ACCELERATED | SDL.SDL_RendererFlags.SDL_RENDERER_PRESENTVSYNC);
            Debug.Assert(SDLRenderer != IntPtr.Zero, "SDL Renderer could not be created!");

            _ = SDL.SDL_SetRenderDrawBlendMode(SDLRenderer, SDL.SDL_BlendMode.SDL_BLENDMODE_BLEND);
        }

        ~BLRenderer()
        {
            SDL.SDL_DestroyRenderer(SDLRenderer);
        }

        public void BLRender()
        {
            Input.BLSetWorldProperties(cameraPosition: Camera.Position);

            _ = SDL.SDL_SetRenderDrawColor(SDLRenderer, (byte)ClearColor.R, (byte)ClearColor.G, (byte)ClearColor.B, (byte)ClearColor.A);
            _ = SDL.SDL_RenderClear(SDLRenderer);

            List<RenderedComponent> queue = GetOrdered();
            foreach (RenderedComponent renderedComponent in queue)
            {
                if (!renderedComponent.IsActive) { continue; }

                Vector2 position = renderedComponent.Transform.Position - Camera.Position;
                Vector2 scale = renderedComponent.Transform.Scale;
                float rotation = renderedComponent.Transform.Rotation;

                SDL.SDL_Rect rect = new()
                {
                    x = (int)position.X,
                    y = (int)position.Y,
                    w = (int)scale.X,
                    h = (int)scale.Y,
                };

                switch (renderedComponent)
                {
                    case Quad r:
                        _ = SDL.SDL_SetRenderDrawColor(SDLRenderer,
                            (byte)r.Color.R, (byte)r.Color.G, (byte)r.Color.B, (byte)r.Color.A);
                        _ = SDL.SDL_RenderFillRect(SDLRenderer, ref rect);
                        break;
                    case Sprite r:
                        if (r.Path is null) { Debug.Warn("Sprite received no path!"); continue; }
                        IntPtr texture = SDL_image.IMG_LoadTexture(SDLRenderer, r.Path);
                        _ = SDL.SDL_RenderCopy(SDLRenderer, texture, IntPtr.Zero, ref rect);
                        break;
                }
            }

            // Swap buffer (must run last)
            SDL.SDL_RenderPresent(SDLRenderer);
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
