using CodeHub.DotnetAcademy.AsyncApi.Models.ExternalApi;

namespace CodeHub.DotnetAcademy.AsyncApi.Interfaces
{
	public interface IUserService
	{
		List<User> GetAll();
		User? GetById(int id);
	}
}
