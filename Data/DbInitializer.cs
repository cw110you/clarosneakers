using System;
using System.Linq;
using webstore.Models;

namespace webstore.Data
{
    public static class DbInitializer
    {
        public static void Initialize(WebstoreContext context)
        {

            if (context.Database.EnsureCreated())
            {
                InitialProducts products = new InitialProducts();
                InitialAdvertisements advertisements = new InitialAdvertisements();
                InitialCategories categories = new InitialCategories();
                InitialCatalogs catalogs = new InitialCatalogs();
                InitialMembers members = new InitialMembers();
                InitialStates states = new InitialStates();

                foreach (Product product in products.products)
                {
                    context.Products.Add(product);
                }
                context.SaveChanges();

                foreach (Image image in products.images)
                {
                    context.Images.Add(image);
                }
                context.SaveChanges();

                foreach (Advertisement advertisement in advertisements.advertisements)
                {
                    context.Advertisements.Add(advertisement);
                }
                context.SaveChanges();

                foreach (Category category in categories.categories)
                {
                    context.Categories.Add(category);
                }
                context.SaveChanges();

                foreach (Catalog catalog in catalogs.catalogs)
                {
                    context.Catalogs.Add(catalog);
                }
                context.SaveChanges();

                foreach (Member member in members.members)
                {
                    context.Members.Add(member);
                }
                context.SaveChanges();

                foreach (State state in states.states)
                {
                    context.States.Add(state);
                }
                context.SaveChanges();
            }
            else
            {
                return; // DB is already existed.
            }

        }
    }
}