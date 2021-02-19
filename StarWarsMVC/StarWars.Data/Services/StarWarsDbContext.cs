using StarWars.Core;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace StarWars.Data
{
	public class StarWarsDbContext : DbContext
	{
		public DbSet<Movie> Movies { get; set; }
		public DbSet<MovieRating> Ratings { get; set; }
		public StarWarsDbContext(DbContextOptions<StarWarsDbContext> options) : base(options)
		{

		}

	}
}
