using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StarWars.Core;
using StarWars.Data;
using StarWarsMVC.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace StarWarsMVC.Controllers
{
	public class MoviesController : Controller
	{
		private readonly IMovieData db;
		private readonly IMoviesService movieService;

		public MoviesController(IMoviesService movieservice, IMovieData db)
		{
			this.movieService = movieservice;
			this.db = db;
		}
        // GET: Movies
        public ActionResult Index()
        {
			var model = db.GetAll();
			//return View(model);
			var filmslist = movieService.GetMoviesFromAPI();
			if(model != null)
			{
				foreach (var movie in model)
				{
					filmslist.Add(movie);
				}
			}
			
			return View(filmslist);
		}

		public ActionResult Details(int id)
		{
			var movie = db.GetMovieById(id);
			if (movie == null)
			{
				var apiMovies = movieService.GetMovieFromAPI(id + 1);
				var ratingForApi = db.GetAllRatings(apiMovies);
				if(ratingForApi.Count > 0)
				{
					apiMovies.AverageRating = ratingForApi.Average(f => (int)f.ScoreSum);
				}
				else if (apiMovies == null)
				{
					return View("NotFound");
				}
				return View(apiMovies);
			}

			var rating = db.GetAllRatings(movie);		

			if (rating.Count > 0)
			{
				movie.AverageRating = rating.Average(r => (int)r.ScoreSum);
			}
			else if (movie == null)
			{
				return View("NotFound");
			}
			return View(movie);
		}

		[HttpGet]
		public ActionResult Create()
		{

			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Movie movie)
		{
			if (ModelState.IsValid)
			{
				var hasValues = db.GetAll();
				if (hasValues.Count() > 0)
				{
					var maxValue = db.GetAll().Max(e => e.EpisodeId);
					movie.EpisodeId = maxValue + 1;
				}
				else
				{
					movie.EpisodeId = 7;
				}
				db.Add(movie);
				TempData["Message"] = "You have created the movie!";
				return RedirectToAction("Details", new { id = movie.MovieId });
			}
			return View();
		}
		[HttpGet]
		public ActionResult Edit(int id)
		{
			var model = db.GetMovieById(id);
			if (model == null)
			{
				return View("NotFound");
			}
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Movie movie)
		{
			if (ModelState.IsValid)
			{
				db.Update(movie);
				TempData["Message"] = "You have saved the movie!";
				return RedirectToAction("Details", new { id = movie.MovieId });
			}
			return View(movie);
		}

		[HttpGet]
		public ActionResult Delete(int id)
		{
			var movie = db.GetMovieById(id);
			if (movie == null)
			{
				return View("NotFound");
			}
			var rating = db.GetAllRatings(movie);
			if (rating.Count > 0)
			{
				movie.AverageRating = rating.Average(r => (int)r.ScoreSum);
			}
			return View(movie);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(int id, IFormCollection form)
		{
			db.Delete(id);
			TempData["Message"] = "You have deleted the movie!";
			return RedirectToAction("Index");
		}

		[HttpGet]
		public ActionResult GetMovieDetails(int id)
		{
			var model = db.GetMovieById(id);
			if (model == null)
			{
				return RedirectToAction("NotFound");
			}
			return RedirectToAction("Details", id);
		}

		public IActionResult Rate(int id, string title)
		{
			var rating = new MovieRating { EpisodeId = id };
			ViewBag.Title = title;
			return View(rating);
		}

		[HttpPost]
		public IActionResult AddRatingToDB(MovieRating grade)
		{
			if (ModelState.IsValid)
			{
				db.Add(grade);
				TempData["Message"] = "You have successfully rated the movie! Episodeid: " + grade.EpisodeId;
				return RedirectToAction("Index");
			}
			return View(grade);
		}
	}
}
