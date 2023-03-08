using EntityFramework.Models;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.Data
{
	public class MVCContext : DbContext
	{
		public MVCContext(DbContextOptions options) : base(options) {
		}

		// Map following entities into the database
		public DbSet<Entity> Entities { get; set; }
		public DbSet<EntityTPA> EntityTPAs { get; set; }
	}
}
