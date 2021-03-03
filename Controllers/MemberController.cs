using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Linq;
using webstore.Data;
using webstore.Models;
using webstore.Models.RequestModels;
using webstore.Models.ResponseModels;
using webstore.Helpers;

namespace webstore.Controllers
{
    public class MemberController : Controller
    {
        private WebstoreContext _db;
        private IHttpContextAccessor _http;

        public MemberController(WebstoreContext db, IHttpContextAccessor http)
        {
            _db = db;
            _http = http;
        }

        public IActionResult Index()
        {
            string account = _http.HttpContext.Session.GetString("account");

            if (string.IsNullOrEmpty(account)) return RedirectToAction("Index", "Login");

            Member user = _db.Members.Where(member => member.Account == account).FirstOrDefault();

            return View(model: user);
        }

        [HttpPost]
        public IActionResult AlterProfile([FromBody] AlertProfileRequestModel request)
        {
            string account = _http.HttpContext.Session.GetString("account");

            if (string.IsNullOrEmpty(account) || account != request.account) return Ok(new ResultResponseModel { isSuccess = false, message = "登入時效已過，請重新登入。", redirectTo = "/login" });

            if (ModelState.IsValid)
            {
                Member user = _db.Members.Where(member => member.Account == account).FirstOrDefault();

                user.Username = request.username;
                user.Address = request.address;
                user.Phone = request.phone;
                user.Email = request.email;

                _db.SaveChanges();
            }

            return Ok(new ResultResponseModel { isSuccess = true, message = "資料已更新" });
        }

        public IActionResult Password()
        {
            string account = _http.HttpContext.Session.GetString("account");

            if (string.IsNullOrEmpty(account)) return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public IActionResult ChangePassword([FromBody] ChangePassword request)
        {
            string account = _http.HttpContext.Session.GetString("account");

            if (string.IsNullOrEmpty(account)) return Ok(new ResultResponseModel { isSuccess = false, message = "登入時效已過，請重新登入。", redirectTo = "/login" });

            if (ModelState.IsValid)
            {
                Member user = _db.Members.Where(member => member.Account == account).FirstOrDefault();

                if (HashHelper.Hash(request.currentPassword, user.Salt) == user.Password)
                {
                    PasswordWithSalt pws = HashHelper.Hash(request.newPassword);

                    user.Password = pws.password;
                    user.Salt = pws.salt;
                    _db.SaveChanges();
                }
                else
                {
                    return Ok(new ResultResponseModel { isSuccess = false, message = "輸入的密碼有誤，請重新輸入。" });
                }
            }

            return Ok(new ResultResponseModel { isSuccess = true, message = "密碼已更新" });
        }
    }
}