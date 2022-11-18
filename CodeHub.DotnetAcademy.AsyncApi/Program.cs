using CodeHub.DotnetAcademy.AsyncApi.Interfaces;
using CodeHub.DotnetAcademy.AsyncApi.Repositories;
using CodeHub.DotnetAcademy.AsyncApi.Services;
using Polly.Extensions.Http;
using Polly;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSingleton<IPostService, PostService>();
builder.Services.AddSingleton<IUserService, UserService>();
//// UserService Named HttpClient Registration
//builder.Services.AddHttpClient<IUserService, UserService>(client =>
//{
//	client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/users/");
//})
//	.AddPolicyHandler(GetRetryPolicy())
//	.AddPolicyHandler(GetCircuitBreakerPolicy());

//// PostService Named HttpClient Registration
//builder.Services.AddHttpClient<IPostService, PostService>(client =>
//{
//	client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com/posts/");
//})
//	.AddPolicyHandler(GetRetryPolicy())
//	.AddPolicyHandler(GetCircuitBreakerPolicy());

// MovieRepository Registration
builder.Services.AddScoped<IMovieRepository, MovieRepository>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

//static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
//{
//	return HttpPolicyExtensions
//		.HandleTransientHttpError()
//		.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
//		.WaitAndRetryAsync(6, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
//}

//static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
//{
//	return HttpPolicyExtensions
//		.HandleTransientHttpError()
//		.OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
//		.CircuitBreakerAsync(3, TimeSpan.FromMinutes(2));
//}
