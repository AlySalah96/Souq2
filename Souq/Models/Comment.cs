using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Souq.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string CustomerId { get; set; }

        public int ProductId { get; set; }

        public int Stars { get; set; }

        public string CommentText { get; set; }

        [ForeignKey("CustomerId")]
        public virtual Customer Customer { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { get; set; }

    }
}