using Application.Extensions;
using Application.Interfaces;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using Core.Models;
using Core.Notifications;
using Core.Notifications.Interfaces;
using Core.Resources;
using Domain.Interfaces;
using Domain.Models;

namespace Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IDomainNotifier _domainNotifier;

        public MovieService(IMovieRepository movieRepository,
            IDomainNotifier domainNotifier)
        {
            _movieRepository = movieRepository;
            _domainNotifier = domainNotifier;
        }

        public async Task<MovieResponseViewModel?> GetById(Guid id) =>
            (await _movieRepository.GetByIdAsync(id)).ToViewModel();

        public async Task<PagedResult<MovieResponseViewModel>> GetAll(int pageSize, int pageIndex)
        {
            var movies = await _movieRepository.GetAllAsync(pageSize, pageIndex);

            return new PagedResult<MovieResponseViewModel>()
            {
                List = movies.List.ToViewModel(),
                TotalResults = movies.TotalResults,
                PageIndex = pageIndex,
                PageSize = pageSize,
            };
        }

        public async Task<AwardResponseViewModel> GetProducerIntervals()
        {
            // 1. Filtrar apenas vencedores
            var wins = await _movieRepository.SearchAsync(m => m.Winner != null && m.Winner == true);

            // 2. Produtor com maior intervalo entre dois prêmios consecutivos
            var intervalsBetweenTwoConsecutiveAwards = wins
            .GroupBy(g => g.Producer)
            .Where(g => g.Count() > 1) // Apenas produtores com mais de uma vitória
            .SelectMany(g => {
                var list = g.ToList();
                var result = new List<IntervalResponseViewModel>();

                if (list.Max(m => m.Year) - list.Min(m => m.Year) > 1)
                {
                    result.Add(new IntervalResponseViewModel
                    {
                        Producer = g.Key,
                        Interval = list.Max(m => m.Year) - list.Min(m => m.Year),
                        PreviousWin = list.Min(m => m.Year),
                        FollowingWin = list.Max(m => m.Year)
                    });
                }

                return result;
            })
            .ToList();

            // 3. Produtor que obteve dois prêmios mais rápido
            var intervalsTwoAwardsFaster = wins
            .GroupBy(g => g.Producer)
            .Where(g => g.Count() > 1) // Apenas produtores com mais de uma vitória
            .SelectMany(g => {
                var list = g.ToList();
                var result = new List<IntervalResponseViewModel>();

                if (list.Max(m => m.Year) - list.Min(m => m.Year) <= 1)
                {
                    result.Add(new IntervalResponseViewModel
                    {
                        Producer = g.Key,
                        Interval = list.Max(m => m.Year) - list.Min(m => m.Year),
                        PreviousWin = list.Min(m => m.Year),
                        FollowingWin = list.Max(m => m.Year)
                    });
                }

                return result;
            })
            .ToList();

            if (!intervalsBetweenTwoConsecutiveAwards.Any() && !intervalsTwoAwardsFaster.Any()) return new AwardResponseViewModel();

            return new AwardResponseViewModel
            {
                Max = intervalsBetweenTwoConsecutiveAwards.ToList(),
                Min = intervalsTwoAwardsFaster.ToList(),
            };
        }

        public async Task<MovieResponseViewModel?> Register(MovieRequestViewModel model)
        {
            if (!model.IsValid())
            {
                foreach(var error in model.ValidationResult!.Errors)
                {
                    _domainNotifier.Add(new DomainNotification(error.ErrorMessage, error.PropertyName));
                }

                return null;
            }

            Movie movie = new(model.Year, 
                model.Title, 
                model.Studio, 
                model.Producer, 
                model.Winner);

            await _movieRepository.AddAsync(movie);
            if(await _movieRepository.SaveChangesAsync() <= 0)
            {
                _domainNotifier.Add(new DomainNotification(Errors.ErrorRegister));
            }

            return new MovieResponseViewModel()
            {
                Id = movie.Id,
                Year = movie.Year,
                Title = movie.Title,
                Studio = movie.Studio,
                Producer = movie.Producer,
                Winner = movie.Winner,
            };
        }

        public async Task Update(Guid id, MovieRequestViewModel model)
        {
            if (!model.IsValid())
            {
                foreach (var error in model.ValidationResult!.Errors)
                {
                    _domainNotifier.Add(new DomainNotification(error.ErrorMessage, error.PropertyName));
                }

                return;
            }

            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie is null)
            {
                _domainNotifier.Add(new DomainNotification(Errors.MovieNotFound));
                return;
            }

            Movie movieUpdate = Movie.MovieFactory(movie.Id, 
                model.Year, 
                model.Title, 
                model.Studio, 
                model.Producer, 
                model.Winner);

            _movieRepository.Update(movieUpdate);
            if (await _movieRepository.SaveChangesAsync() <= 0)
            {
                _domainNotifier.Add(new DomainNotification(Errors.ErrorUpdate));
            }
        }

        public async Task Delete(Guid id)
        {
            var movie = await _movieRepository.GetByIdAsync(id);
            if (movie is null)
            {
                _domainNotifier.Add(new DomainNotification(Errors.MovieNotFound));
                return;
            }

            _movieRepository.Remove(movie);
            if (await _movieRepository.SaveChangesAsync() <= 0)
            {
                _domainNotifier.Add(new DomainNotification(Errors.ErrorDelete));
            }
        }
    }
}
