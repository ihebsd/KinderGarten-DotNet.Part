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
    public class AnotifService : Service<AdminNotif>, IAnotifService
    {
        static IDatabaseFactory Factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);

        public AnotifService() : base(utk)
        {

        }

        public List<AdminNotif> getNotifs()
        {
            IEnumerable<AdminNotif> m = (from Anotifs in utk.getRepository<AdminNotif>().GetAll()
                                         select Anotifs);
            List<AdminNotif> list = m.ToList<AdminNotif>();
            return list;
        }

        public IEnumerable<AdminNotif> SearchnotifBystat(string searchString)
        {
            IEnumerable<AdminNotif> RecDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                RecDomain = GetMany(x => x.msg.Contains(searchString));
            }
            return RecDomain;
        }
    }
}
