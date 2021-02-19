using StarWars.Core;
using System.Collections.Generic;
using System.Text;

namespace StarWars.Data
{
	public interface IMovieData
	{
		IEnumerable<Movie> GetAll();
		Movie GetMovieById(int id);
		MovieRating GetRatingById(int id);
		void Update(Movie updatedMovie);
		void Update(MovieRating updatedRating);
		void Add(Movie newMovie);
		void Add(MovieRating newRating);
		void Delete(int id);
		List<MovieRating> GetAllRatings(Movie movie);
	}
}
