using Entities.DataTransferObjects;
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
        User GetProfile(Guid userId);
        void CreateUser(User user);
        void UpdateUser(User user, UserUpdateDto update);
        User Login(LoginModel user);
    }
}
