using OpportunitiesBL.Interfaces;
using OpportunityEF.Models;
using OpportunityEF.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpportunitiesBL.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _repository.GetByUsernameAsync(username);
        }

        public async Task<User> AddAsync(User user)
        {
            return await _repository.AddAsync(user);
        }
    }

}
