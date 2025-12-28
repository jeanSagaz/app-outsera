using CsvHelper.Configuration.Attributes;

namespace Infra.Data.Models
{
    public class MovieCsv
    {
        public int? Year { get; set; }

        public string? Title { get; set; }

        public string? Studio { get; set; }

        public string? Producer { get; set; }

        public string? Winner { get; set; }
    }
}
