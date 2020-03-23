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
    public class KidService : Service<Kid>, IKidService
    {
        static IDatabaseFactory Factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public KidService() : base(utk)
        {
           
        }
        public IEnumerable<Kid> GetKidByName(string FirstName)
        {
            return GetMany(f => f.FirstName.Contains(FirstName));
        }
    }
}