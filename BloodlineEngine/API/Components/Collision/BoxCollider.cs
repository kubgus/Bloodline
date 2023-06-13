namespace BloodlineEngine
{
    public class BoxCollider : CollisionComponent
    {
        public string Tag { get; set; } = "BoxCollider";
        public Transform ModifiedTransform => new Transform(
                Transform.Position + RelativeTransform.Position,
                Transform.Scale + RelativeTransform.Scale,
                Transform.Rotation + RelativeTransform.Rotation,
                Transform.Z + RelativeTransform.Z);
        public Transform RelativeTransform { get; set; } = new();

        public bool IsColliding(string tag)
        {
            foreach (BoxCollider boxCollider in BLGeneralComponentHandler.Get().OfType<BoxCollider>())
            {
                if (boxCollider.Tag != tag) { continue; }
                return IsColliding(boxCollider);
            }
            return false;
        }

        public bool IsColliding(BoxCollider other)
        {
            Vector2[] thisVertices = ModifiedTransform.Vertices;
            Vector2[] otherVertices = other.ModifiedTransform.Vertices;

            return CheckPolygonCollision(thisVertices, otherVertices);
        }
    }
}
