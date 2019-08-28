using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
