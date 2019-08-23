using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace moviesApp.Model
{
    public class Movie
    {
        [BsonId]
        public ObjectId Id { get; set; }

        public string Title { get; set; }
        public string Director { get; set; }
        public string Actors { get; set; }
        public string Image { get; set; }
        public int Year { get; set; }

        [BsonConstructor]
        public Movie(string title, string director, string actors, string image, int year)
        {
            Title = title;
            Director = director;
            Actors = actors;
            Image = image;
            Year = year;
        }
    }
}
