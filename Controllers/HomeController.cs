using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using webstore.Data;
using webstore.Models;
using webstore.Models.ViewModels;

namespace webstore.Controllers
{
    public class HomeController : Controller
    {
        private WebstoreContext _db;
        public HomeController(WebstoreContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            List<Product> products = _db.Products.OrderBy(product => product.Stock).Where(product => product.ReleaseDate.CompareTo(DateTime.Now) <= 0).Take(15).ToList();

            products.ForEach(product => product.Images = _db.Images.Where(image => image.ProductId == product.Id).OrderBy(image => image.Order).Take(1).ToList());
            model.products = products;

            model.advertisements = _db.Advertisements.Where(advt => advt.StartDate.CompareTo(DateTime.Now) <= 0 && advt.EndDate.CompareTo(DateTime.Now) > 0).OrderBy(advt => advt.StartDate).ToList();

            model.categories = _db.Categories.OrderBy(category => category.Id).ToList();

            return View(model: model);
        }
    }
}