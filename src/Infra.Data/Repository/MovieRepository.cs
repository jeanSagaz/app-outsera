using Domain.Interfaces;
using Domain.Models;
using Infra.Data.Context;

namespace Infra.Data.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
