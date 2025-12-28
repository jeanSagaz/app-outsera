using Core.DomainObjects;

namespace Domain.Models
{
    public class Movie : Entity
    {
        public int? Year { get; private set; }

        public string? Title { get; private set; }

        public string? Studio { get; private set; }

        public string? Producer { get; private set; }

        public bool? Winner { get; private set; }

        public Movie(int? year, string? title, string? studio, string? producer, bool? winner)
        {
            Year = year; 
            Title = title;
            Studio = studio;
            Producer = producer;
            Winner = winner;
        }

        public static Movie MovieFactory(Guid id, int? year, string? title, string? studio, string? producer, bool? winner)
        {
            return new Movie(year, title, studio, producer, winner)
            {
                Id = id,
            };
        }
    }
}
