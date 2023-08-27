namespace BloodlineEngine
{
    public class LineAndLine
    {
        public static bool Check(Vector2[] lineA, Vector2[] lineB)
        {
            if (lineA == null || lineB == null || lineA.Length != 2 || lineB.Length != 2)
                throw new ArgumentException("Both lines must have 2 points.");

            // Convert lines to polygons and use the existing methods
            Vector2[] polygonA = new Vector2[] { lineA[0], lineA[1], lineA[1] + (lineA[1] - lineA[0]).Normalized };
            Vector2[] polygonB = new Vector2[] { lineB[0], lineB[1], lineB[1] + (lineB[1] - lineB[0]).Normalized };

            bool intersects = PolygonAndPolygon.Check(polygonA, polygonB);
            return intersects;
        }
    }
}