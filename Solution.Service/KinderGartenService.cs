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
    public class KinderGartenService : Service<KinderGarten>, IKinderGartenService
    {
        static IDatabaseFactory Factory = new DatabaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public KinderGartenService() : base(utk)
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






        public IEnumerable<KinderGarten> SearchKindergartenByName(string searchString)
        {
            IEnumerable<KinderGarten> KindergartenDomain = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                KindergartenDomain = GetMany(x => x.Name.Contains(searchString));
            }

            return KindergartenDomain;
        }
        public void IncNbVue(int? id)
        {
            KinderGarten k = GetById((int)id);
            k.nbVue++;
            Commit();
        }
    }
}
