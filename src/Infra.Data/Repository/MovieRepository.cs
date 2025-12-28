using Domain.Interfaces;
using Domain.Models;
using Infra.Data.Context;
using Infra.Data.Models;

namespace Infra.Data.Repository
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(ApplicationContext context) : base(context)
        {
        }
    }
}
