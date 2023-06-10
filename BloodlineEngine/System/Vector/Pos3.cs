namespace BloodlineEngine
{
    public class Pos3 : Vector2
    {
        public float Z { get; set; }

        public Pos3(float x, float y, float z) : base(x, y)
        {
            Z = z;
        }
    }
}
