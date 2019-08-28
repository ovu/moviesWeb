using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
using moviesApp.Model;
using Xunit;

namespace moviesApp.Controllers
{
    public class MoviesController_GetMovieTests
    {
        [Fact]
        public async Task GettingAMovieWithAnInvalidMovieIdShouldReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);

            var controller = new MoviesController(mockRepo.Object);

            // Act
            var result = await controller.GetMovie("invalid id");

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task GettingAMovieWithAValidMovieIdShouldReturnOk()
        {
            // Arrange
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);
            var movie = new Movie("title", "director", "actors", "image", 2000);
            mockRepo.Setup(repo => repo.FindMovie(It.IsAny<ObjectId>()))
                .ReturnsAsync(movie);

            var controller = new MoviesController(mockRepo.Object);

            // Act
            var result = await controller.GetMovie("5d5fbc449fdf6d3f23ce4510");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
