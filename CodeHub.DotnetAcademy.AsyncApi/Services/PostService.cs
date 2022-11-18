using CodeHub.DotnetAcademy.AsyncApi.Interfaces;
using CodeHub.DotnetAcademy.AsyncApi.Models.ExternalApi;
using System.Net;
using System.Text.Json;

namespace CodeHub.DotnetAcademy.AsyncApi.Services
{
	public class PostService : IPostService
	{
		private readonly ILogger<PostService> _logger;

		public PostService(ILogger<PostService> logger)
		{
			_logger = logger;
		}

		public List<Post> GetAll()
		{
			var result = new List<Post>();

			try
			{
				// TODO - Change this code in order to use HttpClient instead of WebClient and call GetAsync instead of DownloadString on the client object instance.
				using var client = new WebClient();
				
				var postsJsonStr = client.DownloadString("https://jsonplaceholder.typicode.com/posts");

				var postsFromExternalApi = JsonSerializer.Deserialize<List<Post>>(postsJsonStr);

				if (postsFromExternalApi is not null)
				{
					result.AddRange(postsFromExternalApi);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return result;
		}

		public Post? GetById(int id)
		{
			try
			{
				// TODO - Change this code in order to use HttpClient instead of WebClient and call GetAsync instead of DownloadString on the client object instance.
				using var client = new WebClient();
				
				var post = client.DownloadString("https://jsonplaceholder.typicode.com/posts/" + id.ToString());

				return JsonSerializer.Deserialize<Post>(post);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return null;
		}

		public List<Post> GetPostsForUser(int userID)
		{
			var result = new List<Post>();

			try
			{
				// TODO - Change this code in order to use HttpClient instead of WebClient and call GetAsync instead of DownloadString on the client object instance.
				using var client = new WebClient();
				
				var postsJsonStr = client.DownloadString("https://jsonplaceholder.typicode.com/posts?userId=" + userID.ToString());

				var postsFromExternalApi = JsonSerializer.Deserialize<List<Post>>(postsJsonStr);

				if (postsFromExternalApi is not null)
				{
					result.AddRange(postsFromExternalApi);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return result;
		}
	}
}
