using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        User GetUserById(Guid userId);
        void CreateUser(User user);
        void UpdateUser(User user);
        User Login(LoginModel user);
    }
}
