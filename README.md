<img width="128" align="right" src="https://github.com/kubgus/Bloodline/blob/master/BloodlineEngine/.assets/BloodlineLogo1080.png"></img>
# Bloodline

A 2D game framework/engine for simple games and projects.

## What is Bloodline? 🖼️
I created Bloodline as my "final" C# project, before fully moving to C++. It's meant as a proof of all I've learned in C#. Bloodline can be used by anyone.

## How to use it? 🧑‍💻
Download or compile a DLL of Bloodline, include it in your project and you're done!

1. Create your game class:
```cs
using BloodlineEngine;

namespace MyGame {

  public class Game : BLApplication
  {
    public Game() : base(512f, "My First Game!", lauchWithConsoleShown: true) { }
  }

}
```
2. Modify your `Program.cs`:
```cs
using BloodlineEngine;

namespace MyGame {

  class Program
  {
    static void Main()
    {
      Bloodline.StartApplication<Game>();
    }
  }

}
```
3. Start adding content:
```cs
using BloodlineEngine;

namespace MyGame {

  public class Player : Root
  {
    public override void Init()
    {
      CreateComponent<Quad>()
        .Col("#ff0000")
        .Scl(20f);
        CreateComponent<PlayerMovement>();
    }
  }

  public class PlayerMovement : Component
  {
    readonly float Speed = 5f;

    public override void Update()
    {
      if (Input.IsKeyPressed(Keyboard.W)) { Transform.Position.Y -= Speed; }
      if (Input.IsKeyPressed(Keyboard.S)) { Transform.Position.Y += Speed; }
      if (Input.IsKeyPressed(Keyboard.A)) { Transform.Position.X -= Speed; }
      if (Input.IsKeyPressed(Keyboard.D)) { Transform.Position.X += Speed; }
    }
  }

}
```
4. Add core game functionality:
```cs
using BloodlineEngine;

namespace MyGame {

  public class Game : BLApplication
  {
    // Rest of the class...

    public override void Ready()
    {
      Renderer.ClearColor = (0,0,0,0);

      Instantiate<Player>();
    }
  }

}
```

## Cross-platform ⭐
This service is cross-platform. It should work on Windows, Mac, Linux, Android and iOS. Not thoroughly tested yet.
