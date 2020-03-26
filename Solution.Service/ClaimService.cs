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
    public class ClaimService : Service<Claim>, IClaimService
    {
        static IDatabaseFactory Factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public ClaimService() : base(utk)
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
        public IEnumerable<Claim> SearchKClaimByName(string searchString)
        {
            IEnumerable<Claim> ClaimDomain = GetMany().ToList();
            if (!String.IsNullOrEmpty(searchString))
            {
                ClaimDomain = GetMany(x => x.Name.Contains(searchString));
            }

            return ClaimDomain;
        }
    }
}
