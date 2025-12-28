using CsvHelper.Configuration;
using Infra.Data.Models;

namespace Infra.Data.CsvMapping
{
    public class MovieMapping : ClassMap<MovieCsv>
    {
        public MovieMapping()
        {
            Map(m => m.Year).Name("year");
            Map(m => m.Title).Name("title");
            Map(m => m.Studio).Name("studios");
            Map(m => m.Producer).Name("producers");
            Map(m => m.Winner).Name("winner");
        }
    }
}
