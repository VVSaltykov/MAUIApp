using MAUI_API.Interfaces;
using MAUI_API.Repositories;
using Microsoft.EntityFrameworkCore;

namespace MAUI_API
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			string? connection = builder.Configuration.GetConnectionString("DefaultConnection");

			builder.Services.AddDbContext<ApplicationContext>(options => options.UseNpgsql(connection));

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddTransient<IUser, UserRepository>();
			builder.Services.AddTransient<FileRepository>();
			builder.Services.AddTransient<EventLogRepository>();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
			}
			app.UseDeveloperExceptionPage();
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			c.SwaggerEndpoint("/swagger/v1/swagger.json", "MAUI_API"));

			app.UseHttpsRedirection();

			app.UseAuthorization();
			app.UseAuthentication();


			app.MapControllers();

			app.Run();
		}
	}
}