namespace BloodlineEngine
{
    public class PhysicsBody : Component
    {
        public Vector2 Velocity { get; set; } = new();
        public float Gravity { get; set; }
        /// <summary>
        /// The slow-down of an object.
        /// </summary>
        public Vector2 Drag { get; set; } = new();

        public PhysicsBody Vel(Vector2 velocity) { Velocity = velocity; return this; }
        public PhysicsBody Gra(float gravity) { Gravity = gravity; return this; }
        public PhysicsBody Dra(Vector2 drag) { Drag = drag; return this; }
        /// <param name="drag">Horizontal drag.</param>
        public PhysicsBody Dra(float dragX) { Drag.X = dragX; return this; }

        protected Func<bool> BeforePhysics { get; set; } = () => { return true; };
        protected Func<bool> AfterPhysics { get; set; } = () => { return true; };

        protected Func<bool> BeforeHorizontalPhysics { get; set; } = () => { return true; };
        protected Func<bool> AfterHorizontalPhysics { get; set; } = () => { return true; };

        protected Func<bool> BeforeVerticalPhysics { get; set; } = () => { return true; };
        protected Func<bool> AfterVerticalPhysics { get; set; } = () => { return true; };

        public PhysicsBody Bep(Func<bool> beforePhysics) { BeforePhysics = beforePhysics; return this; }
        public PhysicsBody Afp(Func<bool> afterPhysics) { AfterPhysics = afterPhysics; return this; }

        public PhysicsBody Bhp(Func<bool> beforeHorizontalPhysics)
        { BeforeHorizontalPhysics = beforeHorizontalPhysics; return this; }
        public PhysicsBody Ahp(Func<bool> afterHorizontalPhysics)
        { AfterHorizontalPhysics = afterHorizontalPhysics; return this; }

        public PhysicsBody Bvp(Func<bool> beforeVerticalPhysics)
        { BeforeVerticalPhysics = beforeVerticalPhysics; return this; }
        public PhysicsBody Avp(Func<bool> afterVerticalPhysics)
        { AfterVerticalPhysics = afterVerticalPhysics; return this; }

        protected override void BaseFixedUpdate()
        {
            HandleHorizontalPhysics();
            HandleVerticalPhysics();
        }

        private void HandleHorizontalPhysics()
        {
            if (!BeforePhysics() || !BeforeHorizontalPhysics()) return;
            if (Velocity.X > 0) Velocity.X = Velocity.X - Drag.X < 0f ? 0f : Velocity.X - Drag.X;
            else if (Velocity.X < 0) Velocity.X = Velocity.X + Drag.X > 0f ? 0f : Velocity.X + Drag.X;
            float velocity = Velocity.X;
            Transform.Position.X += velocity;
            if (!AfterPhysics() || !AfterHorizontalPhysics()) Transform.Position.X -= velocity;
        }

        private void HandleVerticalPhysics()
        {
            if (!BeforePhysics() || !BeforeVerticalPhysics()) return;
            if (Velocity.Y > 0) Velocity.Y = Velocity.Y - Drag.Y < 0f ? 0f : Velocity.Y - Drag.Y;
            else if (Velocity.Y < 0) Velocity.Y = Velocity.Y + Drag.Y > 0f ? 0f : Velocity.Y + Drag.Y;
            Velocity.Y += Gravity;
            float velocity = Velocity.Y;
            Transform.Position.Y += velocity;
            if (!AfterPhysics() || !AfterVerticalPhysics()) Transform.Position.Y -= velocity;
        }
    }
}
