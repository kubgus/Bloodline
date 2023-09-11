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

                switch (renderedComponent)
                {
                    case Quad r:
                        IntPtr quadSurface = SDL.SDL_CreateRGBSurface(0, (int)r.Transform.Scale.X, (int)r.Transform.Scale.Y, 32, 0, 0, 0, 0);
                        _ = SDL.SDL_FillRect(quadSurface, IntPtr.Zero, (uint)r.Color);
                        Draw(r, SDL.SDL_CreateTextureFromSurface(SDLRenderer, quadSurface));
                        SDL.SDL_FreeSurface(quadSurface);
                        break;
                    case Sprite r:
                        if (r.Path is null) { Debug.BLWarn("Sprite received no path!"); continue; }
                        Draw(r, SDL_image.IMG_LoadTexture(SDLRenderer, r.Path));
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

        // TODO: Fix camera rotation
        private void Draw(RenderedComponent renderedComponent, IntPtr texture)
        {
            Vector2 cameraScale = 1f + Camera.Scale;
            Vector2 positionWithoutCameraRotation = renderedComponent.Transform.DefaultTopLeft
                * cameraScale
                + Camera.WindowSize / 2f
                - Camera.Position;

            Vector2 position = Vector2.RotateVertex(positionWithoutCameraRotation, Camera.Position + Camera.WindowSize / 2f, Camera.Rotation);
            Vector2 scale = renderedComponent.Transform.Scale
                * cameraScale;
            float rotation = renderedComponent.Transform.Rotation
                + Camera.Rotation;

            SDL.SDL_Rect destRect = new()
            {
                x = (int)position.X,
                y = (int)position.Y,
                w = (int)scale.X,
                h = (int)scale.Y,
            };

            SDL.SDL_Point pivot = new()
            {
                x = (int)scale.X / 2,
                y = (int)scale.Y / 2,
            };

            _ = SDL.SDL_RenderCopyEx(SDLRenderer, texture, IntPtr.Zero, ref destRect,
                rotation, ref pivot,
                renderedComponent.HorizontalFlip && renderedComponent.VerticalFlip ? SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL | SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL :
                renderedComponent.HorizontalFlip && !renderedComponent.VerticalFlip ? SDL.SDL_RendererFlip.SDL_FLIP_HORIZONTAL :
                !renderedComponent.HorizontalFlip && renderedComponent.VerticalFlip ? SDL.SDL_RendererFlip.SDL_FLIP_VERTICAL :
                SDL.SDL_RendererFlip.SDL_FLIP_NONE);
        }
    }
}
