﻿using System.Text.Json.Serialization;

namespace CodeHub.DotnetAcademy.AsyncApi.Models.ExternalApi
{
    public class Post
    {
        [JsonPropertyName("userId")]
		public int UserId { get; set; }

		[JsonPropertyName("id")]
		public int Id { get; set; }
		
		[JsonPropertyName("title")]
		public string Title { get; set; } = default!;
		
		[JsonPropertyName("body")]
		public string Body { get; set; } = default!;
    }
}
