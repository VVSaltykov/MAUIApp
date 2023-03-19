using MAUI_API.Models;
using Microsoft.EntityFrameworkCore;

namespace MAUI_API
{
	public class ApplicationContext: DbContext
	{
		public DbSet<User> Users { get; set; }
		public DbSet<Models.File> Files { get; set; }
		public DbSet<EventLog> EventLogs { get; set; }

		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}
	}
}
