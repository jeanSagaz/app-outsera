using Application.Interfaces;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using Core.Models;
using Core.Notifications.Interfaces;
using Equinox.Services.Api.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace Services.Api.Controllers
{    
    [Route("api/v1/[controller]")]
    public class MoviesController : BaseController
    {
        private readonly IMovieService _movieService;        

        public MoviesController(IMovieService movieService,            
            IDomainNotifier notifier) : base(notifier) =>        
            _movieService = movieService;

        [HttpGet("producer-intervals")]
        public async Task<AwardResponseViewModel> GetProducerIntervals() =>
            await _movieService.GetProducerIntervals();

        [HttpGet]
        public async Task<PagedResult<MovieResponseViewModel>> Get([FromQuery] int ps = 8, [FromQuery] int page = 1) =>        
            await _movieService.GetAll(ps, page);

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            var movie = await _movieService.GetById(id);
            return CustomResponse(movie);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieRequestViewModel model)
        {
            var movie = await _movieService.Register(model);

            if (HasNotification())
            {
                return CreatedAtAction(nameof(Get), new { id = movie?.Id }, movie);
            }

            return CustomResponse(movie);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] MovieRequestViewModel model)
        {
            await _movieService.Update(id, model);
            return CustomResponse();
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _movieService.Delete(id);
            return CustomResponse();
        }
    }
}
