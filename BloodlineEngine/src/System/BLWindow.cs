namespace BloodlineEngine
{
    public class BLWindow
    {
        public IntPtr SDLWindow { get; private set; }

        public BLRenderer Renderer { get; private set; }

        public bool Opened { get; private set; }
        public bool Active { get; private set; }

        public BLWindow(Vector2 size, string title, bool resizable = false)
        {
            Opened = true;
            Active = true;

            Input.BLSetWorldProperties(windowSize: size);

            SDL.SDL_SetHint(SDL.SDL_HINT_WINDOWS_DISABLE_THREAD_NAMING, "1");
            Debug.Assert(SDL.SDL_Init(SDL.SDL_INIT_VIDEO) == 0, "SDL could not be initialized!");

            SDLWindow = SDL.SDL_CreateWindow(title,
                SDL.SDL_WINDOWPOS_CENTERED, SDL.SDL_WINDOWPOS_CENTERED,
                (int)size.X, (int)size.Y,
                (resizable ? SDL.SDL_WindowFlags.SDL_WINDOW_RESIZABLE : SDL.SDL_WindowFlags.SDL_WINDOW_SHOWN) |
                SDL.SDL_WindowFlags.SDL_WINDOW_VULKAN);
            Debug.Assert(SDLWindow != IntPtr.Zero, "SDL Window could not be created!");

            Renderer = new(this);

            Debug.Info($"Initialized a window with SDL!");
        }

        ~BLWindow()
        {
            SDL.SDL_DestroyWindow(SDLWindow);
            SDL.SDL_Quit();
        }

        public void BLStartHandlingEvents()
        {
            while (Opened)
            {
                while (SDL.SDL_PollEvent(out SDL.SDL_Event e) == 1)
                {
                    switch (e.type)
                    {
                        case SDL.SDL_EventType.SDL_QUIT: Close(); break;
                        case SDL.SDL_EventType.SDL_KEYDOWN: Input.Press(e.key.keysym.sym); break;
                        case SDL.SDL_EventType.SDL_KEYUP: Input.Release(e.key.keysym.sym); break;
                        case SDL.SDL_EventType.SDL_MOUSEBUTTONDOWN: Input.Press(e.button.button); break;
                        case SDL.SDL_EventType.SDL_MOUSEBUTTONUP: Input.Release(e.button.button); break;
                        case SDL.SDL_EventType.SDL_MOUSEMOTION: Input.BLModifyMousePosition((e.motion.x, e.motion.y)); break;
                        case SDL.SDL_EventType.SDL_WINDOWEVENT: // Window events
                            switch (e.window.windowEvent) // ^^
                            {
                                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_CLOSE: Close(); break;
                                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_GAINED: Focus(); break;
                                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_FOCUS_LOST: Blur(); break;
                                case SDL.SDL_WindowEventID.SDL_WINDOWEVENT_RESIZED: Scaled((e.window.data1, e.window.data2)); break;
                            }
                            break;
                    }
                }
            }
        }

        private void Close()
        {
            Opened = false;
            SDL.SDL_DestroyWindow(SDLWindow);
            SDL.SDL_DestroyRenderer(Renderer.SDLRenderer);
            SDL.SDL_Quit();
        }

        private void Focus() { Active = true; }
        private void Blur() { Active = false; }

        private void Scaled(Vector2 windowSize) {
            Renderer.Camera.WindowSize = windowSize;
            Input.BLSetWorldProperties(windowSize: windowSize);
        }
    }
}
