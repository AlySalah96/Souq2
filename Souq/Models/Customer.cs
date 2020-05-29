using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Souq.Models
{

    public class Customer:IdentityUser
    {
        public string Address { get; set; }

        public string Image { get; set; }
        
    }


}