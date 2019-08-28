using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using moviesApp.Model;
using moviesApp.TestHelpers;
using Xunit;

namespace moviesApp.Controllers
{
    public class MoviesController_DeleteTests
    {

        [Fact]
        public async Task DeletingAMovieWithAnInvalidMovieIdShouldReturnBadRequest()
        {
            // Arrange
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);

            var controller = new MoviesController(mockRepo.Object);

            // Act
            var result = await controller.DeleteMovie("invalid id");

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }
    }
}
