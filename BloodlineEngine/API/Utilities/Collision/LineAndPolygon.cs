namespace BloodlineEngine
{
    public static class LineAndPolygon
    {
        public static bool Check(Vector2[] line, Vector2[] polygon)
        {
            if (line == null || polygon == null || line.Length != 2 || polygon.Length < 3)
                throw new ArgumentException("The line must have 2 points and the polygon must have at least 3 points.");

            // Convert lines to polygons and use the existing methods
            Vector2[] polygonA = polygon;
            Vector2[] polygonB = new Vector2[] { line[0], line[1], line[1] + (line[1] - line[0]).Normalized };

            bool intersects = PolygonAndPolygon.Check(polygonA, polygonB);
            return intersects;
        }
    }
}