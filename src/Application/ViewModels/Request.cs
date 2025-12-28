using FluentValidation.Results;
using System.Text.Json.Serialization;

namespace Application.ViewModels
{
    public abstract class Request 
    {
        [JsonIgnore]
        public ValidationResult? ValidationResult { get; set; }

        public virtual bool IsValid()
        {
            throw new NotImplementedException();
        }
    }
}
