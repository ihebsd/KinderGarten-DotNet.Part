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
   public  class DislikeService : Service<Dislike>, IDislikeInterface
    {
        static IDatabaseFactory factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(factory);
        public DislikeService() : base(utk)
        {

        }
    }
}
