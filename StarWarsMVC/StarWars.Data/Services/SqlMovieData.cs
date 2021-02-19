using StarWars.Core;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace StarWars.Data
{
	public class SqlMovieData : IMovieData
	{
		private readonly StarWarsDbContext db;

		public SqlMovieData(StarWarsDbContext db)
		{
			this.db = db;
		}
		public void Add(Movie newMovie)
		{
			db.Movies.Add(newMovie);
			db.SaveChanges();
		}
		
		public void Delete(int id)
		{
			var movie = db.Movies.Find(id);
			var rating = db.Ratings.Where(r => r.EpisodeId == movie.EpisodeId).ToList();
			if(rating != null)
			{
				foreach(var rate in rating)
				{
					db.Ratings.Remove(rate);
				}
			}
			if (movie != null)
			{
				db.Movies.Remove(movie);
			}
			db.SaveChanges();
		}

		public Movie GetMovieById(int id)
		{
			return db.Movies.Find(id);
		}

		public IEnumerable<Movie> GetAll()
		{

			//return from m in db.Movies
			//	   orderby m.Title
			//	   select m;
			
			return db.Movies.OrderBy(m => m.Title).Select(m => m);

		}

		public void Update(Movie updatedMovie)
		{
			var entity = db.Movies.Attach(updatedMovie);
			entity.State = EntityState.Modified;
			db.SaveChanges();
		}

		public void Update(MovieRating updatedRating)
		{
			var entity = db.Ratings.Attach(updatedRating);
			entity.State = EntityState.Modified;
			db.SaveChanges();
		}

		public void Add(MovieRating newRating)
		{
			db.Ratings.Add(newRating);
			db.SaveChanges();
		}

		public MovieRating GetRatingById(int id)
		{
			return db.Ratings.Find(id);
		}
		public List<MovieRating> GetAllRatings(Movie movie)
		{
			var rating = db.Ratings.Where(r => r.EpisodeId == movie.EpisodeId).ToList();
			return rating;
		}
	}
}
