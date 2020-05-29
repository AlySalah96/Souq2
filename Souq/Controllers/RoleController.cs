using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Souq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Souq.Controllers
{
    
    public class RoleController : Controller
    {
        // GET: Role
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> New(string RoleName)
        {
            if (RoleName != null)
            {
                SouqContext context = new SouqContext();
                RoleStore<IdentityRole> store = new RoleStore<IdentityRole>(context);
                RoleManager<IdentityRole> manager = new RoleManager<IdentityRole>(store);

                IdentityRole role = new IdentityRole();
                role.Name = RoleName;
                IdentityResult result = await manager.CreateAsync(role);
                if (result.Succeeded)
                    return View();
                else
                {
                    ViewBag.Error = result.Errors;
                    ViewBag.RoleName = RoleName;
                    return View();
                }
            }
            return View();
        }
    }
}