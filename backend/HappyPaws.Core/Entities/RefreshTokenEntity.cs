using HappyPaws.Core.Entities.Common;

namespace HappyPaws.Core.Entities
{
    public class RefreshTokenEntity : Entity
    {
        public Guid UserId { get; set; }
        public string Token { get; set; }
        public DateTime Expires { get; set; }
    }
}
