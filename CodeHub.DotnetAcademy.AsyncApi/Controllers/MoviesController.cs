using CodeHub.DotnetAcademy.AsyncApi.Entities;
using CodeHub.DotnetAcademy.AsyncApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.DotnetAcademy.AsyncApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class MoviesController : ControllerBase
	{
		private readonly ILogger<MoviesController> _logger;
		private readonly IMovieRepository _movieRepository;

		public MoviesController(ILogger<MoviesController> logger, IMovieRepository movieRepository)
		{
			_logger = logger;
			_movieRepository = movieRepository;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			try
			{
				var movies = _movieRepository.GetAll();

				return new OkObjectResult(movies);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet("{id}")]
		public IActionResult GetByID(int id)
		{
			try
			{
				if (id <= 0)
				{
					return BadRequest();
				}

				var movie = _movieRepository.GetById(id);

				if (movie == null)
				{
					return NotFound();
				}

				return new OkObjectResult(movie);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpPost]
		public IActionResult Post(Movie movie)
		{
			try
			{
				if (!ModelState.IsValid)
				{
					return BadRequest();
				}

				var moviesInserted = _movieRepository.Save(movie);

				if (moviesInserted == 1)
				{
					return NoContent();
				}
				else
				{
					return new StatusCodeResult(StatusCodes.Status500InternalServerError);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
