using Contracts;
using Entities;
using Entities.DataTransferObjects;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
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

        public void CreateUser(User user)
        {
            var check = Context.Users.FirstOrDefault(i => i.Email == user.Email);

            if (check != null) throw new Exception("Email already exists");
            Create(user);
        }

        public ReturnProfileDto GetProfile(Guid userId)
        {
            var user = Context.Users.FirstOrDefault(user => user.Id.Equals(userId)) ??
                throw new NullReferenceException("User doesnot exists");
            
            return new ReturnProfileDto()
            {
                Description = user.Description,
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Id = userId,
                Image = user.Image
            };
        }

        public User Login(LoginModel user)
        {
            var acc = Context.Users.FirstOrDefault(u => u.Email.ToLower() == user.Email.ToLower()
                                                       && u.Password.ToLower() == user.Password.ToLower());

            if (acc != null) return acc;

            throw new NullReferenceException("Invalid Login attempt");
        }

        public void UpdateUser(ReturnProfileDto update)
        {
            var user = Context.Users.FirstOrDefault(i => i.Id == update.Id);
            if (user == null) throw new ArgumentException();

            // check which properties is updating.

            user.Email = update.Email ?? user.Email;

            user.FirstName = update.FirstName ?? user.FirstName;

            user.LastName = update.LastName ?? user.LastName;

            user.Image = update.Image ?? user.Image;

            user.Description = update.Description ?? user.Description;

        }
    }
}
