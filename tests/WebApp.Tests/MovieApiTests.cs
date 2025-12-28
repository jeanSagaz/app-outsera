using Application.ViewModels;
using Application.ViewModels.Requests;
using Application.ViewModels.Responses;
using Core.Models;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using WebApp.Tests.Config;
using Xunit;

namespace WebApp.Tests
{    
    public class MovieApiTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly CustomWebApplicationFactory<Program> _factory;
        private readonly string _host;

        public MovieApiTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
            _host = "https://localhost:7227";
        }

        [Fact(DisplayName = "Add a new invalid movie")]
        [Trait("Movies", "API Integration - Movies")]
        public async Task AddMovie_NewMovie_ShouldReturnWithError()
        {
            // Arrange
            var movieData = new MovieRequestViewModel
            {
                Year = 2025,
                Title = string.Empty,
                Studio = string.Empty,
                Producer = string.Empty,
                Winner = null
            };

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{_host}/api/v1/movies", movieData);

            // Assert
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact(DisplayName = "Add a new valid movie")]
        [Trait("Movies", "API Integration - Movies")]
        public async Task AddMovie_NewMovie_ShouldReturnWithSuccess()
        {
            // Arrange
            var movieData = new MovieRequestViewModel
            {
                Year = 2025,
                Title = "The Long Walk",
                Studio = "Lionsgate Films",
                Producer = "Francis Lawrence",
                Winner = null
            };

            var client = _factory.CreateClient();

            // Act
            var response = await client.PostAsJsonAsync($"{_host}/api/v1/movies", movieData);

            // Assert
            response.EnsureSuccessStatusCode();
        }

        [Fact(DisplayName = "Get movies")]
        [Trait("Movies", "API Integration - Movies")]
        public async Task GetMovies_ShouldReturnWithSuccess()
        {
            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync($"{_host}/api/v1/movies?ps=8&page=1");

            // Assert
            response.EnsureSuccessStatusCode();
            var content = await response.Content.ReadAsStringAsync();
            var movies = JsonSerializer.Deserialize<PagedResult<MovieResponseViewModel>>(content);
            Assert.True(movies is not null && movies.TotalResults > 0);

        }
    }
}
