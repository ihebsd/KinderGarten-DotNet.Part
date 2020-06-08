using Service.Pattern;
using Solution.Data.Infrastructure;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public class CarPoolService : Service<CarPool>, ICarPoolService
    {
        private static IDatabaseFactory dbfactory = new DatabaseFactory();
        private static IUnitOfWork UOW = new UnitOfWork(dbfactory);
       // private Context db = new Context();
        IUserService UserService;
        IGeoLocationService GeoLocationService;
        public CarPoolService() : base(UOW)
        {
            UserService = new UserService();
            GeoLocationService = new GeoLocationService();
        
        }
        public IEnumerable<CarPool> SearchParentByTo(string searchString)
        {
            IEnumerable<CarPool> carPoolDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                carPoolDomain = GetMany(x => x.To.Contains(searchString));
            }

            return carPoolDomain;
        }


    }
}
