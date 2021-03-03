using System.ComponentModel.DataAnnotations;

namespace webstore.Models.RequestModels
{
    public class CartRequestModel
    {
        [Required]
        public int productId { get; set; }

        [Required]
        public int quantity { get; set; }
    }

    public class CartDeleteRequestModel
    {
        [Required]
        public int productId { get; set; }
    }
}