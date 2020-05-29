using Souq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Souq.Service
{
    public class CategoryService
    {
        public SouqContext Context { get; }

        public CategoryService(SouqContext context)
        {
            Context = context;
        }


        public int Add(Category Model)
        {
            Context.Categories.Add(Model);
            Context.SaveChanges();
            return 1;
        }

        public int Delete(int id)
        {
            var category = Context.Categories.FirstOrDefault(w => w.Id == id);
            Context.Categories.Remove(category);
            Context.SaveChanges();
            return 1;
        }

        public List<Category> GetAll()
        {
            return Context.Categories.ToList();
        }

        public Category GetById(int id)
        {
            return Context.Categories.FirstOrDefault(c => c.Id == id);
        }

        public int Update(int id, Category Model)
        {
            var category = Context.Categories.FirstOrDefault(w => w.Id == id);

            category.Name = Model.Name;

            Context.SaveChanges();

            return 1;
        }
    }
}