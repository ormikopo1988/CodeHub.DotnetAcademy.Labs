using CodeHub.DotnetAcademy.AsyncApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.DotnetAcademy.AsyncApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class PostsController : ControllerBase
	{
		private readonly ILogger<PostsController> _logger;
		private readonly IPostService _postService;

		public PostsController(ILogger<PostsController> logger, IPostService postService)
		{
			_logger = logger;
			_postService = postService;
		}

		[HttpGet]
		public IActionResult GetAll()
		{
			try
			{
				var posts = _postService.GetAll();

				return new OkObjectResult(posts);
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

				var post = _postService.GetById(id);

				if (post == null)
				{
					return NotFound();
				}

				return new OkObjectResult(post);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
