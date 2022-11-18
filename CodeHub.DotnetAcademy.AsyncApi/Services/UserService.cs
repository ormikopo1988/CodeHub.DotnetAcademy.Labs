using CodeHub.DotnetAcademy.AsyncApi.Interfaces;
using CodeHub.DotnetAcademy.AsyncApi.Models.ExternalApi;
using System.Net;
using System.Text.Json;

namespace CodeHub.DotnetAcademy.AsyncApi.Services
{
	public class UserService : IUserService
	{
		private readonly ILogger<UserService> _logger;

		public UserService(ILogger<UserService> logger)
		{
			_logger = logger;
		}

		public List<User> GetAll()
		{
			var result = new List<User>();

			try
			{
				// TODO - Change this code in order to use HttpClient instead of WebClient and call GetAsync instead of DownloadString on the client object instance.
				using var client = new WebClient();
				
				var usersJsonStr = client.DownloadString("https://jsonplaceholder.typicode.com/users");

				var usersFromExternalApi = JsonSerializer.Deserialize<List<User>>(usersJsonStr);

				if (usersFromExternalApi is not null)
				{
					result.AddRange(usersFromExternalApi);
				}
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return result;
		}

		public User? GetById(int id)
		{
			try
			{
				// TODO - Change this code in order to use HttpClient instead of WebClient and call GetAsync instead of DownloadString on the client object instance.
				using var client = new WebClient();
				
				var user = client.DownloadString("https://jsonplaceholder.typicode.com/users/" + id.ToString());

				return JsonSerializer.Deserialize<User>(user);
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);
			}

			return null;
		}
	}
}
