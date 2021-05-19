using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MultimediaCenter.Data;
using MultimediaCenter.Models;
using MultimediaCenter.ViewModels;

namespace MultimediaCenter.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MoviesController> _logger;


        public MoviesController(ApplicationDbContext context, ILogger<MoviesController> logger)
        {
            _context = context;
            _logger = logger;
        }

        [HttpGet]
        [Route("filter/{minReleaseYear}")]
        public ActionResult<IEnumerable<Movie>> FilterMovies(int minReleaseYear)
        {
            var query = _context.Movies.Where(m => m.ReleaseYear >= minReleaseYear);
            _logger.LogInformation(query.ToQueryString());
            return query.ToList();

        }

        [HttpGet("{id}/Comments")]
        public ActionResult<IEnumerable<MovieWithCommentsViewModels>> GetCommentsForMovies (int id)
        {
            var query_v1 = _context.Comments.Where(c => c.Movie.Id == id).Include(c => c.Movie).Select(c => new MovieWithCommentsViewModels
            {
              Id = c.Movie.Id,
              Title = c.Movie.Title,
              Description = c.Movie.Description,
              Genre = c.Movie.Genre,
              Duration = c.Movie.Duration,
              ReleaseYear = c.Movie.ReleaseYear,
              Director = c.Movie.Director,
              DateAdded = c.Movie.DateAdded,
              Rating = c.Movie.Rating,
              Watched = c.Movie.Watched,
              Comments = c.Movie.Comments.Select(pc => new CommentViewModel 
              {
                  Id = pc.Id,
                  Content = pc.Content,
                  DateTime = pc.DateTime,
                  Stars = pc.Stars

})
               
            });

            var query_v2 = _context.Movies.Where(m => m.Id == id).Include(m => m.Comments).Select(m => new MovieWithCommentsViewModels
            {

                Id = m.Id,
                Title = m.Title,
                Description = m.Description,
                Genre = m.Genre,
                Duration = m.Duration,
                ReleaseYear = m.ReleaseYear,
                Director = m.Director,
                DateAdded = m.DateAdded,
                Rating = m.Rating,
                Watched = m.Watched,
                Comments = m.Comments.Select(pc => new CommentViewModel
                {
                    Id = pc.Id,
                    Content = pc.Content,
                    DateTime = pc.DateTime,
                    Stars = pc.Stars

                })

            });

            _logger.LogInformation(query_v1.ToQueryString());
            return query_v2.ToList();
        }

        [HttpPost("{id}/Comments")]
        public IActionResult PostCommentForMovie(int id, Comment comment)
        {
            comment.Movie = _context.Movies.Find(id);
            if (comment.Movie == null)
            {
                return NotFound();
            }
            _context.Comments.Add(comment);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        [Route("filter2")]
        public async Task<ActionResult<IEnumerable<Movie>>> FilterByAddedDate (DateTime? fromDate, DateTime? toDate)
        {

            var filteredMovies = await _context.Movies
                .Where(m => m.DateAdded >= fromDate && m.DateAdded <= toDate)
                .OrderByDescending(m => m.ReleaseYear)
                .Select(m => m)
                .ToListAsync();

            return Ok(filteredMovies);
        }

        // GET: api/Movies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Movie>>> GetMovies()
        {
            return await _context.Movies.ToListAsync();
        }

        // GET: api/Movies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<MovieViewModel>> GetMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            var movieViewModel = new MovieViewModel
            {
                Id = movie.Id,
                Title = movie.Title,
                Description = movie.Description,
                Genre = movie.Genre,
                Duration = movie.Duration,
                ReleaseYear = movie.ReleaseYear,
                Director = movie.Director,
                DateAdded = movie.DateAdded,
                Rating = movie.Rating,
                Watched = movie.Watched

            };

            if (movie == null)
            {
                return NotFound();
            }

            return movieViewModel;
        }

        // PUT: api/Movies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMovie(int id, Movie movie)
        {
            if (id != movie.Id)
            {
                return BadRequest();
            }

            _context.Entry(movie).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Movies
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Movie>> PostMovie(Movie movie)
        {
            _context.Movies.Add(movie);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMovie", new { id = movie.Id }, movie);
        }

        // DELETE: api/Movies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var movie = await _context.Movies.FindAsync(id);
            if (movie == null)
            {
                return NotFound();
            }

            _context.Movies.Remove(movie);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MovieExists(int id)
        {
            return _context.Movies.Any(e => e.Id == id);
        }
    }
}
