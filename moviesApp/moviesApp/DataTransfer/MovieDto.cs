namespace moviesApp.Model
{
    public class MovieDto
    {
        public string Title { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }

        public Movie toMovie()
        {
            return new Movie(Title, Director, Actors, Image, Year);
        }
    }
}
