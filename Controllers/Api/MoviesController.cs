using AutoMapper;
using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Vidly.Dtos;
using Vidly.Models;

namespace Vidly.Controllers.Api
{
    public class MoviesController : ApiController
    {
        private ApplicationDbContext _context;

        public MoviesController()
        {
            _context = new ApplicationDbContext();
        }

        public IEnumerable<MovieDto> GetMovies(string query = null)
        {
            var moviesQuery = _context.Movies
                .Include(m => m.Genre)
                .Where(m => m.NumberAvailable > 0);

            if (!String.IsNullOrWhiteSpace(query))
                moviesQuery = moviesQuery.Where(m => m.Name.Contains(query));

            return moviesQuery
                .ToList()
                .Select(Mapper.Map<Movie, MovieDto>);
        }

        // GET /api/movies/1
        public IHttpActionResult GetMovie (int Id)
        {
            var movie = _context.Movies.SingleOrDefault(m => m.Id == Id);

            if (movie == null)
                return NotFound();

            return Ok(movie);
        }

        // CREATE /api/movies
        [HttpPost]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult CreateMovie (MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newMovie = Mapper.Map<MovieDto, Movie>(movieDto);

            _context.Movies.Add(newMovie);
            _context.SaveChanges();

            movieDto.Id = newMovie.Id;

            return Created(new Uri(Request.RequestUri + "/" + movieDto.Id), movieDto);
        }

        // UPDATE /api/movies/1
        [HttpPut]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public IHttpActionResult UpdateMovie (int Id, MovieDto movieDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var newMovie = _context.Movies.SingleOrDefault(m => m.Id == Id);

            if (newMovie == null)
                return NotFound();

            newMovie.Id = Id;
            Mapper.Map(movieDto, newMovie);

            _context.SaveChanges();

            return Ok();
        }

        // DELETE /api/movies/1
        [HttpDelete]
        [Authorize(Roles =RoleName.CanManageMovies)]
        public IHttpActionResult DeleteMovie (int Id)
        {
            var movieInDb = _context.Movies.SingleOrDefault(m => m.Id == Id);

            if (movieInDb == null)
                return NotFound();

            _context.Movies.Remove(movieInDb);
            _context.SaveChanges();

            return Ok();
        }
    }
}
