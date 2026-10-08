using System.ComponentModel.DataAnnotations;

namespace thurston_building.Models
{
	public class Tenant
	{
		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string FirstName { get; set; } = string.Empty;

		[Required]
		[StringLength(50)]
		public string LastName { get; set; } = string.Empty;

		[Required]
		[EmailAddress]
		[StringLength(100)]
		public string Email { get; set; } = string.Empty;

		[Phone]
		[StringLength(20)]
		public string? PhoneNumber { get; set; }

		public int RoomId { get; set; }

		public Room? Room { get; set; }

	}
}
