using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using webstore.Data;
using webstore.Helpers;
using webstore.Models;
using webstore.Models.RequestModels;
using webstore.Models.ResponseModels;

namespace webstore.Controllers
{
    public class LoginController : Controller
    {
        private WebstoreContext _db;
        private IHttpContextAccessor _http;

        public LoginController(WebstoreContext db, IHttpContextAccessor http)
        {
            _db = db;
            _http = http;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SignUp([FromBody] SignUpRequestModel request)
        {
            if (ModelState.IsValid)
            {
                PasswordWithSalt pws = HashHelper.Hash(request.password);

                if (_db.Members.Where(member => member.Account == request.account).FirstOrDefault() == null)
                {
                    _db.Members.Add(new Member()
                    {
                        Account = request.account,
                            Password = pws.password,
                            Salt = pws.salt
                    });
                }
                else
                {
                    return Ok(new ResultResponseModel { isSuccess = false, message = "此組帳號已被註冊，請使用其他帳號。" });
                }

                _db.SaveChanges();

                return Ok(new ResultResponseModel { isSuccess = true, message = "signed up." });
            }

            return Ok(new ResultResponseModel { isSuccess = false, message = "帳號與密碼皆須至少8個字以上，且須含有英文字母及數字。" });
        }

        [HttpPost]
        public IActionResult Login([FromBody] LoginRequestModel request)
        {
            if (ModelState.IsValid)
            {
                Member user = _db.Members.Where(member => member.Account == request.account).FirstOrDefault();

                if (user != null)
                {
                    if (user.Password == HashHelper.Hash(request.password, user.Salt))
                    {
                        _http.HttpContext.Session.SetString("account", user.Account);
                        _http.HttpContext.Response.Cookies.Append("account", user.Account);

                        //add cookie-cart to database.
                        if (_http.HttpContext.Request.Cookies.TryGetValue("cart", out string cookieCart))
                        {
                            CartRequestModel[] items = JsonConvert.DeserializeObject<CartRequestModel[]>(cookieCart);
                            user.Carts = _db.Carts.Where(cart => cart.MemberId == user.Id).ToList();
                            user.Carts.ForEach(cart => cart.Product = _db.Products.Where(product => product.Id == cart.ProductId).FirstOrDefault());

                            foreach (var item in items)
                            {
                                if (user.Carts.Exists(cart => cart.ProductId == item.productId))
                                {
                                    Cart cart = user.Carts.Find(cart => cart.ProductId == item.productId);

                                    cart.Product = _db.Products.Where(product => product.Id == cart.ProductId).FirstOrDefault();
                                    if (cart.Quantity + item.quantity > cart.Product.Stock) cart.Quantity = cart.Product.Stock;
                                    else cart.Quantity += item.quantity;
                                }
                                else
                                {
                                    Product product = _db.Products.Where(product => product.Id == item.productId).FirstOrDefault();

                                    _db.Carts.Add(new Cart { MemberId = user.Id, ProductId = item.productId, Quantity = item.quantity > product.Stock ? product.Stock : item.quantity });
                                }
                            }

                            _db.SaveChanges();
                            _http.HttpContext.Response.Cookies.Delete("cart");
                        }

                        return Ok(new ResultResponseModel { isSuccess = true, message = "logged in." });
                    }
                }
            }

            return Ok(new ResultResponseModel { isSuccess = false, message = "您輸入的帳號或密碼有誤，請輸入正確的帳號及密碼。" });
        }

        public IActionResult Logout()
        {
            _http.HttpContext.Session.Clear();
            _http.HttpContext.Response.Cookies.Delete("account");

            return RedirectToAction("Index", "Home");
        }
    }
}