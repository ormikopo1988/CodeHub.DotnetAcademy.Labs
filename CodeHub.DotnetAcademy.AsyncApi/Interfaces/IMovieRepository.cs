using CodeHub.DotnetAcademy.AsyncApi.Entities;

namespace CodeHub.DotnetAcademy.AsyncApi.Interfaces
{
	public interface IMovieRepository
	{
		List<Movie> GetAll();
		Movie? GetById(int id);
		int Save(Movie movie);
	}
}
