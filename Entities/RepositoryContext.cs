using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options)
        {
                
        }
        public DbSet<Apartments> Apartments { get; set; } = null!;
        public DbSet<BookingGuests> BookingGuests { get; set; } = null!;
        public DbSet<User> Users { get; set; } = null!;
    }
}
