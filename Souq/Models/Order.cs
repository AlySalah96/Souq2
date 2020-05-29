using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Souq.Models
{
    public class Order
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }

        public int TotalMoney { get; set; }

        public DateTime Date { get; set; }

        public string Address { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        public virtual ICollection<Order_Product> Order_Product { get; set; }


        /*
         *Id CutomerId Product  Count
         * 1  1        10       3
         * 1  1        11       2         
         * 1  1        12       1
         */


    }
}