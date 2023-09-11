namespace BloodlineEngine
{
    public class BoxCollider : CollisionComponent
    {
        public override string Tag { get; set; } = "BoxCollider";

        public Transform RelativeTransform { get; set; } = new();

        public BoxCollider Rel(Transform relativeTransform) { RelativeTransform = relativeTransform; return this; }

        public Transform ModifiedTransform => Transform + RelativeTransform;

        protected override bool IsCollidingAbstraction(CollisionComponent other)
        {
            // Maybe change to switch statment later, for now it looks cleaner this way.
            if (other is BoxCollider boxCollider) { return IsColliding(boxCollider); }
            return false;
        }

        private bool IsColliding(BoxCollider other)
        { return PolygonAndPolygon.Check(ModifiedTransform.Vertices, other.ModifiedTransform.Vertices); }
    }
}
