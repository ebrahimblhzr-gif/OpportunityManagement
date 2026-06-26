using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpportunityEF.Models;

namespace OpportunitiesBL.Interfaces
{

    public interface IOpportunityService
    {
        Task<List<Opportunity>> GetAllAsync();

        Task<Opportunity?> GetByIdAsync(int id);

        Task<Opportunity> AddAsync(Opportunity opportunity);

        Task<bool> UpdateAsync(Opportunity opportunity);

        Task<bool> DeleteAsync(int id);

        Task<List<Opportunity>> GetFilteredAsync(
    string? keyword,
    string? type,
    string? country,
    bool? isFullyFunded,
    int pageNumber,
    int pageSize);
    
    Task<int> CountAsync(
    string? keyword,
    string? type,
    string? country,
    bool? isFullyFunded);

    }
}
