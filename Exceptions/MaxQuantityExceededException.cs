namespace Inlämning_Backend.Exceptions
{
    public class MaxQuantityExceededException : Exception
    {
        public MaxQuantityExceededException(string message) : base(message)
        {
        }
    }
}
