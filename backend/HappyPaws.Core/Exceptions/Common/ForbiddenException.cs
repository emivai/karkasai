namespace HappyPaws.Core.Exceptions.Common
{
    public class ForbiddenException : Exception
    {
        public ForbiddenException()
        {
        }

        public ForbiddenException(string text)
           : base(string.Format(text))
        {

        }
    }
}
