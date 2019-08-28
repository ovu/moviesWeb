using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using moviesApp.Model;
using moviesApp.TestHelpers;
using Xunit;

namespace moviesApp.Controllers
{
    public class MoviesController_UpdateTests
    {

        [Fact]
        public async Task UpdatingAMovieWithAnInvalidMovieIdShouldReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);

            var controller = new MoviesController(mockRepo.Object);

            // Act
            var movieRequest = new MovieRequestBuilder().WithValidMovie().Build();
            var result = await controller.UpdateMovie("invalid", movieRequest);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task UpdatingAMovieWithAValidMovieIdShouldReturnOk()
        {
            // Arrange
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);
            mockRepo.Setup(repo => repo.UpdateMovie(It.IsAny<Movie>()))
                .ReturnsAsync(true);

            var controller = new MoviesController(mockRepo.Object);

            // Act
            var movieRequest = new MovieRequestBuilder().WithValidMovie().Build();
            var result = await controller.UpdateMovie("5d5fbc449fdf6d3f23ce4510", movieRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task UpdatingAMovieWithAnInvalidModelShouldReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);
            mockRepo.Setup(repo => repo.UpdateMovie(It.IsAny<Movie>()))
                .ReturnsAsync(true);

            var controller = new MoviesController(mockRepo.Object);
            // This will cause the validation fails
            controller.ModelState.AddModelError("Error", "Error");

            // Act
            // Title longer than 100

            var movieRequest = new MovieRequestBuilder().WithValidMovie().Build();
            var result = await controller.UpdateMovie("5d5fbc449fdf6d3f23ce4510", movieRequest);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
