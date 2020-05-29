using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Souq.Models
{
    public class SouqContext: IdentityDbContext<Customer>
    {
        public SouqContext():base("Souq")
        {

        }

        public SouqContext(string connectionString) :base(connectionString)
        {

        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Order_Product> Order_Products { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }

    }
}