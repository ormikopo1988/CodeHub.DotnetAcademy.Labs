using System.Text.Json.Serialization;

namespace CodeHub.DotnetAcademy.AsyncApi.Models.ExternalApi
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; } = default!;

		[JsonPropertyName("username")]
		public string Username { get; set; } = default!;

		[JsonPropertyName("email")]
		public string Email { get; set; } = default!;

		[JsonPropertyName("phone")]
		public string Phone { get; set; } = default!;

		[JsonPropertyName("website")]
		public string Website { get; set; } = default!;

        public List<Post> Posts { get; set; } = new List<Post>();
    }
}
