namespace HappyPaws.Core.Exceptions.Common
{
    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message) { }
    }
}
