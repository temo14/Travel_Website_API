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
                Image = user.Image,
                Password = user.Password
            };
        }

        public User Login(LoginModel user)
        {
            var acc = Context.Users.FirstOrDefault(u => u.Email.ToLower() == user.Email.ToLower()
                                                       && u.Password.ToLower() == user.Password.ToLower());

            if (acc != null || acc.Password == user.Password) return acc;

            throw new NullReferenceException();
        }
        

        public void UpdateUser(User user)
        {
            Context.Entry(Context.Users.FirstOrDefault(i=>i.Id ==user.Id)).State = EntityState.Detached;

            var r = Context.Users.FirstOrDefault(i => i.Id == user.Id);
            

            Update(user);

            //Context.Users.Attach(user);

            //Context.Entry(user).State =EntityState.Modified;


            //if (!string.IsNullOrEmpty(update.FirstName))
            //{
            //    user.FirstName = update.FirstName;
            //}
            //if (!string.IsNullOrEmpty(update.Image))
            //{
            //    user.Image = update.Image;
            //}
            //if (!string.IsNullOrEmpty(update.LastName))
            //{
            //    user.LastName = update.LastName;
            //}
            //if (!string.IsNullOrEmpty(update.Email))
            //{
            //    user.Email = update.Email;
            //}
            //user.Description = update.Description;


        }
    }
}
