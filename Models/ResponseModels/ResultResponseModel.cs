using System.ComponentModel.DataAnnotations;

namespace webstore.Models.ResponseModels
{
    public class ResultResponseModel
    {
        public bool isSuccess { get; set; }

        public string message { get; set; }

        public string redirectTo { get; set; }
    }

    public class CartAlertResponseModel : ResultResponseModel
    {
        public int productId { get; set; }
    }

    public class CartDeleteResponseModel : ResultResponseModel
    {
        public int[] productIds { get; set; }
    }
}