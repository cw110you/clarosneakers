using webstore.Models;

namespace webstore.Data
{
    public class InitialCategories
    {
        public Category[] categories { get; set; }

        public InitialCategories()
        {
            categories = new Category[]
            {
                //new Category { Name = "其他", ImgSrc = "/images/categories/other.png" },
                new Category { Name = "慢跑鞋", ImgSrc = "/images/categories/jogging.png" },
                new Category { Name = "訓練鞋", ImgSrc = "/images/categories/training.png" },
                new Category { Name = "登山鞋", ImgSrc = "/images/categories/hiking.png" },
                new Category { Name = "休閒鞋", ImgSrc = "/images/categories/recreation.png" },
                new Category { Name = "籃球鞋", ImgSrc = "/images/categories/basketball.png" }
                //new Category{ Name = "", ImgSrc = "" }
            };
        }
    }
}