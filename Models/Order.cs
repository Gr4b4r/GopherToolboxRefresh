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
        public int QuestId { get; set; }
        [ForeignKey("QuestId")]
        public Quest Quest { get; set; }

        [Required]
		public string UserId { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime QuestDateStart { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [EndDateAfterStartDate("QuestDateStart")]
        public DateTime QuestDateEnd { get; set; }

        public User? User { get; set; }  
        public bool CancellationRequested { get; set; }
        public CancelRequest? CancelRequest { get; set; }
        public bool IsCanceled { get; set; }
	}
}
