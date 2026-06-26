using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpportunityEF.Repositories;
using OpportunitiesBL.Interfaces;
using OpportunityEF.Models;


namespace OpportunitiesBL.Services
{
  public class OpportunityService : IOpportunityService
    {
        private readonly IOpportunityRepository _repository;

        public OpportunityService(IOpportunityRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Opportunity>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<Opportunity?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Opportunity> AddAsync(Opportunity opportunity)
        {
            return await _repository.AddAsync(opportunity);
        }

        public async Task<bool> UpdateAsync(Opportunity opportunity)
        {
            return await _repository.UpdateAsync(opportunity);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }
        public async Task<List<Opportunity>> GetFilteredAsync(
    string? keyword,
    string? type,
    string? country,
    bool? isFullyFunded,
    int pageNumber,
    int pageSize)
        {
            return await _repository.GetFilteredAsync(
                keyword,
                type,
                country,
                isFullyFunded,
                pageNumber,
                pageSize);
        }

        public async Task<int> CountAsync(
    string? keyword,
    string? type,
    string? country,
    bool? isFullyFunded)
        {
            return await _repository.CountAsync(
                keyword,
                type,
                country,
                isFullyFunded);
        }

    }
}
