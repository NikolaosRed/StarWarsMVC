using System.ComponentModel.DataAnnotations;

namespace StarWars.Core
{
	public class MovieRating
	{
		[Key]
		public int Id { get; set; }
		public int EpisodeId { get; set; }
		public MovieRatings ScoreSum { get; set; }
	}
	public enum MovieRatings
	{
		Disaster = 0,
		Awful = 1,
		Bad = 2,
		Poor = 3,
		BelowAverage = 4,
		Average = 5,
		Good = 6,
		Great = 7,
		Superb = 8,
		Perfect = 9,
		Masterpiece = 10
	}
}
