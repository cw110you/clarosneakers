using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace webstore.Models
{
    public class OrderStatus
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int StateId { get; set; }

        public DateTime Date { get; set; }

        public Order Order { get; set; }    //refer to Order

        public State State { get; set; }    //refer to State
    }
}