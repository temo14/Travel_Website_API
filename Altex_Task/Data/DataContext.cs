//using Microsoft.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.AspNetCore.Identity;
//using Altex_Task.Models.Profile;

//namespace Altex_Task.Data

//{
//    public class DataContext : IdentityDbContext<UserModel>
//    {
//        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

//        public DbSet<BookingGuests>? BookingsGuests { get; set; }
//        public DbSet<Appartment>? Appartments { get; set; }

//        public DbSet<City>? Cities { get; set; }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);
//            //builder.Entity<UserModel>().Ignore(x => x.UserName);
//        }
//    }
//}
