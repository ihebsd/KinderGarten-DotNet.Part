using Service.Pattern;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public interface ICommentService : IService<Comment>
    {
        IEnumerable<Comment> GetCommByTitle(String title);
        IEnumerable<Comment> GetCommentById(int Id);
    }
}
