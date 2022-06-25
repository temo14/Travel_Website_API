using Contracts;
using Entities;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(RepositoryContext context) : base(context) { }

        public void CreateUser(User user) => Create(user);

        public User GetUserById(Guid userId)
        {
            return RepositoryContext.Users.Where(user => user.Id.Equals(userId)).FirstOrDefault()?? 
                throw new NullReferenceException("User doesnot exists");
        }

        public User Login(LoginModel user)
        {
            var acc = RepositoryContext.Users.FirstOrDefault(u => u.Email.ToLower() == user.Email.ToLower());

            if (acc != null || acc.Password == user.Password) return acc;

            throw new NullReferenceException();
        }
        

        public void UpdateUser(User user) => RepositoryContext.Users.Update(user);
        
    }
}
