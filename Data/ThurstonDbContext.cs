using Microsoft.EntityFrameworkCore;
using thurston_building.Models;

namespace thurston_building.Data
{
	public class ThurstonDbContext : DbContext
	{
		public ThurstonDbContext(
			DbContextOptions<ThurstonDbContext> options)
			: base(options)
		{
		}

		public DbSet<Room> Rooms { get; set; }
		public DbSet<Tenant> Tenants { get; set; }
	}
}
