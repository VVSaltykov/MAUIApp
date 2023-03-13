using Microsoft.EntityFrameworkCore;

namespace MAUI_API
{
	public class ApplicationContext: DbContext
	{
		public ApplicationContext(DbContextOptions<ApplicationContext> options)
			: base(options)
		{
			Database.EnsureCreated();
		}
	}
}
