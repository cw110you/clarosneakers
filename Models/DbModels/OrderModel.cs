using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webstore.Models
{
    public class Order
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public long OrderNumber { get; set; }

        [Required]
        public int MemberId { get; set; }

        [Required]
        public string Consignee { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        public string Remark { get; set; }

        public Member Member { get; set; } //refer to Member
        public List<OrderItem> OrderItems { get; set; } //refer to OrderItem
        public List<OrderStatus> OrderStatuses { get; set; } //refer to OrderStatus
    }
}