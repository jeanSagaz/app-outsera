using System.Text.Json.Serialization;

namespace Core.Models
{
    public class PagedResult<T> where T : class
    {
        [JsonPropertyName("list")]
        public IEnumerable<T>? List { get; set; }

        [JsonPropertyName("totalResults")]
        public int TotalResults { get; set; }

        [JsonPropertyName("pageIndex")]
        public int PageIndex { get; set; }

        [JsonPropertyName("pageSize")]
        public int PageSize { get; set; }
    }
}
