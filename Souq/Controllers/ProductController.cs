using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Souq.Models;
using Souq.Service;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Souq.Controllers
{
    public class ProductController : Controller
    {
        const int NUMBEROFPRODUCTINPAGE = 8 ;


        ProductService productService;
        CommentService commentService;
        CategoryService categoryService;

        UserManager<Customer> manager;
        UserStore<Customer> store;

        public ProductController()
        {
            productService = new ProductService(new SouqContext());
            commentService = new CommentService(new SouqContext());
            categoryService = new CategoryService(new SouqContext());

            store = 
                new UserStore<Customer>(new SouqContext());

            manager =
                new UserManager<Customer>(store);



            

        }

        //GET: Watches
        public ActionResult Index()
        {
            int pageNo = int.Parse(Session["Page"].ToString());

            var watches =
                productService.GetAll().Skip(pageNo * NUMBEROFPRODUCTINPAGE).Take(NUMBEROFPRODUCTINPAGE);

            ViewBag.cat = categoryService.GetAll();

            return View(watches);
        }

        [HttpPost]
        public ActionResult Search(int CatId, string Name)
        {
            int pageNo = int.Parse(Session["Page"].ToString());
            ViewBag.cat = categoryService.GetAll();
            if (CatId == -1)
            {
                var Pro =
                productService.GetAll().Where(p => p.Name.ToLower().Contains(Name.ToLower())).Skip(pageNo * NUMBEROFPRODUCTINPAGE).Take(NUMBEROFPRODUCTINPAGE);
                return PartialView("_ProductDetails", Pro);
            }
            var Products =
                productService.GetAll().Where(p => p.CatId == CatId && p.Name.ToLower().Contains(Name.ToLower())).Skip(pageNo * NUMBEROFPRODUCTINPAGE).Take(NUMBEROFPRODUCTINPAGE);

            return PartialView("_ProductDetails", Products);
        }


        [Authorize]
        // GET: Watches/Details/5
        public ActionResult Details(int id)
        {
            Product product = productService.GetById(id);

            var comments =
                commentService.GetProductComments(id);

            ViewBag.Comments = comments;

            ViewBag.ProdectId = id;

            return View(product);
        }

        [HttpGet]
        public async Task<ActionResult> SaveComment(int productId, string CommentText)
        {
            Customer user = await manager.FindByNameAsync(User.Identity.Name);
            
            Comment comment = new Comment();

            comment.CustomerId = user.Id; 
            comment.ProductId = productId;
            comment.CommentText = CommentText;
            comment.Stars = 4;

            commentService.Add(comment);


            var comments =
                commentService.GetProductComments(productId);

            ViewBag.Comments = comments;

            ViewBag.ProdectId = productId;

            return PartialView("_ProductComment");
        }

        [Authorize]
        // GET: Watches/Create
        public ActionResult Create()
        {
            ViewBag.categories = categoryService.GetAll();
            return View();
        }

        [Authorize(Roles = ("Admin"))]
        // POST: Watches/Create
        [HttpPost]
        public ActionResult Create(Product newproduct , HttpPostedFileBase Image)
        {


            ViewBag.categories = categoryService.GetAll();
            if (ModelState.IsValid)
            {

                try
                {
                    if (Image != null)
                    {
                        string path = Path.Combine(Server.MapPath("~/Content/Images/Product"), Path.GetFileName(Image.FileName));
                        newproduct.Image = Image.FileName;

                        Image.SaveAs(path);
                    }
                    productService.Add(newproduct);
                    return RedirectToAction("Index","Admin");
                }
                catch
                { return View(newproduct); }
            }
            else
            { return View(newproduct); }
        }

        [Authorize(Roles = ("Admin"))]
        // GET: Watches/Edit/5
        public ActionResult Edit(int id)
        {
            ViewBag.categories = categoryService.GetAll();
            return View(productService.GetById(id));
        }

        [Authorize(Roles=("Admin"))]
        // POST: Watches/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Product p)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ViewBag.Select = "Product";
                    productService.Update(id, p);
                    return RedirectToAction("Index","Product");
                }
                catch
                { return View(p); }
            }
            else
            { return View(p); }
        }

        [Authorize(Roles = ("Admin"))]
        // GET: Watches/Delete/5
        public ActionResult Delete(int id)
        {
            productService.Delete(id);

            return RedirectToAction("Index");
        }

        [Authorize(Roles = ("Admin"))]
        // POST: Watches/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        public ActionResult ShowWatchesDetails(int pageNo, string orderBy)
        {
            //int page = int.Parse(Session["Page"].ToString());

            #region Sort
            //if (orders == null)
            //{
            //    sortByWatches = watchesService.GetAll();
            //    return PartialView("_WatchesDetails", sortByWatches);
            //}

            //if (orders.Equals("Price:High to Low"))
            //{
            //    sortByWatches = (from watch in watchesService.GetAll()
            //    orderby watch.Salary descending
            //    select watch).Skip(page * NUMBEROFPRODUCTINPAGE).Take(NUMBEROFPRODUCTINPAGE).ToList();

            //    return PartialView("_WatchesDetails", sortByWatches);
            //}
            #endregion

            List<Product> sortByWatches;

            if (orderBy == null)
                orderBy = Session["Order"].ToString();

            else
                Session["Order"] = orderBy;


                sortByWatches=
                    (Sort(orderBy).Skip((pageNo-1 )* NUMBEROFPRODUCTINPAGE).Take(NUMBEROFPRODUCTINPAGE)).ToList();

            
            return PartialView("_ProductDetails" , sortByWatches);

        }

        public ActionResult OpenModal(int id)
        {
            var watch = productService.GetById(id);

            return PartialView("_ProductModal", watch);
        }

        private IEnumerable<Product> Sort(string sortBy)
        {
            IEnumerable<Product> watches;
            switch (sortBy)
            {
                case "Best Match":
                    watches = (from watch in productService.GetAll()
                               select watch).ToList();
                    return watches;

                case "New Arrival":
                    watches = (from watch in productService.GetAll()
                               select watch).ToList();
                    return watches;

                case "Price:Low to High":
                    watches = (from watch in productService.GetAll()
                               orderby watch.Price 
                               select watch).ToList();
                    return watches;

                case "Price:High to Low":
                    watches = (from watch in productService.GetAll()
                               orderby watch.Price descending
                               select watch).ToList();
                    return watches;

                default:
                    watches = (from watch in productService.GetAll()
                               select watch).ToList();
                    return watches;
            }
        }


    }
    
}
