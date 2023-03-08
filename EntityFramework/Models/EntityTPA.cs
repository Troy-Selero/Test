using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Models
{
	public class EntityTPA
	{
		[Key]
		public long EntityTPAID { get; set; }

		[Required, MaxLength(50)]
		public string Name { get; set; }

		public Entity Entity { get; set; }
	}
}
