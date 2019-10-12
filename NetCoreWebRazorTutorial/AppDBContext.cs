using Microsoft.EntityFrameworkCore;

namespace NetCoreWebRazorTutorial
{
	public class AppDBContext : DbContext
	{
		public AppDBContext(DbContextOptions options) : base(options)
		{

		}

		public DbSet<Customer> Customers { get; set; }
	}
}