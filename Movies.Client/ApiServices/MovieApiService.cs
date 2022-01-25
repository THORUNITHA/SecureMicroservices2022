using IdentityModel.Client;
using Movies.Client.ApiServices;
using Movies.Client.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
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
            //// creates a new HttpClient to talk to our IdentityServer (localhost:5005)
            var client = new HttpClient();

            //// just checks if we can reach the Discovery document. Not 100% needed but..
            var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5005");
            if (disco.IsError)
           {
            return null; // throw 500 error
            }

            //// 2. Authenticates and get an access token from Identity Server
           var tokenResponse = await client.RequestClientCredentialsTokenAsync(apiClientCredentials);            
            if (tokenResponse.IsError)
            {
              return null;
            }

            //// Another HttpClient for talking now with our Protected API
           var apiClient = new HttpClient();

            //// 3. Set the access_token in the request Authorization: Bearer <token>
            apiClient.SetBearerToken(tokenResponse.AccessToken);

            //// 4. Send a request to our Protected API
            var response = await apiClient.GetAsync("https://localhost:5001/api/movies");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            var movieList = JsonConvert.DeserializeObject<List<Movie>>(content);
            return movieList;
          /*  var movieList = new List<Movie>();
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
            return await Task.FromResult(movieList);*/
        }

        public Task<Movie> UpdateMovie(Movie movie)
        {
            throw new System.NotImplementedException();
        }
    }
}
