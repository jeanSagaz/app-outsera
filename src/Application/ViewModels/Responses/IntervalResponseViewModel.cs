using System.Text.Json.Serialization;

namespace Application.ViewModels.Responses
{
    public class IntervalResponseViewModel
    {
        [JsonPropertyName("producer")]
        public string? Producer { get; set; }

        [JsonPropertyName("interval")]
        public int? Interval { get; set; }

        [JsonPropertyName("previousWin")]
        public int? PreviousWin { get; set; }

        [JsonPropertyName("followingWin")]
        public int? FollowingWin { get; set; }
    }

    public class AwardResponseViewModel
    {
        [JsonPropertyName("min")]
        public List<IntervalResponseViewModel> Min { get; set; } = new();

        [JsonPropertyName("max")]
        public List<IntervalResponseViewModel> Max { get; set; } = new();
    }
}
