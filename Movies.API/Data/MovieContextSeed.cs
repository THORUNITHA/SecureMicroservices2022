using Movies.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Movies.API.Data
{
    public class MovieContextSeed
    {
        public static void SeedAsync(MoviesAPIContext moviesContext)
        {
            if (!moviesContext.Movie.Any())
            {
                var movies = new List<Movie>
                {
            new Movie
            {
                Id = 1,
                Genre = "Drama",
                Title = "The Redemption",
                Rating = "9.3",
                ImageUrl = "images/src",
                ReleaseDate = new DateTime(1994, 5,5),
                Owner = "alice"
            },
            new Movie
            {
                Id=2,
                Genre="Crime",
                Title="12 Angry Men",
                Rating="8.9",
                ImageUrl="images/src",
                ReleaseDate=new DateTime(1993,5,5),
                Owner="alice"
            },
             new Movie
            {
                Id=3,
                Genre="Romantic",
                Title="Titanic",
                Rating="8.9",
                ImageUrl="images/src",
                ReleaseDate=new DateTime(1993,5,5),
                Owner="bob"
            }
            };
                moviesContext.Movie.AddRange(movies);
                moviesContext.SaveChanges();
            }
        }
    }
}
