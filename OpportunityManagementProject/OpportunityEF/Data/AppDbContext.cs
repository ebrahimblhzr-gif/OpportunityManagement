using Microsoft.EntityFrameworkCore;
using OpportunityEF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpportunityEF.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<Opportunity> Opportunities { get; set; }

        public DbSet<User> Users { get; set; }
    }
}
