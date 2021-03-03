using System.Collections.Generic;

namespace webstore.Models.ViewModels
{
    public class HomeIndexViewModel
    {
        public ICollection<Product> products { get; set; }
        public ICollection<Advertisement> advertisements { get; set; }
        public ICollection<Category> categories { get; set; }
    }
}