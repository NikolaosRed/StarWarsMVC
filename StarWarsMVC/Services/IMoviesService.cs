using StarWars.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StarWarsMVC.Managers
{
	public interface IMoviesService
	{
		List<Movie> GetMoviesFromAPI();
		Movie GetMovieFromAPI(int id);
	}
}
