using HappyPaws.Core.Exceptions.Common;
using Microsoft.AspNetCore.Mvc;

namespace HappyPaws.API.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected Guid GetUserId()
        {
            var userId = User.FindFirst("UserId")?.Value;

            if (userId == null)
            {
                throw new ForbiddenException();
            }

            return new Guid(userId);
        }
    }
}
