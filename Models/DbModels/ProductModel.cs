using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webstore.Models
{
    public class Product
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Specification { get; set; }

        public string Description { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Stock { get; set; }

        public DateTime ReleaseDate { get; set; }

        public List<Image> Images { get; set; } //refer to Image
        public List<Catalog> Catalogs { get; set; } //refer to Catalog
        public List<Cart> Carts { get; set; } //refer to Cart
        public List<OrderItem> OrderItems { get; set; } //refer to OrderItem
    }
}