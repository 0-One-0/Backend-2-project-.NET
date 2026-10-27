namespace Inlämning_Backend.Exceptions
{
    public class DuplicateSlugException : Exception
    {
        public DuplicateSlugException(string message) : base(message)
        {
        }
    }
}
