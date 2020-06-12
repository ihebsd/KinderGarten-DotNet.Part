
using Service.Pattern;
using Solution.Domain.Entities;

namespace Solution.Service
{
    public interface IUserService: IService<User>
    {
         User GetUserByLoginAndPassword(string login, string pwd);
    }
}
