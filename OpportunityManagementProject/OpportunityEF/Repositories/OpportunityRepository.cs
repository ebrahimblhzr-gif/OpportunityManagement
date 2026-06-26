using Microsoft.EntityFrameworkCore;
using OpportunityEF.Data;
using OpportunityEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpportunityEF.Repositories
{
    public class OpportunityRepository : IOpportunityRepository
    {
        private readonly AppDbContext _context;

        public OpportunityRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Opportunity>> GetAllAsync()
        {
            return await _context.Opportunities.ToListAsync();
        }

        public async Task<Opportunity?> GetByIdAsync(int id)
        {
            return await _context.Opportunities
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Opportunity> AddAsync(Opportunity opportunity)
        {
            await _context.Opportunities.AddAsync(opportunity);

            await _context.SaveChangesAsync();

            return opportunity;
        }

        public async Task<bool> UpdateAsync(Opportunity opportunity)
        {
            _context.Opportunities.Update(opportunity);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var opportunity = await _context.Opportunities.FindAsync(id);

            if (opportunity == null)
                return false;

            _context.Opportunities.Remove(opportunity);

            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<List<Opportunity>> GetFilteredAsync(
    string? keyword,
    string? type,
    string? country,
    bool? isFullyFunded,
    int pageNumber,
    int pageSize)
        {
            IQueryable<Opportunity> query =
                _context.Opportunities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(o =>
                    o.Title.Contains(keyword) ||
                    o.Description.Contains(keyword));
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(o => o.Type == type);
            }

            if (!string.IsNullOrWhiteSpace(country))
            {
                query = query.Where(o => o.Country == country);
            }

            if (isFullyFunded.HasValue)
            {
                query = query.Where(o =>
                    o.IsFullyFunded == isFullyFunded.Value);
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }


        public async Task<int> CountAsync(
    string? keyword,
    string? type,
    string? country,
    bool? isFullyFunded)
        {
            IQueryable<Opportunity> query =
                _context.Opportunities.AsQueryable();

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                query = query.Where(o =>
                    o.Title.Contains(keyword) ||
                    o.Description.Contains(keyword));
            }

            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(o => o.Type == type);
            }

            if (!string.IsNullOrWhiteSpace(country))
            {
                query = query.Where(o => o.Country == country);
            }

            if (isFullyFunded.HasValue)
            {
                query = query.Where(o =>
                    o.IsFullyFunded == isFullyFunded.Value);
            }

            return await query.CountAsync();
        }
    }

}
