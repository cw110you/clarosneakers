using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webstore.Models
{
    public class Member
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Account { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Salt { get; set; }

        public string Username { get; set; }

        public string Address { get; set; }

        public string Phone { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public List<Cart> Carts { get; set; } //refer to Cart
        public List<Order> Orders { get; set; } //refer to Orders
    }
}