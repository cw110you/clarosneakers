using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webstore.Data;
using webstore.Models;
using webstore.Models.RequestModels;
using webstore.Models.ResponseModels;

namespace webstore.Controllers
{
    public class CartController : Controller
    {
        private WebstoreContext _db;
        private IHttpContextAccessor _http;
        public CartController(WebstoreContext db, IHttpContextAccessor http)
        {
            _db = db;
            _http = http;
        }


        public IActionResult Index()
        {
            string account = _http.HttpContext.Session.GetString("account");
            Member user = _db.Members.Where(member => member.Account == account).FirstOrDefault();

            if (string.IsNullOrEmpty(account) || user == null)
            {
                if (_http.HttpContext.Request.Cookies.TryGetValue("cart", out string cookieCart))
                {
                    CartRequestModel[] items = JsonConvert.DeserializeObject<CartRequestModel[]>(cookieCart);
                    List<Cart> carts = new List<Cart>();

                    foreach (var item in items)
                    {
                        Product product = _db.Products.Where(product => product.Id == item.productId).FirstOrDefault();

                        product.Images = _db.Images.Where(image => image.ProductId == product.Id).OrderBy(image => image.Order).Take(1).ToList();

                        carts.Add(new Cart { MemberId = 0, ProductId = product.Id, Quantity = item.quantity > product.Stock ? product.Stock : item.quantity, Product = product });
                    }

                    return View(carts);
                }
                else
                    return View(new List<Cart>());
            }
            else
            {
                user.Carts = _db.Carts.Where(cart => cart.MemberId == user.Id).ToList();
                user.Carts.ForEach(cart =>
                {
                    cart.Product = _db.Products.Where(product => product.Id == cart.ProductId).FirstOrDefault();
                    cart.Product.Images = _db.Images.Where(image => image.ProductId == cart.Product.Id).OrderBy(image => image.Order).Take(1).ToList();
                });

                return View(user.Carts);
            }
        }

        [HttpPost]
        public IActionResult Add([FromBody] CartRequestModel request)
        {
            string account = _http.HttpContext.Session.GetString("account");
            Member user = _db.Members.Where(member => member.Account == account).FirstOrDefault();

            if (string.IsNullOrEmpty(account) || user == null) return Ok(new ResultResponseModel { isSuccess = false, message = "Please login to continue.", redirectTo = "/login" });

            if (ModelState.IsValid)
            {
                user.Carts = _db.Carts.Where(cart => cart.MemberId == user.Id).ToList();

                if (user.Carts.Exists(cart => cart.ProductId == request.productId))
                {
                    Cart cart = user.Carts.Find(cart => cart.ProductId == request.productId);

                    cart.Product = _db.Products.Where(product => product.Id == cart.ProductId).FirstOrDefault();

                    if (cart.Quantity + request.quantity > cart.Product.Stock) cart.Quantity = cart.Product.Stock;
                    else cart.Quantity += request.quantity;
                }
                else
                {
                    Product product = _db.Products.Where(product => product.Id == request.productId).FirstOrDefault();

                    _db.Carts.Add(new Cart { MemberId = user.Id, ProductId = request.productId, Quantity = request.quantity > product.Stock ? product.Stock : request.quantity });
                }

                _db.SaveChanges();

                return Ok(new ResultResponseModel { isSuccess = true, message = "Added to cart." });
            }

            return Ok(new ResultResponseModel { isSuccess = false, message = "Failed to add product to cart." });
        }

        [HttpPost]
        public IActionResult Alter([FromBody] CartRequestModel request)
        {
            string account = _http.HttpContext.Session.GetString("account");
            Member user = _db.Members.Where(member => member.Account == account).FirstOrDefault();

            if (string.IsNullOrEmpty(account) || user == null) return Ok(new ResultResponseModel { isSuccess = false, message = "Please login to continue.", redirectTo = "/login" });

            if (ModelState.IsValid)
            {
                user.Carts = _db.Carts.Where(cart => cart.MemberId == user.Id).ToList();

                if (user.Carts.Exists(cart => cart.ProductId == request.productId))
                {
                    Cart cart = user.Carts.Find(cart => cart.ProductId == request.productId);

                    cart.Product = _db.Products.Where(product => product.Id == cart.ProductId).FirstOrDefault();

                    if (request.quantity > cart.Product.Stock) cart.Quantity = cart.Product.Stock;
                    else cart.Quantity = request.quantity;

                    _db.SaveChanges();

                    return Ok(new CartAlertResponseModel { isSuccess = true, message = "Altered quantity.", productId = request.productId });
                }
            }

            return Ok(new ResultResponseModel { isSuccess = false, message = "Failed to alter quantity." });
        }

        [HttpPost]
        public IActionResult Delete([FromBody] CartDeleteRequestModel[] request)
        {
            string account = _http.HttpContext.Session.GetString("account");
            Member user = _db.Members.Where(member => member.Account == account).FirstOrDefault();

            if (string.IsNullOrEmpty(account) || user == null) return Ok(new ResultResponseModel { isSuccess = false, message = "Please login to continue.", redirectTo = "/login" });

            if (ModelState.IsValid)
            {
                user.Carts = _db.Carts.Where(cart => cart.MemberId == user.Id).ToList();

                _db.Carts.RemoveRange(user.Carts.Where(cart => Array.Exists(request, cartDel => (cart.ProductId == cartDel.productId))).ToList());

                _db.SaveChanges();

                return Ok(new CartDeleteResponseModel { isSuccess = true, message = "Delete cart.", productIds = request.Select(req => req.productId).ToArray() });
            }

            return Ok(new ResultResponseModel { isSuccess = false, message = "Failed to delete cart." });
        }

        public IActionResult CheckOut()
        {
            string account = _http.HttpContext.Session.GetString("account");
            Member user = _db.Members.Where(member => member.Account == account).FirstOrDefault();

            if (string.IsNullOrEmpty(account) || user == null) return RedirectToAction("Index", "Login");

            user.Carts = _db.Carts.Where(cart => cart.MemberId == user.Id).ToList();
            user.Carts.ForEach(cart =>
            {
                cart.Product = _db.Products.Where(product => product.Id == cart.ProductId).FirstOrDefault();
                cart.Product.Images = _db.Images.Where(image => image.ProductId == cart.Product.Id).OrderBy(image => image.Order).Take(1).ToList();
            });

            return View(user);
        }
    }
}