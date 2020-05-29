using Souq.Models;
using Souq.Service;
using Souq.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Souq.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        SouqContext context;
        ProductService productService;

        public AdminController()
        {
            context = new SouqContext();
            productService = new ProductService(context);
        }

        
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            DashboardViewModel dashboard = new DashboardViewModel();

            dashboard.CatogeriesCount = context.Categories.Count();
            dashboard.ProductCount = context.Products.Count();
            dashboard.OrderCount = context.Orders.Count();
            dashboard.RolesCount = context.Roles.Count();
            dashboard.UsersCount = context.Users.Count();
            dashboard.CommentsCounts = context.Comments.Count();

            return PartialView("_Dashboard", dashboard);
        }

        public ActionResult AllProducts()
        {
            var products = productService.GetAll();

            return PartialView("_ProductsAdmin" , products);
        }
    }
}