using CodeHub.DotnetAcademy.AsyncApi.Entities;
using CodeHub.DotnetAcademy.AsyncApi.Interfaces;
using System.Data.SqlClient;
using System.Data;
using Dapper;

namespace CodeHub.DotnetAcademy.AsyncApi.Repositories
{
	// For testing this when ready:
	// - Open SQL Server Object Explorer View in your VS2022 instance
	// - Add (localdb)\MSSQLLocalDB instance to the view
	// - Create a new database inside this local db instance called MockDb
	// - Right-click on MockDb and run the following query:
	//CREATE TABLE[dbo].[Movie]
	//(
	//    [Id] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	//    [Name] NCHAR(50) NOT NULL,
	//    [Description] NVARCHAR(MAX) NOT NULL,
	//    [AppropriateAbove] INT NOT NULL,
	//    [ImdbRating] FLOAT(53) NULL
	//);
	public class MovieRepository : IMovieRepository
	{
		private readonly ILogger<MovieRepository> _logger;
		private readonly string _connectionString;

		public MovieRepository(ILogger<MovieRepository> logger, IConfiguration configuration)
		{
			_logger = logger;
			_connectionString = configuration.GetValue<string>("ConnectionStrings:Default");
		}

		private IDbConnection GetConnection()
		{
			var connection = new SqlConnection(_connectionString);

			connection.Open();

			return connection;
		}

		public List<Movie> GetAll()
		{
			try
			{
				using var connection = GetConnection();

				var movies = connection.Query<Movie>("SELECT * FROM [dbo].[Movie] WITH (NOLOCK)");

				return movies.ToList();
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				throw;
			}
		}

		public Movie? GetById(int id)
		{
			try
			{
				using var connection = GetConnection();
				
				return connection.QueryFirstOrDefault<Movie>("SELECT * FROM [dbo].[Movie] WITH (NOLOCK) WHERE Id=@Id", new { Id = id });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				throw;
			}
		}

		public int Save(Movie movie)
		{
			try
			{
				var sql = "INSERT INTO [dbo].[Movie] (Name, Description, AppropriateAbove, ImdbRating) Values (@Name, @Description, @AppropriateAbove, @ImdbRating);";

				using var connection = GetConnection();
				
				return connection.Execute(sql, new { movie.Name, movie.Description, movie.AppropriateAbove, movie.ImdbRating });
			}
			catch (Exception ex)
			{
				_logger.LogError(ex.Message);

				throw;
			}
		}
	}
}
