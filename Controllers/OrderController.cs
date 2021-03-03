using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webstore.Data;
using webstore.Models;
using webstore.Models.RequestModels;

namespace webstore.Controllers
{
    public class OrderController : Controller
    {
        private WebstoreContext _db;
        private IHttpContextAccessor _http;

        public OrderController(WebstoreContext db, IHttpContextAccessor http)
        {
            _db = db;
            _http = http;
        }

        public IActionResult Index()
        {
            string account = _http.HttpContext.Session.GetString("account");
            Member user = _db.Members.Where(member => member.Account == account).FirstOrDefault();

            if (string.IsNullOrEmpty(account) || user == null)
                return RedirectToAction("Index", "Login");
            else
            {
                user.Orders = _db.Orders.Where(order => order.MemberId == user.Id).ToList();
                user.Orders.ForEach(order =>
                {
                    order.OrderItems = _db.OrderItems.Where(oi => oi.OrderId == order.Id).ToList();
                    order.OrderItems.ForEach(orderItem =>
                    {
                        orderItem.Product = _db.Products.Where(prd => prd.Id == orderItem.ProductId).FirstOrDefault();
                        orderItem.Product.Images = _db.Images.Where(img => img.ProductId == orderItem.ProductId).OrderBy(img => img.Order).Take(1).ToList();
                    });
                    order.OrderStatuses = _db.OrderStatues.Where(os => os.OrderId == order.Id).OrderBy(os => os.Date).ToList();
                    order.OrderStatuses.ForEach(orderStatus => { orderStatus.State = _db.States.Where(s => s.Id == orderStatus.StateId).FirstOrDefault(); });
                });
            }

            return View(user.Orders);
        }

        [HttpPost]
        public IActionResult Insert([FromForm] OrderInsertRequestModel request)
        {
            string account = _http.HttpContext.Session.GetString("account");
            Member user = _db.Members.Where(member => member.Account == account).FirstOrDefault();

            if (string.IsNullOrEmpty(account) || user == null) return RedirectToAction("Index", "Login");

            if (ModelState.IsValid)
            {
                user.Carts = _db.Carts.Where(cart => cart.MemberId == user.Id).ToList();
                user.Carts.ForEach(cart =>
                {
                    cart.Product = _db.Products.Where(product => product.Id == cart.ProductId).FirstOrDefault();
                    cart.Product.Images = _db.Images.Where(image => image.ProductId == cart.Product.Id).OrderBy(image => image.Order).Take(1).ToList();
                });

                if (request.Check(user.Carts))
                {
                    string dateToday = DateTime.Now.ToString("yyyyMMdd");
                    long newOrderNumber = _db.Orders.Where(order => order.OrderNumber.ToString().Substring(0, 8) == dateToday).OrderByDescending(order => order.OrderNumber).Select(order => order.OrderNumber).FirstOrDefault();

                    if (newOrderNumber == 0)
                        newOrderNumber = long.Parse(dateToday + "00001");
                    else
                        newOrderNumber++;

                    _db.Orders.Add(new Order
                    {
                        OrderNumber = newOrderNumber,
                            MemberId = user.Id,
                            Consignee = request.consignee,
                            PhoneNumber = request.phonenumber,
                            Address = request.address,
                            Remark = request.remark
                    });
                    _db.SaveChanges();

                    Order currentOrder = _db.Orders.Where(order => order.OrderNumber == newOrderNumber).FirstOrDefault();
                    List<OrderItem> newOrderItems = new List<OrderItem>();

                    user.Carts.ForEach(cart =>
                    {
                        _db.OrderItems.Add(new OrderItem
                        {
                            OrderId = currentOrder.Id,
                                ProductId = cart.ProductId,
                                Quantity = cart.Quantity,
                                Price = cart.Product.Price
                        });

                        _db.Products.Where(product => product.Id == cart.ProductId).FirstOrDefault().Stock -= cart.Quantity;
                    });

                    _db.Carts.RemoveRange(user.Carts);
                    _db.OrderStatues.Add(new OrderStatus
                    {
                        OrderId = currentOrder.Id,
                            StateId = (int) StateIDs.Placed,
                            Date = DateTime.Now
                    });

                    _db.SaveChanges();

                    currentOrder.Member = user;
                    currentOrder.OrderItems = _db.OrderItems.Where(orderItem => orderItem.OrderId == currentOrder.Id).ToList();
                    currentOrder.OrderItems.ForEach(orderItem => { orderItem.Product = _db.Products.Where(product => product.Id == orderItem.ProductId).FirstOrDefault(); });
                    currentOrder.OrderStatuses = _db.OrderStatues.Where(orderStatus => orderStatus.OrderId == currentOrder.Id).ToList();
                    currentOrder.OrderStatuses.ForEach(orderStatus => { orderStatus.State = _db.States.Where(state => state.Id == orderStatus.StateId).FirstOrDefault(); });

                    return View(currentOrder);
                }
                else
                    ViewBag.errorMessage = request.errorMessage;
            }

            return View();
        }
    }
}