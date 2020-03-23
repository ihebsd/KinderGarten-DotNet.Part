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
    public class ReputationService: Service<Reputation>, IReputationService
    {
        static IDatabaseFactory Factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public ReputationService() : base(utk)
        {
        }
        public void commit()
        {
            utk.Commit();
        }
        public void Dispose()
        {
            utk.Dispose();
        }
        public IEnumerable<Reputation> SearchKReputationByName(string searchString)
        {
            IEnumerable<Reputation> ReputationDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                ReputationDomain = GetMany(x => x.Name.Contains(searchString));
            }

            return ReputationDomain;
        }
    }
}
