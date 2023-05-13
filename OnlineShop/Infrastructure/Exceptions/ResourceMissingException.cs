namespace OnlineShop.Infrastructure.Exceptions
{
    public class ResourceMissingException : Exception
    {
        public ResourceMissingException(string message) : base(message) { }
    }
}
