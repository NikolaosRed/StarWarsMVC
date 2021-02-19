using Microsoft.VisualStudio.TestTools.UnitTesting;
using StarWars.Data;
using System.Linq;

namespace StarWarsMVC.Tests
{
	[TestClass]
	public class UnitTest1
	{
		[TestMethod]
		public void CountMovies()
		{
			//Arrange 
			InMemoryMovieData container = new InMemoryMovieData();

			//Act
			var countMovies = container.movies.Count();
			var countRatings = container.ratings.Count();

			//Assert
			Assert.AreEqual(3, countMovies);
			Assert.AreEqual(3, countRatings);
		}

	}
}
