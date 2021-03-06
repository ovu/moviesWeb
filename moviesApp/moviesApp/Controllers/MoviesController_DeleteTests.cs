﻿using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using Moq;
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

        [Fact]
        public async Task DeletingAMovieWithAValidMovieIdShouldReturnOk()
        {
            // Arrange
            var mockRepo = new Mock<IMoviesRepository>(MockBehavior.Strict);
            mockRepo.Setup(repo => repo.DeleteMovie(It.IsAny<ObjectId>()))
                .ReturnsAsync(true);

            var controller = new MoviesController(mockRepo.Object);

            // Act
            var result = await controller.DeleteMovie("5d5fbc449fdf6d3f23ce4510");

            // Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
