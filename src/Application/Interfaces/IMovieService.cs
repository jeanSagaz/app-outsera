using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using Core.Models;

namespace Application.Interfaces
{
    public interface IMovieService
    {
        Task<MovieResponseViewModel?> GetById(Guid id);

        Task<PagedResult<MovieResponseViewModel>> GetAll(int pageSize, int pageIndex);

        Task<AwardResponseViewModel> GetProducerIntervals();

        Task<MovieResponseViewModel?> Register(MovieRequestViewModel model);

        Task Update(Guid id, MovieRequestViewModel model);

        Task Delete(Guid id);
    }
}
