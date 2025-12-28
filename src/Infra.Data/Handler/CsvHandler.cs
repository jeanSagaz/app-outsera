using CsvHelper;
using CsvHelper.Configuration;
using Infra.Data.CsvMapping;
using Infra.Data.Models;
using System.Globalization;

namespace Infra.Data.Handler
{
    public static class CsvHandler
    {
        public static IEnumerable<MovieCsv> ReaderCsv()
        {
            var config = new CsvConfiguration(CultureInfo.InvariantCulture)
            {
                Delimiter = ";",
                HasHeaderRecord = true,
                TrimOptions = TrimOptions.Trim,
                HeaderValidated = null,
                MissingFieldFound = null
            };
            
            string path = Path.Combine(AppContext.BaseDirectory, "Movielist.csv");            

            List<MovieCsv> moviesCsv = new();
            using var reader = new StreamReader(path);
            using (var csv = new CsvReader(reader, config))
            {
                csv.Context.RegisterClassMap<MovieMapping>();

                moviesCsv = csv.GetRecords<MovieCsv>().ToList();
            };

            return moviesCsv;
    }
    }
}
