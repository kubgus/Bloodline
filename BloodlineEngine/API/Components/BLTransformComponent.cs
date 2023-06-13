namespace BloodlineEngine
{
    public class BLTransformComponent : Component
    {
        public new Transform Transform;

        public BLTransformComponent()
        {
            Transform = new Transform();
        }

        public BLTransformComponent(Transform transform)
        {
            Transform = transform;
        }

        public static implicit operator BLTransformComponent(Transform value) { return new(value); }
        public static implicit operator Transform(BLTransformComponent value) { return value.Transform; }
    }
}
