using CodeHub.DotnetAcademy.AsyncApi.Models.ExternalApi;

namespace CodeHub.DotnetAcademy.AsyncApi.Interfaces
{
	public interface IPostService
	{
		List<Post> GetAll();
		Post? GetById(int id);
		List<Post> GetPostsForUser(int userID);
	}
}
