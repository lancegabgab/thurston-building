using System.ComponentModel.DataAnnotations;

namespace thurston_building.Models
{
	public class Room
	{
		public int Id { get; set; }

		[Required]
		[StringLength(20)]
		public string RoomNumber { get; set; } = string.Empty;

		public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();
	}
}
