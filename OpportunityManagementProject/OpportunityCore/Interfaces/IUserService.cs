using OpportunityEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpportunitiesBL.Interfaces
{
    public interface IUserService
    {
        Task<User?> GetByUsernameAsync(string username);

        Task<User> AddAsync(User user);
    }
}
