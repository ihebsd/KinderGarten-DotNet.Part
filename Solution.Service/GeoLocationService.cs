using Service.Pattern;
using Solution.Data.Infrastructure;
using Solution.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Solution.Service
{
    public class GeoLocationService : Service<GeoLocation>, IGeoLocationService
    {
        static IDatabaseFactory Factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public GeoLocationService() : base(utk)
        {

        }

        public void Add(GeoLocationService entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(GeoLocationService entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<GeoLocationService, bool>> where)
        {
            throw new NotImplementedException();
        }

        public GeoLocationService Get(Expression<Func<GeoLocationService, bool>> where)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GeoLocationService> GetMany(Expression<Func<GeoLocationService, bool>> where = null, Expression<Func<GeoLocationService, bool>> orderBy = null)
        {
            throw new NotImplementedException();
        }

        public void Update(GeoLocationService entity)
        {
            throw new NotImplementedException();
        }

        GeoLocationService IService<GeoLocationService>.GetById(long id)
        {
            throw new NotImplementedException();
        }

        GeoLocationService IService<GeoLocationService>.GetById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
