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
    public class PublicationService : Service<Publication>, IPublicationService
    {
        static IDatabaseFactory factory = new DatabaseFactory();
        static IUnitOfWork UTK = new UnitOfWork(factory);
        public PublicationService() : base(UTK)
        {

        }

        public void annud(int id)
        {
            Publication p = GetById(id);
            p.nbDislike--;
            Commit();
        }

        public void annul(int id)
        {
            Publication p = GetById(id);
            p.nbLike--;
            Commit();
        }

        public void Dislike(int id)
        {
            Publication p = GetById(id);
            p.nbDislike++;
            Commit();
        }

        public IEnumerable<Publication> GetPubByTitle(string title)
        {
            return GetMany(f => f.titlePub.Contains(title));
        }

        public void IncNbVue(int id)
        {
            Publication p = GetById(id);
            p.nbVue++;
            Commit();
        }

        public void Like(int id)
        {
            Publication p = GetById(id);
            p.nbLike++;
            Commit();
        }
    }
}
