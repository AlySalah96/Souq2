using Souq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Souq.Service
{
    public class ProductService : IServiceBase<Product>
    {
        public SouqContext Context { get; }

        public ProductService(SouqContext context)
        {
            Context = context;
        }

        
        public int Add(Product Model)
        {
            Context.Products.Add(Model);
            Context.SaveChanges();
            return 1;
        }

        public int Delete(int id)
        {
            var watch = Context.Products.FirstOrDefault(w => w.ID == id);
            Context.Products.Remove(watch);
            Context.SaveChanges();
            return 1;
        }

        public List<Product> GetAll()
        {
            return Context.Products.ToList();
        }

        public Product GetById(int id)
        {
            return Context.Products.FirstOrDefault(watch => watch.ID == id);
        }

        public int Update(int id, Product Model)
        {
            var watch = Context.Products.FirstOrDefault(w => w.ID == id);

            watch.Name = Model.Name;
            watch.Brand = Model.Brand;
            watch.Price = Model.Price;
            watch.Image = Model.Image;
            watch.Stars = Model.Stars;
            watch.Description = Model.Description;

            Context.SaveChanges();

            return 1;
        }
    }
}