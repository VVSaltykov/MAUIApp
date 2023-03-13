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


			app.MapControllers();

			app.Run();
		}
	}
}