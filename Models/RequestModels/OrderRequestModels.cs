using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace webstore.Models.RequestModels
{
    public class OrderInsertRequestModel
    {
        [Required]
        public List<ProductItem> products { get; set; }

        [Required]
        [StringLength(20)]
        public string consignee { get; set; }

        [Required]
        [StringLength(20)]
        public string phonenumber { get; set; }

        [Required]
        [StringLength(40)]
        public string address { get; set; }

        [StringLength(50)]
        public string remark { get; set; }

        public List<Cart> carts { get; set; }

        public string errorMessage { get; set; }

        public bool Check(List<Cart> userCarts)
        {
            carts = new List<Cart>();

            foreach (var item in products)
                carts.Add(userCarts.Where(cart => cart.ProductId == item.productId && cart.Quantity == item.quantity).FirstOrDefault());

            if (carts.SequenceEqual(userCarts))
            {
                carts.ForEach(cart =>
                {
                    if (cart.Quantity > cart.Product.Stock)
                        errorMessage = "「" + cart.Product.Name + "」庫存數量只剩下" + cart.Product.Stock + "個，請更改數量後再重新送出訂單。";
                });
            }
            else
                errorMessage = "訂單內容有誤，請再次確認後重新送出。";

            if (String.IsNullOrEmpty(errorMessage))
                return true;
            else
                return false;
        }
    }

    public class ProductItem
    {
        [Required]
        public int productId { get; set; }

        [Required]
        public int quantity { get; set; }
    }
}