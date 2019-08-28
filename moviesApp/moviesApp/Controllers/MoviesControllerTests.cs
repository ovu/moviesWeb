using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using moviesApp.DataTransfer;
using moviesApp.Model;
using Xunit;

namespace moviesApp.Controllers
{
    public class MoviesControllerTests
    {
        [Fact]
        public async Task SearchingMovieWithEmptyStringShouldReturnEmptyList()
        {
            // Arrange
            // Repository should not be called
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);
            var controller = new MoviesController(mockRepo.Object);

            // Act
            var result = await controller.SearchMovies(string.Empty);

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<MovieResponseDto>>(result);
            Assert.Equal(0, model.Count());
            
        }

        [Fact]
        public async Task SearchingMovieWithAStringShouldReturnResultFromRepository()
        {
            // Arrange
            var textToSearch = "movie";
            Movie[] movieList = { new Movie("Silence", "Martin Scorsese", "", "", 2000) };
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);
            mockRepo.Setup(repo => repo.FindMovies(textToSearch))
                .ReturnsAsync(movieList);

            var controller = new MoviesController(mockRepo.Object);

            // Act
            var result = await controller.SearchMovies(textToSearch);

            // Assert
            var model = Assert.IsAssignableFrom<IEnumerable<MovieResponseDto>>(result);
            Assert.Equal(1, model.Count());

        }

        [Fact]
        public async Task UpdatingAMovieWithAnInvalidMovieIdShouldReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);

            var controller = new MoviesController(mockRepo.Object);

            // Act
            var movieRequest = new MovieRequestDto { Title = "TestTitle" };
            var result = await controller.UpdateMovie("invalid", movieRequest);

            // Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task UpdatingAMovieWithAValidMovieIdShouldReturnOk()
        {
            // Arrange
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);
            mockRepo.Setup(repo => repo.UpdateMovie (It.IsAny<Movie>()))
                .ReturnsAsync(true);

            var controller = new MoviesController(mockRepo.Object);

            // Act
            var movieRequest = new MovieRequestDto { Title = "TestTitle" };
            var result = await controller.UpdateMovie("5d5fbc449fdf6d3f23ce4510", movieRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
