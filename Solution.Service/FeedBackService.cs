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
    public class FeedBackService: Service<FeedBack>, IFeedBackService
    {
        static IDatabaseFactory Factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public FeedBackService() : base(utk)
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
       /* public IEnumerable<FeedBack> SearchKReputationByName(string searchString)
        {
            IEnumerable<FeedBack> FeedBackDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                FeedBackDomain = GetMany(x => x.Name.Contains(searchString));
            }

            return FeedBackDomain;
        }*/
    }
}
