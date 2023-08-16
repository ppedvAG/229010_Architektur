using Microsoft.EntityFrameworkCore;
using ppedv.CarRentalXPress.Model;

namespace ppedv.CarRentalXPress.Data.EfCore
{
    public class CarRentalXPressContext : DbContext
    {
        private readonly string conString;

        public DbSet<Car> Cars { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Rent> Rents { get; set; }

        public CarRentalXPressContext(string conString)
        {
            this.conString = conString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(conString).UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Car>().Property(x => x.KW).HasColumnName("Leistungggggg");
        }

    }
}
