using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Json.Serialization;

namespace StarWars.Core
{
	public class Movie
	{
		[Key]
		public int MovieId { get; set; }
		[Required, StringLength(100)]
		[JsonPropertyName("title")]
		public string Title { get; set; }
		[JsonPropertyName("episode_id")]
		public int EpisodeId { get; set; }
		[JsonPropertyName("opening_crawl")]
		public string OpeningCrawl { get; set; }
		[JsonPropertyName("director")]
		public string Director { get; set; }
		[JsonPropertyName("producer")]
		public string Producer { get; set; }
		[JsonPropertyName("release_date")]
		public string ReleaseDate { get; set; }
		//[JsonPropertyName("characters")]
		[JsonIgnore]
		public string Characters { get; set; }
		//[JsonPropertyName("planets")]
		[JsonIgnore]
		public string Planets { get; set; }
		//[JsonPropertyName("starships")]
		[JsonIgnore]
		public string Starships { get; set; }
		//[JsonPropertyName("vehicles")]
		[JsonIgnore]
		public string Vehicles { get; set; }
		//[JsonPropertyName("species")]
		[JsonIgnore]
		public string Species { get; set; }
		[DisplayFormat(DataFormatString = "{0:n2}", ApplyFormatInEditMode = true)]
		[JsonIgnore]
		public double AverageRating { get; set; }
		[JsonIgnore]
		public MovieRating MovieRating { get; set; }
	}
}
