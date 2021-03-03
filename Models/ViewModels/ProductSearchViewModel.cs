using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using webstore.Data;

namespace webstore.Models.ViewModels
{
    public class ProductSearchViewModel
    {
        public static int pageSize = 15;
        public static int paginationLength = 7;

        public int totalPages { get; set; }
        public List<string> displayPages { get; set; }
        public List<Product> products;

        [Range(0, int.MaxValue)]
        public int categoryId { get; set; }

        [StringLength(50, ErrorMessage = "Query keyword can't exceed {1} characters.")]
        public string keyword { get; set; }

        [RegularExpression("asc|desc", ErrorMessage = "Query order is not valid.")]
        public string order { get; set; }

        [RegularExpression("price|date", ErrorMessage = "Query orderby is not valid.")]
        public string orderby { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Pagenumber must be greater than zero.")]
        public int page { get; set; }

        public ProductSearchViewModel()
        {
            categoryId = 0;
            keyword = "";
            order = "asc";
            //orderby = "date";
            page = 1;
        }

        public void ApplyQuery(WebstoreContext db)
        {
            if (categoryId == 0)
            {
                products = db.Products.Where(product => product.Name.Contains(keyword) && product.ReleaseDate.CompareTo(DateTime.Now) <= 0).ToList();
            }
            else
            {
                int[] prodIds = db.Catalogs.Where(catalog => catalog.CategoryId == categoryId).Select(catalog => catalog.ProductId).ToArray();
                products = db.Products.Where(product => prodIds.Contains(product.Id) && product.Name.Contains(keyword) && product.ReleaseDate.CompareTo(DateTime.Now) <= 0).ToList();
            }

            switch (orderby)
            {
                case "price":
                    products.Sort((x, y) => order == "asc" ? x.Price.CompareTo(y.Price) : y.Price.CompareTo(x.Price));
                    break;
                case "date":
                    products.Sort((x, y) => order == "asc" ? x.ReleaseDate.CompareTo(y.ReleaseDate) : y.ReleaseDate.CompareTo(x.ReleaseDate));
                    break;
            }

            products.ForEach(product => product.Images = db.Images.Where(image => image.ProductId == product.Id).OrderBy(image => image.Order).Take(1).ToList());

            Initialize();
        }

        private void Initialize()
        {
            totalPages = Convert.ToInt16(Math.Ceiling(products.Count / Convert.ToSingle(pageSize)));

            displayPages = new List<string> { page.ToString() };
            if (page <= totalPages)
                SetDisplayPages();
        }

        private void SetDisplayPages()
        {
            for (int i = 1; displayPages.Count < paginationLength && (page - i > 0 || page + i <= totalPages); i++)
            {
                if (page - i > 0)
                    displayPages.Insert(0, (page - i).ToString());
                if (displayPages.Count >= paginationLength)
                    break;
                if (page + i <= totalPages)
                    displayPages.Add((page + i).ToString());
            }

            if (displayPages.Count >= paginationLength)
            {
                if (Convert.ToInt16(displayPages.FirstOrDefault()) > 1)
                    displayPages[0] = "…";
                if (Convert.ToInt16(displayPages.LastOrDefault()) < totalPages)
                    displayPages[displayPages.Count - 1] = "…";
            }
        }
    }
}