using Core.Notifications;
using Core.Notifications.Interfaces;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
        }

        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            var errors = modelState.Values.SelectMany(e => e.Errors);
            foreach (var error in errors)
            {
                NotifyError(error.ErrorMessage);
            }

            return CustomResponse();
        }

        protected ActionResult CustomResponse(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                NotifyError(error.ErrorMessage);
            }

            return CustomResponse();
        }
        

        protected bool HasNotification()
        {
            return !_notifier.HasNotification();
        }        

        protected void NotifyError(string message, string? key = null)
        {
            _notifier.Add(new DomainNotification(message, key));
        }
    }
}
