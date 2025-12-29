using Core.Notifications.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Equinox.Services.Api.Controllers
{
    [ApiController]
    public abstract class BaseController : ControllerBase
    {
        private readonly IDomainNotifier _notifier;
        
        public BaseController(IDomainNotifier notifier)
        {
            _notifier = notifier;
        }

        protected ActionResult CustomResponse(object? result = null)
        {
            if (HasNotification())
            {
                return Ok(result);
            }

            return BadRequest(new ValidationProblemDetails(new Dictionary<string, string[]>
            {
                { "messages", _notifier.GetNotifications().Select(n => n.Message).ToArray() }
            }));

            //return BadRequest(new 
            //{
            //    errors = _notifier.GetNotifications().Select(n => new { property = n.Key, n.Message })
            //});
        }

        protected bool HasNotification()
        {
            return !_notifier.HasNotification();
        }
    }
}
