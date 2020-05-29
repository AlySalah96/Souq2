using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace Souq.Models
{

    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public int Stars { get; set; }
        public int Sale { get; set; }
        public int Quantity { get; set; }
        public int CatId { get; set; }

        [ForeignKey("CatId")]
        public virtual Category Category { get; set; }

        public virtual ICollection<Cart> Carts { get; set; }
    }
}