using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webstore.Models
{
    public class State
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<OrderStatus> OrderStatuses { get; set; }
    }

    public enum StateIDs
    {
        Placed = 1,
        Processing = 2,
        Delivered = 3,
        Completed = 4,

        Canceled = 0
    }
}