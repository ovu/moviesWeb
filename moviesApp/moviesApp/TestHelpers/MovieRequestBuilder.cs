using moviesApp.DataTransfer;

namespace moviesApp.TestHelpers
{
    public class MovieRequestBuilder
    {
        MovieRequestDto movieRequest = null;
        public MovieRequestBuilder()
        {
        }

        public MovieRequestBuilder(MovieRequestDto requestDto)
        {
            movieRequest = requestDto;
        }

        public MovieRequestBuilder WithValidMovie()
        {
            return new MovieRequestBuilder(new MovieRequestDto
            {
                Title = "Title",
                Director = "Director",
                Actors = "Actors",
                Year = 2000,
                Image = "https://picsum.photos/id/723/500/100"
            });
        }

        public MovieRequestBuilder WithTitle(string title)
        {
            return new MovieRequestBuilder(new MovieRequestDto
            {
                Title = title,
                Director = movieRequest.Director,
                Actors = movieRequest.Actors,
                Year = movieRequest.Year,
                Image = movieRequest.Image
            });
        }

        public MovieRequestDto Build()
        {
            return movieRequest;
        }

    }
}
