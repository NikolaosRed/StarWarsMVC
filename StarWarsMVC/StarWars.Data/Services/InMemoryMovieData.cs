using StarWars.Core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.Data
{
	public class InMemoryMovieData : IMovieData
	{
		public List<Movie> movies;
		public List<MovieRating> ratings;


		public InMemoryMovieData()
		{
			movies = new List<Movie>()
			{
				new Movie {
					MovieId = 1, Title = "A New Hope", EpisodeId = 4,
					OpeningCrawl = "It is a period of civil war....", Director = "George Lucas",
					Producer = "Gary Kurtz, Rick McCallum", ReleaseDate ="1977-05-25",
					Characters = "people/1/, people/2/, people/3/, people/4/",
					Planets = "planets/1/, planets/2/, planets/3/",
					Starships = "starships/2/, starships/3/, starships/5/",
					Vehicles =  "vehicles/4/, vehicles/6/, vehicles/7/",
					Species = "spices/1/, spices/2/, spices/3/, spices/4/",

				},
				new Movie {
					MovieId = 2, Title = "The Empire Strikes Back", EpisodeId = 5,
					OpeningCrawl = "It is a dark time for the....", Director = "Irvin Kershner",
					Producer = "Gary Kurtz, Rick McCallum", ReleaseDate ="1980-05-17",
					Characters = "people/1/, people/2/, people/3/, people/4/",
					Planets = "planets/1/, planets/2/, planets/3/",
					Starships = "starships/2/, starships/3/, starships/5/",
					Vehicles =  "vechicles/4/, vehicles/6/, vehicles/7/",
					Species = "spices/1/, spices/2/, spices/3/, spices/4/",

				},
				new Movie {
					 MovieId = 3, Title = "Return of the Jedi", EpisodeId = 6,
					OpeningCrawl = "Luke Skywalker has returned....", Director = "Irvin Kershner",
					Producer = "Gary Kurtz, Rick McCallum", ReleaseDate ="1980-05-17",
					Characters = "people/1/, people/2/, people/3/, people/4/" ,
					Planets = "planets/1/, planets/2/, planets/3/",
					Starships = "starships/2/, starships/3/, starships/5/",
					Vehicles =  "vehicles/4/, vehicles/6/, vehicles/7/",
					Species = "spices/1/, spices/2/, spices/3/, spices/4/",

				}
			};

			ratings = new List<MovieRating>()
			{
				new MovieRating
				{
					EpisodeId = 1,
					Id = 1,
					ScoreSum = MovieRatings.Masterpiece
				},
				new MovieRating
				{
					EpisodeId = 2,
					Id = 2,
					ScoreSum = MovieRatings.Masterpiece
				},
				new MovieRating
				{
					EpisodeId = 2,
					Id = 3,
					ScoreSum = MovieRatings.Masterpiece
				}
			};
		}

		public IEnumerable<Movie> GetAll()
		{
			//LINQ query
			return from m in movies
				   orderby m.Title
				   select m;

			//Extension methods
			//return movies.OrderBy(m => m.Title); 
		}

		public Movie GetMovieById(int id)
		{
			return movies.SingleOrDefault(m => m.MovieId == id);
		}

		public void Delete(int id)
		{
			var movie = GetMovieById(id);
			if (movie != null)
			{
				movies.Remove(movie);
			}
		}
		public void Add(Movie movie)
		{
			movies.Add(movie);
			movie.MovieId = movies.Max(m => m.MovieId) + 1;
		}

		public void Update(Movie movie)
		{
			var existing = GetMovieById(movie.MovieId);
			if (existing != null)
			{
				existing.Title = movie.Title;
				existing.Characters = movie.Characters;
				existing.Director = movie.Director;
				existing.OpeningCrawl = movie.OpeningCrawl;
				existing.Planets = movie.Planets;
				existing.Producer = movie.Producer;
				existing.ReleaseDate = movie.ReleaseDate;
				existing.Species = movie.Species;
				existing.Starships = movie.Starships;
				existing.Vehicles = movie.Vehicles;
				existing.MovieRating = movie.MovieRating;
			}
		}

		public void Update(MovieRating updatedRating)
		{
			var existing = GetRatingById(updatedRating.Id);
			if (existing != null)
			{
				existing.EpisodeId = updatedRating.EpisodeId;
				existing.ScoreSum = updatedRating.ScoreSum;
			}
		}

		public MovieRating GetRatingById(int id)
		{
			return ratings.SingleOrDefault(r => r.Id == id);
		}

		public void Add(MovieRating newRating)
		{
			ratings.Add(newRating);
			newRating.Id = ratings.Max(r => r.Id) + 1;
		}

		public List<MovieRating> GetAllRatings(Movie movie)
		{
			var sum = ratings.Where(r => r.EpisodeId == movie.EpisodeId).ToList();
			return sum;
		}
	}
}
