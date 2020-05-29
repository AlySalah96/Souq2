using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Souq.Models;
using Souq.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Souq.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly CartService cartService;

        UserManager<Customer> manager;
        UserStore<Customer> store;




        public CartController()
        {
            cartService = new CartService(new SouqContext());

            store =
                new UserStore<Customer>(new SouqContext());

            manager =
                new UserManager<Customer>(store);
        }


        public async Task<ActionResult> Checkout()
        {
            Customer user = await manager.FindByNameAsync(User.Identity.Name);

            var carts = cartService.GetUserCart(user.Id);

            return View(carts);
        }

        // GET: Cart
        public async Task<ActionResult> ShowUserCart(int ID)
        {

            Customer user = await manager.FindByNameAsync(User.Identity.Name);

            AddToSession();

            if (ID != -1)
            {
                Cart cart = new Cart();


                cart.CustomerId = user.Id;
                cart.ProductId = ID;
                cart.Quantity = 1;

                cartService.Add(cart);
            }
            var carts = cartService.GetUserCart(user.Id);

            return View(carts);
        }


        public void AddToSession()
        {
            if (Session["countofAllitems"] == null)
            {
                AccountController account = new AccountController();
                account.CartInfo(User.Identity.Name);

                Session["countofAllitems"] = 1;
                return;
            }

            Session["countofAllitems"] = (int)Session["countofAllitems"] + 1;
        }


    }
}