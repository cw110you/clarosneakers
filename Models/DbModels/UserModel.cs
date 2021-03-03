using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webstore.Models
{
    public class User
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UserId { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }

        public string Telphone { get; set; }

        public string Cellphone { get; set; }

        public string Zipcode { get; set; }
        
        public string Address { get; set; }
    }

}
