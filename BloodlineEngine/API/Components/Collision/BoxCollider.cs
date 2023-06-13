namespace BloodlineEngine
{
    public class BoxCollider : CollisionComponent
    {
        public override string Tag { get; set; } = "BoxCollider";

        public Transform RelativeTransform { get; set; } = new();

        public Transform ModifiedTransform => new Transform(
                Transform.Position + RelativeTransform.Position,
                Transform.Scale + RelativeTransform.Scale,
                Transform.Rotation + RelativeTransform.Rotation,
                Transform.Z + RelativeTransform.Z);

        public override bool IsColliding(CollisionComponent other)
        {
            // TODO: Maybe change to switch statment later, for now it looks cleaner this way.
            if (other is BoxCollider boxCollider) { return IsColliding(boxCollider); }
            return false;
        }

        private bool IsColliding(BoxCollider other)
        { return CheckPolygonCollision(ModifiedTransform.Vertices, other.ModifiedTransform.Vertices); }
    }
}
