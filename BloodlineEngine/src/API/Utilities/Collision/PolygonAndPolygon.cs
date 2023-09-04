namespace BloodlineEngine
{
    public static class PolygonAndPolygon
    {
        public static bool Check(Vector2[] polygonA, Vector2[] polygonB)
        {
            if (polygonA == null || polygonB == null || polygonA.Length < 3 || polygonB.Length < 3)
                throw new ArgumentException("Both polygons must have at least 3 points.");

            List<Vector2> list = new();

            AddUniqueAxes(polygonA, list);
            AddUniqueAxes(polygonB, list);

            Vector2[] axes = list.ToArray();

            foreach (Vector2 axis in axes)
            {
                Projection projectionA = GetProjection(polygonA, axis);
                Projection projectionB = GetProjection(polygonB, axis);

                if (!projectionA.Overlaps(projectionB))
                    return false;
            }

            return true;
        }

        private static void AddUniqueAxes(Vector2[] polygon, List<Vector2> axes)
        {
            for (int i = 0; i < polygon.Length; i++)
            {
                Vector2 edge = polygon[(i + 1) % polygon.Length] - polygon[i];
                Vector2 axis = new Vector2(-edge.Y, edge.X).Normalized;

                if (!axes.Contains(axis))
                    axes.Add(axis);
            }
        }

        private static Projection GetProjection(Vector2[] polygon, Vector2 axis)
        {
            float min = float.MaxValue;
            float max = float.MinValue;

            foreach (Vector2 point in polygon)
            {
                float projection = Vector2.Dot(point, axis);
                min = Math.Min(min, projection);
                max = Math.Max(max, projection);
            }

            return new Projection(min, max);
        }

        private readonly struct Projection
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
