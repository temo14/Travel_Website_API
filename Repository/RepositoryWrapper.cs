using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contracts;
using Entities;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _context;
        private IApartmentRepository? _apartmentRepository;
        private IUserRepository? _userRepository;
        private IActions? _actions;
        private IJwtUtils? _jwt;
        public RepositoryWrapper(RepositoryContext context)
        {
            _context = context;
        }
        public IJwtUtils JWT
        {
            get
            {
                if (_jwt == null)
                    _jwt = new JwtUtils();
                return _jwt;
            }
        }
        public IActions Actions
        {
            get
            {
                if (_actions == null)
                    _actions = new ActionsRepository(_context);
                return _actions;
                
            }
        }
        public IUserRepository User 
        { 
            get 
            {
                if (_userRepository == null)
                    _userRepository = new UserRepository(_context);
                return _userRepository;
            } 
        }

        public IApartmentRepository Apartment
        {
            get
            {
                if (_apartmentRepository == null)
                    _apartmentRepository = new ApartmentRepository(_context);
                return _apartmentRepository;
            }
        }
        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
