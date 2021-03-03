using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using webstore.Data;
using webstore.Models;
using webstore.Models.ViewModels;
using System.Linq;
using System;

namespace webstore.Controllers
{
    public class ProductController : Controller
    {
        private WebstoreContext _db;

        public ProductController(WebstoreContext db)
        {
            _db = db;
        }

        public IActionResult Search([FromQuery] ProductSearchViewModel query)
        {
            if (ModelState.IsValid)
            {
                query.ApplyQuery(_db);

                return View(model: query);
            }
            else
            {
                query.products = new List<Product>();

                return View(model: query);
            }
        }

        public IActionResult Detail(int id)
        {
            Product product = _db.Products.Where(product => product.Id == id && product.ReleaseDate.CompareTo(DateTime.Now) <= 0).FirstOrDefault();

            if (!(product == null))
            {
                product.Images = _db.Images.Where(img => img.ProductId == product.Id).OrderBy(img => img.Order).ToList();
                product.Catalogs = _db.Catalogs.Where(cat => cat.ProductId == product.Id).OrderBy(cat => cat.CategoryId).ToList();
            }

            return View(model: product);
        }
    }
}