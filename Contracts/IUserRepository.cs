using Entities.DataTransferObjects;
using Entities.Models;

namespace Contracts
{
    public interface IUserRepository : IRepositoryBase<User>
    {
        ReturnProfileDto GetProfile(Guid userId);
        void CreateUser(User user);
        void UpdateUser(ReturnProfileDto update);
        User Login(LoginModel user);
    }
}
