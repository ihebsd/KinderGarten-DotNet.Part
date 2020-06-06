
using Service.Pattern;
using Solution.Domain.Entities;
using System.Collections.Generic;

namespace Solution.Service
{
    public interface IUserService: IService<User>
    {
         User GetUserByLoginAndPassword(string login, string pwd);
        IEnumerable<User> GetParentByName(string nom);
        IEnumerable<User> GetParentByPrenom(string prenom);
        
    }
}
