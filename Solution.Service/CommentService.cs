using Service.Pattern;
using Solution.Data.Infrastructure;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public class CommentService : Service<Comment>, ICommentService
    {
        static IDatabaseFactory factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(factory);
        public CommentService() : base(utk)
        {

        }
        IEnumerable<Comment> ICommentService.GetCommentById(int Id)
        {
            return GetMany(f => f.CommentId == Id);
        }
        public IEnumerable<Comment> GetCommByTitle(string title)
        {
            return GetMany(f => f.post.Contains(title));
        }

    }
}
