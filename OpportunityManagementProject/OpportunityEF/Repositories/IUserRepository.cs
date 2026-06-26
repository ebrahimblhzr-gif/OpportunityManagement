using OpportunityEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpportunityEF.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByUsernameAsync(string username);

        Task<User> AddAsync(User user);
    }
}
