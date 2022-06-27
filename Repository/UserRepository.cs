﻿using Contracts;
using Entities;
using Entities.DataTransferObjects;
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

        public void CreateUser(User user)
        {
            var check = Context.Users.FirstOrDefault(i => i.Email == user.Email);
            if (check != null) throw new Exception("Email already exists");
            Create(user);
        }
           

        public User GetProfile(Guid userId)
        {
            var apartment = Context.Apartments.FirstOrDefault(i => i.OwnerId == userId);

            var user = Context.Users.Where(user => user.Id.Equals(userId)).FirstOrDefault() ??
                throw new NullReferenceException("User doesnot exists");

            user.Apartment = apartment;

            return user;
               
        }

        public User Login(LoginModel user)
        {
            var acc = Context.Users.FirstOrDefault(u => u.Email.ToLower() == user.Email.ToLower()
                                                       && u.Password.ToLower() == user.Password.ToLower());

            if (acc != null || acc.Password == user.Password) return acc;

            throw new NullReferenceException();
        }
        

        public void UpdateUser(User user,  UserUpdateDto update)
        {
            if (!string.IsNullOrEmpty(update.FirstName))
            {
                user.FirstName = update.FirstName;
            }
            if (!string.IsNullOrEmpty(update.Image))
            {
                user.Image = update.Image;
            }
            if (!string.IsNullOrEmpty(update.LastName))
            {
                user.LastName = update.LastName;
            }
            if (!string.IsNullOrEmpty(update.Email))
            {
                user.Email = update.Email;
            }
            user.Description = update.Description;

            Context.Users.Update(user);
        }
    }
}
