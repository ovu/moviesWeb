using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using moviesApp.Model;
using moviesApp.TestHelpers;
using Xunit;

namespace moviesApp.Controllers
{
    public class MoviesController_InsertTests
    {
        [Fact]
        public async Task InsertingAValidMovieShouldReturnOk()
        {
            // Arrange
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);
            var movie = new Movie ("title", "director", "actors", "image", 2000);
            mockRepo.Setup(repo => repo.InsertMovie(It.IsAny<Movie>()))
                .ReturnsAsync(movie);

            var controller = new MoviesController(mockRepo.Object);

            // Act
            var movieRequest = new MovieRequestBuilder().WithValidMovie().Build();
            var result = await controller.InsertMovie(movieRequest);

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async Task InsertingAnInvalidModelShouldReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);
            var movie = new Movie("title", "director", "actors", "image", 2000);
            mockRepo.Setup(repo => repo.InsertMovie(It.IsAny<Movie>()))
                .ReturnsAsync(movie);

            var controller = new MoviesController(mockRepo.Object);
            // This will cause the validation fails
            controller.ModelState.AddModelError("Error", "Error");

            // Act
            var movieRequest = new MovieRequestBuilder().WithValidMovie().Build();
            var result = await controller.InsertMovie(movieRequest);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
