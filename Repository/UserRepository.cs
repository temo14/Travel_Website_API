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
            var user = Context.Users.Where(user => user.Id.Equals(userId)).FirstOrDefault() ??
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

            throw new NullReferenceException();
        }
        
        public void UpdateUser(ReturnProfileDto update)
        {
            var user = Context.Users.FirstOrDefault(i => i.Id == update.Id);
            if (user != null)
            {
                user.Email = update.Email == null ? user.Email : update.Email;

                user.FirstName = update.FirstName == null ? user.FirstName : update.FirstName;

                user.LastName = update.LastName == null ? user.LastName : update.LastName;

                user.Image = update.Image == null ? user.Image : update.Image;

                user.Description = update.Description == null ? user.Description : update.Description;
            }
        }
    }
}
