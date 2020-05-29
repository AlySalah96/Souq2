using Souq.Models;
using Souq.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace Souq.Service
{
    public class CartService
    {
        private readonly SouqContext context;
        private readonly ProductService productService;

        public CartService(SouqContext context)
        {
            this.context = context;
            productService = new ProductService(new SouqContext());
        }

        public int Add(Cart newCart)
        {
            context.Carts.Add(newCart);
            context.SaveChanges();
            return 1;

        }

        public List<CartViewModel> GetUserCart(string customerId)
        {

            var userCart = (from userProduct in context.Carts
                            where userProduct.CustomerId == customerId
                            select userProduct);

            List<CartViewModel> UserCart = new List<CartViewModel>();
            CartViewModel cart;

            foreach (var item in userCart)
            {
                cart = new CartViewModel();
                cart.Id = item.Id;
                cart.Product = productService.GetById(item.ProductId);
                cart.Quantity = item.Quantity;
                cart.Price = cart.Product.Price * item.Quantity;

                UserCart.Add(cart);
            }


            return UserCart;
        }

        public SouqContext Context { get; }

        


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