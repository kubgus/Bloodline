namespace BloodlineEngine
{
    public abstract class CollisionComponent : Component
    {
        public abstract string Tag { get; set; }

        /// <summary>
        /// Set collider tag.
        /// </summary>
        public CollisionComponent Pin(string tag) { Tag = tag; return this; }

        public bool IsColliding(CollisionComponent other)
        {
            if (!IsActive || !other.IsActive) return false;
            return IsCollidingAbstraction(other);
        }
        public bool IsColliding(string tag)
        {
            foreach (CollisionComponent collider in BLGeneralComponentHandler.Get().OfType<CollisionComponent>())
            {
                if (collider.Tag != tag) { continue; }
                if (IsColliding(collider)) return true;
            }
            return false;
        }

        protected abstract bool IsCollidingAbstraction(CollisionComponent other);
    }
}
