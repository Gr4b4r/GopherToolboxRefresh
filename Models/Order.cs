using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GopherToolboxRefresh.Models
{
	public class Order
	{
		[Key]
		public int Id { get; set; }

		[Required]
		public string UserId { get; set; } = string.Empty;
		public User? User { get; set; }  // Allow null to avoid warning

		[ForeignKey("Quest")]
		public int QuestId { get; set; }
		public Quest Quest { get; set; } = null!;

		[Required]
		[DataType(DataType.Date)]
		public DateTime QuestDateStart { get; set; }

		[Required]
		[DataType(DataType.Date)]
		[EndDateAfterStartDate("QuestDateStart")]
		public DateTime QuestDateEnd { get; set; }

		[Required]
		public DateTime OrderDate { get; set; }

		public bool CancellationRequested { get; set; }
		public bool IsCanceled { get; set; }
	}
}
