using Souq.Models;
using Souq.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Souq.Controllers
{
    public class CategoryController : Controller
    {
        ProductService ProductService;
        CategoryService CategoryService;

        public CategoryController()
        {

            ProductService = new ProductService(new SouqContext());
            CategoryService = new CategoryService(new SouqContext());

        }
        public ActionResult Index()
        {
            return PartialView("_Index", CategoryService.GetAll());
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category newpCat)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CategoryService.Add(newpCat);
                    return RedirectToAction("Index","Admin");

                }
                catch
                { return View(newpCat); }
            }
            else { return View(newpCat); }
        }

        public ActionResult Edit(int id)
        {
            return View(CategoryService.GetById(id));
        }
        [HttpPost]
        public ActionResult Edit(int id, Category c)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    CategoryService.Update(id, c);
                    return RedirectToAction("Index", "Admin");
                }
                catch
                { return View(c); }
            }
            else
            { return View(c); }
        }

        public ActionResult Delete(int id)
        {
            CategoryService.Delete(id);
            return RedirectToAction("Index","Admin");

        }

    }
}