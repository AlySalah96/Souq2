using Souq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Souq.ViewModels
{
    public class CartViewModel
    {
        public int Id { get; set; }

        public Product  Product { get; set; }

        public int Quantity { get; set; }

        public int Price { get; set; }
    }
}