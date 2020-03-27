
using Service.Pattern;
using Solution.Data.Infrastructure;
using Solution.Domain;
using Solution.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Solution.Service
{
    public class UserService : Service<User>, IUserService
    {
        private static IDatabaseFactory dbf = new DatabaseFactory();
        private static IUnitOfWork ut = new UnitOfWork(dbf);
        public UserService() : base(ut)
        {
        }

        public User GetUserByLoginAndPassword(string login, string pwd)
        {
            var userr = GetMany(u => u.login == login && u.password == pwd).First();
            return userr;
        }
        public IEnumerable<User> GetParentByName(string prenom)
        {
            return GetMany(f => f.prenom.Contains(prenom));
        }
        public IEnumerable<User> GetParentByPrenom(string nom)
        {
            return GetMany(f => f.nom.Contains(nom));
        }
    }
}
