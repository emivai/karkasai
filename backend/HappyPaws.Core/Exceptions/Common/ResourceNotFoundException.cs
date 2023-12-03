namespace HappyPaws.Core.Exceptions.Common
{
    public class ResourceNotFoundException : Exception
    {
        public ResourceNotFoundException() : base(String.Format($"The requested resource could not be found.")){}
    }
}
