using System.Collections.Generic;
using System.Linq;
using webstore.Data;
using webstore.Models;

namespace webstore.Services
{
    public class Categories
    {
        public List<Category> categories;
        public Categories(WebstoreContext db)
        {
            categories = db.Categories.OrderBy(category => category.Id).ToList();
        }
    }
}