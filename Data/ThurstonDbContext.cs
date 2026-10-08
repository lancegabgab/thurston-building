using Microsoft.EntityFrameworkCore;

namespace thurston_building.Data
{
	public class ThurstonDbContext : DbContext
	{
		public ThurstonDbContext(
			DbContextOptions<ThurstonDbContext> options)
			: base(options)
		{
		}
	}
}
