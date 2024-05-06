using Microsoft.EntityFrameworkCore;
using RapidPay.Models;

namespace RapidPay.Persistence
{
    public class RapidPayDbContext : DbContext
    {
        public RapidPayDbContext(DbContextOptions<RapidPayDbContext> options) : base(options) { }

        public DbSet<Card> Cards { get; set; }
    }
}
