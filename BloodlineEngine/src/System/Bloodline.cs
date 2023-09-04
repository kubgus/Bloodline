namespace BloodlineEngine
{
    public static class Bloodline
    {
        /// <summary>
        /// Start a new Bloodline application.
        /// </summary>
        /// <typeparam name="T">Your class that inherits from BLApplication.</typeparam>
        public static T StartApplication<T>() where T : BLApplication, new() { return new T(); }
    }
}
