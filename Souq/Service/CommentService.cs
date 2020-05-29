using Souq.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Souq.Service
{
    public class CommentService : IServiceBase<Comment>
    {
        private readonly SouqContext context;

        public CommentService(SouqContext context)
        {
            this.context = context;
        }

        public int Add(Comment comment)
        {
            context.Comments.Add(comment);
            context.SaveChanges();
            return 1;
        }

        public int Delete(int id)
        {
            Comment comment = context.Comments.FirstOrDefault(c => c.Id == id);

            context.Comments.Remove(comment);
            context.SaveChanges();

            return 1;

        }

        public List<Comment> GetAll()
        {
            throw new NotImplementedException();
        }

        public Comment GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(int id, Comment Model)
        {
            throw new NotImplementedException();
        }

        public List<Comment> GetProductComments(int productId)
        {
            var comments =
                context.Comments.Where(product => product.ProductId == productId).ToList();

            return comments;
        } 
    }
}