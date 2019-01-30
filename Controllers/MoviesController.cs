using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Vidly.Models;
using Vidly.ViewModel;

namespace Vidly.Controllers
{
    public class MoviesController : Controller
    {
        private ApplicationDbContext _context;
        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize(Roles = RoleName.CanManageMovies)]
        public ActionResult New ()
        {
            var genres = _context.Genres.ToList();

            var viewModel = new NewMovieViewModel
            {
                
                Genres = genres
            };
            return View("MovieForm", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save (Movie movie)
        {
            if(!ModelState.IsValid)
            {
                var viewModel = new NewMovieViewModel(movie)
                {
                    
                    Genres = _context.Genres.ToList()
                };

                return View("MovieForm", viewModel);
            }

            if (movie.Id == 0)
            {

                movie.DateAdded = DateTime.Now;
                _context.Movies.Add(movie);
            }
            else
            {
                var movieInDb = _context.Movies.Single(m => m.Id == movie.Id);

                movieInDb.Name = movie.Name;
                movieInDb.ReleaseDate = movie.ReleaseDate;
                movieInDb.NumberInStock = movie.NumberInStock;
                movieInDb.GenreId = movie.GenreId;
            }
            try
            {
                _context.SaveChanges();

            }
            catch (Exception)
            {
                return Content("Something went wrong");
            }

            return RedirectToAction("Index", "Movies");
        }

        // GET: Movies
        public ActionResult Index()
        {
            if (User.IsInRole(RoleName.CanManageMovies))
                return View("List");

            return View("ReadOnlyList");
        }

        public ActionResult Details (int? Id)
        {
            if (Id == null)
                return new HttpNotFoundResult();

            Movie movie = _context.Movies.Include(c => c.Genre).Single(m => m.Id == Id);
           

            return View(movie);
        }

        public ActionResult Edit(int? Id)
        {
            if (Id == null)
                return new HttpNotFoundResult();

            Movie movie = _context.Movies.Include(c => c.Genre).Single(m => m.Id == Id);
            var genres = _context.Genres.ToList();

            NewMovieViewModel viewModel = new NewMovieViewModel(movie)
            {
                Genres = genres
            };

            return View("MovieForm", viewModel);
        }

        [Route("movies/released/{year}/{month:regex(\\d{2}):range(1, 12)}")]
        public ActionResult ByReleaseDate(int year, int month)
        {
            return Content("Year: " + year + "/ Month" + month);
        }
    }
}