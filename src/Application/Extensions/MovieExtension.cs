using Application.ViewModels.Responses;
using Domain.Models;
using Infra.Data.Models;

namespace Application.Extensions
{
    public static class MovieExtension
    {
        public static MovieResponseViewModel? ToViewModel(this Movie? movie)
        {
            if (movie is null) return null;

            return new MovieResponseViewModel
            {
                Id = movie.Id,
                Year = movie.Year,
                Title = movie.Title,
                Studio = movie.Studio,
                Producer = movie.Producer,
                Winner = movie.Winner,
            };
        }

        public static IEnumerable<MovieResponseViewModel>? ToViewModel(this IEnumerable<Movie>? movies)
        {
            if (movies is null) return null;

            return movies.Select(c => c.ToViewModel())!;
        }
            

        public static Movie? ToEntity(this MovieResponseViewModel movieViewModel)
        {
            if (movieViewModel == null) return null;

            return Movie.MovieFactory(movieViewModel.Id, movieViewModel.Year, movieViewModel.Title, movieViewModel.Studio, movieViewModel.Producer, movieViewModel.Winner);
        }
    }
}
