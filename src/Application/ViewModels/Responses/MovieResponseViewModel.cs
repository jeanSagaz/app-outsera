using System.Text.Json.Serialization;

namespace Application.ViewModels.Responses
{
    public class MovieResponseViewModel
    {
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

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
    }
}
