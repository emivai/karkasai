using HappyPaws.Core.Enums;

namespace HappyPaws.Core.Exceptions.Common
{
    public class UserTypeException : Exception
    {
        public UserTypeException() { }

        public UserTypeException(UserType type) 
            : base(String.Format($"User has to be of type {type}.")) { }
    }
}
