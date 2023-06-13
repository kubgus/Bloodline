namespace BloodlineEngine
{
    public abstract class CollisionComponent : Component
    {
        public static bool CheckPolygonCollision(Vector2[] shapeA, Vector2[] shapeB)
        {
            if (shapeA == null || shapeB == null || shapeA.Length < 3 || shapeB.Length < 3)
                throw new ArgumentException("Input shapes must have at least three points.");

            Vector2[] axes = GetPolygonAxes(shapeA, shapeB);

            foreach (Vector2 axis in axes)
            {
                if (IsSeparatingAxis(shapeA, shapeB, axis))
                    return false;
            }

            return true;
        }

        private static Vector2[] GetPolygonAxes(Vector2[] shapeA, Vector2[] shapeB)
        {
            List<Vector2> axes = new List<Vector2>();

            AddUniqueAxes(shapeA, axes);
            AddUniqueAxes(shapeB, axes);

            return axes.ToArray();
        }

        private static void AddUniqueAxes(Vector2[] shape, List<Vector2> axes)
        {
            for (int i = 0; i < shape.Length; i++)
            {
                Vector2 edge = shape[(i + 1) % shape.Length] - shape[i];
                Vector2 axis = new Vector2(-edge.Y, edge.X).Normalized;

                if (!axes.Contains(axis))
                    axes.Add(axis);
            }
        }

        private static bool IsSeparatingAxis(Vector2[] shapeA, Vector2[] shapeB, Vector2 axis)
        {
            Projection projectionA = GetProjection(shapeA, axis);
            Projection projectionB = GetProjection(shapeB, axis);

            return !projectionA.Overlaps(projectionB);
        }

        private static Projection GetProjection(Vector2[] shape, Vector2 axis)
        {
            float min = float.MaxValue;
            float max = float.MinValue;

            foreach (Vector2 point in shape)
            {
                float projection = Vector2.Dot(point, axis);
                min = Math.Min(min, projection);
                max = Math.Max(max, projection);
            }

            return new Projection(min, max);
        }

        private struct Projection
        {
            public float Min { get; }
            public float Max { get; }

            public Projection(float min, float max)
            {
                Min = min;
                Max = max;
            }

            public bool Overlaps(Projection other)
            {
                return Max >= other.Min && other.Max >= Min;
            }
        }
    }
}
