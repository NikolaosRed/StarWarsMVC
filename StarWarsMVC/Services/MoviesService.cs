using Microsoft.Extensions.Options;
using StarWars.Core;
using StarWarsMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace StarWarsMVC.Managers
{
    public class MoviesService : IMoviesService
    {
        private ApiURL swapidev { get; set; }
        [JsonPropertyName("results")]
        public List<Movie> MoviesList { get; set; }
		public MoviesService()
		{

		}
        public MoviesService(IOptions<ApiURL> settings)
        {
            swapidev = settings.Value;
        }

        public List<Movie> GetMoviesFromAPI()
        {
            var url = swapidev.Url;

            using (var client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var listOfMovies = JsonSerializer.Deserialize<MoviesService>(json);
                    return listOfMovies.MoviesList;
                }
                return null;
            }
        }
    
        public Movie GetMovieFromAPI(int id)
        {
            var url = string.Format("{0}{1}/", swapidev.Url, id);
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(url).Result;
                if (response.IsSuccessStatusCode)
                {
                    var json = response.Content.ReadAsStringAsync().Result;
                    var movie = JsonSerializer.Deserialize<Movie>(json);
                    return movie;
                }
                return null;
            }
        }
    }
}
