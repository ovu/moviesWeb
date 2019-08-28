using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using moviesApp.Config;
using moviesApp.Controllers;
using moviesApp.Model;

namespace moviesApp.Repository
{
    public class MoviesRepository : IMoviesRepository
    {
        MongoClient client;
        IMongoDatabase database;
        IMongoCollection<Movie> collection;
        public MoviesRepository(IOptions<AppSettings> settings)
        {
            BsonClassMap.RegisterClassMap<Movie>(cm =>
            {
                cm.AutoMap();

                cm.MapMember(c => c.Title).SetElementName("title");
                cm.MapMember(c => c.Director).SetElementName("director");
                cm.MapMember(c => c.Image).SetElementName("image");
                cm.MapMember(c => c.Actors).SetElementName("actors");
                cm.MapMember(c => c.Year).SetElementName("year");
            });

            // Use the connection string
            client = new MongoClient(settings.Value.MongoDbConnectionString);
            database = client.GetDatabase("movie");

            collection = database.GetCollection<Movie>("movie");
        }

        public async Task<IEnumerable<Movie>> ListMovies()
        {
            var movies = await collection.AsQueryable().ToListAsync();

            return movies;
        }

        public async Task<Movie> InsertMovie(Movie movie)
        {
            await collection.InsertOneAsync(movie);

            return movie;
        }

        public async Task<bool> UpdateMovie(Movie movie)
        {
            var result = await collection.ReplaceOneAsync(x => x.Id == movie.Id, movie);

            if (result.MatchedCount == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<bool> DeleteMovie(ObjectId movieId)
        {
            var result = await collection.DeleteOneAsync(x => x.Id == movieId);

            if (result.DeletedCount == 1)
            {
                return true;
            }

            return false;
        }

        public async Task<IEnumerable<Movie>> FindMovies(string textToSearch)
        {
            var textToSearchLower = textToSearch.ToLower();
            var movies = await collection.FindAsync(x =>
                x.Title.ToLower().Contains(textToSearchLower) ||
                x.Director.ToLower().Contains(textToSearchLower) ||
                x.Actors.ToLower().Contains(textToSearchLower)
            );

            return movies.ToEnumerable();
        }

        public async Task<Movie> FindMovie(ObjectId movieId)
        {
            var result = await collection.FindAsync(x => x.Id == movieId);

            return result.First();
        }
    }
}