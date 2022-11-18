namespace CodeHub.DotnetAcademy.AsyncApi.Entities
{
	public class Movie
	{
		public int Id { get; set; }
		public string Name { get; set; } = default!;
		public string Description { get; set; } = default!;
		public int AppropriateAbove { get; set; }
		public float ImdbRating { get; set; }
	}
}
