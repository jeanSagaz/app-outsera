using Application.Validations;
using System.Text.Json.Serialization;

namespace Application.ViewModels.Requests
{
    public class MovieRequestViewModel : Request
    {
        [JsonPropertyName("year")]
        public int? Year { get; set; }

        [JsonPropertyName("title")]
        public string? Title { get; set; }

        [JsonPropertyName("studio")]
        public string? Studio { get; set; }

        [JsonPropertyName("producer")]
        public string? Producer { get; set; }

        [JsonPropertyName("winner")]
        public bool? Winner { get; set; }

        public override bool IsValid()
        {
            ValidationResult = new MovieRequestValidation().Validate(this);
            return ValidationResult.IsValid;
        }
    }
}
