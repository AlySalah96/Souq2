using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Souq.Models;
using Souq.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin;
using System.Threading.Tasks;
using System.Web.Security;

namespace Souq.Controllers
{
    public class AccountController : Controller
    {
        UserStore<Customer> store;

        UserManager<Customer> manager;

        SouqContext context;

        public AccountController()
        {
            store = new UserStore<Customer>(new SouqContext());

            manager = new UserManager<Customer>(store);

            context = new SouqContext();

        }


        [HttpGet]
        public string Login(string UserName, string password)
        {
            //if (!ModelState.IsValid)
            //    return View(loginAccount);

            try
            {
                var user = manager.Find(UserName, password);

                if (user != null)
                {
                    IAuthenticationManager authenticationManager =
                        HttpContext.GetOwinContext().Authentication;

                    SignInManager<Customer, string> signinmanager =
                        new SignInManager<Customer, string>
                        (manager, authenticationManager);

                    signinmanager.SignIn(user, true, true);


                    CartInfo(UserName);

                    if (manager.IsInRole(user.Id, "Admin"))
                    {
                        return "admin";
                    }

                    return "true";

                }
                else
                {
                    ModelState.AddModelError("", "User name and password are not match");
                    return "false";
                }


            }

            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return "false";
            }
        }


        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterationViewModel registerAccount)
        {
            if (!ModelState.IsValid)
                return View(registerAccount);
            try
            {

                //Map VM to Model
                Customer customer = new Customer();
                customer.UserName = registerAccount.UserName;
                customer.PasswordHash = registerAccount.Password;
                customer.Email = registerAccount.Email;
                customer.Address = registerAccount.Address;


                IdentityResult result
                    = await manager.CreateAsync(customer, registerAccount.Password);

                if (result.Succeeded)
                {
                    manager.AddToRole(customer.Id, "customer");
                    //MAnager SignIn
                    IAuthenticationManager authenticationManager =
                        HttpContext.GetOwinContext().Authentication;
                    SignInManager<Customer, string> signinmanager =
                        new SignInManager<Customer, string>
                        (manager, authenticationManager);
                    signinmanager.SignIn(customer, true, true);

                    return RedirectToAction("index", "Product");
                }
                else
                {
                    //foreach ModelState.AddModelError("", (result.Errors.ToList())[0]);

                    return View(registerAccount);
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
                return View(registerAccount);
            }


        }

        public ActionResult Signout()
        {
            IAuthenticationManager manager = HttpContext.GetOwinContext().Authentication;

            manager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);

            return RedirectToAction("index", "Product");

        }

        public void CartInfo(string uname)
        {
            //Session.Add("countofAllitems", 0);
            //HttpContext.Session.SetS("countofAllitems", jsoncart);
            Customer user = manager.FindByName(uname);

            var userCart = (from userProduct in context.Carts
                            where userProduct.CustomerId == user.Id
                            select userProduct);

            if (Session["countofAllitems"] == null) {
                Session["countofAllitems"] = 0;
            } 

            int totalProducts = 0;
            foreach (var item in userCart)
            {

                totalProducts += item.Quantity;

                Session["countofAllitems"] = totalProducts;

            }

        }

    }
}
