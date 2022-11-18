using CodeHub.DotnetAcademy.AsyncApi.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CodeHub.DotnetAcademy.AsyncApi.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class UsersController : ControllerBase
	{
		private readonly ILogger<UsersController> _logger;
		private readonly IUserService _userService;
		private readonly IPostService _postService;

		public UsersController(ILogger<UsersController> logger, IUserService userService, IPostService postService)
		{
			_logger = logger;
			_userService = userService;
			_postService = postService;
		}

		[HttpGet]
		public ActionResult GetAll()
		{
			try
			{
				var users = _userService.GetAll();

				return new OkObjectResult(users);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}

		[HttpGet("{id}")]
		public ActionResult GetByID(int id)
		{
			try
			{
				if (id <= 0)
				{
					return BadRequest();
				}

				var user = _userService.GetById(id);

				if (user == null)
				{
					return NotFound();
				}

				user.Posts = _postService.GetPostsForUser(user.Id);

				return new OkObjectResult(user);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				return new StatusCodeResult(StatusCodes.Status500InternalServerError);
			}
		}
	}
}
