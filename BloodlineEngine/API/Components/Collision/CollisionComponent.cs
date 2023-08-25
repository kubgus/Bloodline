namespace BloodlineEngine
{
    public abstract class CollisionComponent : Component
    {
        public abstract string Tag { get; set; }

        /// <summary>
        /// Set collider tag.
        /// </summary>
        public CollisionComponent Pin(string tag) { Tag = tag; return this; }

        public abstract bool IsColliding(CollisionComponent other);
        public bool IsColliding(string tag)
        {
            foreach (CollisionComponent collider in BLGeneralComponentHandler.Get().OfType<CollisionComponent>())
            {
                if (collider.Tag != tag) { continue; }
                return IsColliding(collider);
            }
            return false;
        }
    }
}
