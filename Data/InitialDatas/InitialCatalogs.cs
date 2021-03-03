using webstore.Models;

namespace webstore.Data
{
    public class InitialCatalogs
    {
        public Catalog[] catalogs { get; set; }

        public InitialCatalogs()
        {
            catalogs = new Catalog[]
            {
                new Catalog{ ProductId = 1, CategoryId = 1 },
                new Catalog{ ProductId = 2, CategoryId = 1 },
                new Catalog{ ProductId = 3, CategoryId = 1 },
                new Catalog{ ProductId = 4, CategoryId = 2 },
                new Catalog{ ProductId = 4, CategoryId = 3 },

                new Catalog{ ProductId = 5, CategoryId = 2 },
                new Catalog{ ProductId = 6, CategoryId = 3 },
                new Catalog{ ProductId = 7, CategoryId = 3 },
                new Catalog{ ProductId = 8, CategoryId = 4 },
                new Catalog{ ProductId = 8, CategoryId = 5 },

                new Catalog{ ProductId = 9, CategoryId = 3 },
                new Catalog{ ProductId = 9, CategoryId = 2 },
                new Catalog{ ProductId = 10, CategoryId = 2 },
                new Catalog{ ProductId = 11, CategoryId = 4 },
                new Catalog{ ProductId = 11, CategoryId = 5 },

                new Catalog{ ProductId = 12, CategoryId = 2 },
                new Catalog{ ProductId = 13, CategoryId = 3 },
                new Catalog{ ProductId = 14, CategoryId = 4 },
                new Catalog{ ProductId = 15, CategoryId = 5 },
                new Catalog{ ProductId = 16, CategoryId = 4 },

                new Catalog{ ProductId = 17, CategoryId = 1 },
                new Catalog{ ProductId = 17, CategoryId = 3 },
                new Catalog{ ProductId = 18, CategoryId = 2 },
                new Catalog{ ProductId = 18, CategoryId = 3 },
                new Catalog{ ProductId = 18, CategoryId = 5 }
                //new Catalog{ ProductId = 1, CategoryId = 1 }
            };
        }
    }
}