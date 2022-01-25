using IdentityModel.Client;
using Movies.Client.ApiServices;
using Movies.Client.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Client.ApiServices
{
    public class MovieApiService : IMovieApiService
    {
        public Task<Movie> CreateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }

        public Task DeleteMovie(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Movie> GetMovie(string id)
        {
            throw new System.NotImplementedException();
        }
        
        public async Task<IEnumerable<Movie>> GetMovies()
        {
            //// 1. "retrieve" our api credentials. This must be registered on Identity Server!
            var apiClientCredentials = new ClientCredentialsTokenRequest
            {
                Address = "https://localhost:5005/connect/token",

               ClientId = "movieClient",
               ClientSecret = "secret",

            //    // This is the scope our Protected API requires. 
             Scope = "movieAPI"
           };
            var movieList = new List<Movie>();
             movieList.Add(
                 new Movie
            {
                Id = 1,
                Genre = "Drama",
                Title = "The Redemption",
                Rating = "9.3",
                ImageUrl = "images/src",
                ReleaseDate = new DateTime(1994, 5,5),
                Owner = "alice"
            }
                 );
            return await Task.FromResult(movieList);
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }
    }
}
