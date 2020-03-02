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
    public class EventService : Service<Event>, IEventService
    {
        static IDatabaseFactory Factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public EventService() : base(utk)
        {
        }

        public void Add(KinderGarten entity)
        {
            throw new NotImplementedException();
        }

        public void commit()
        {
            utk.Commit();
        }

        public void Delete(KinderGarten entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Expression<Func<KinderGarten, bool>> where)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            utk.Dispose();
        }
            

        public IEnumerable<Event> SearchEventByName(string searchString)
        {
            IEnumerable<Event> EventDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                EventDomain = GetMany(x => x.Name.Contains(searchString));
            }
            return EventDomain;
        }

    
    }
}
