using System.ComponentModel.DataAnnotations;

namespace EntityFramework.Models
{
	public class Entity
	{
		[Key]
		public long EntityID { get; set; }

		[Required, MaxLength(100)]
		public string Name { get; set; }

		public List<EntityTPA> EntityTPAs { get; set; } = new List<EntityTPA>();
	}
}
