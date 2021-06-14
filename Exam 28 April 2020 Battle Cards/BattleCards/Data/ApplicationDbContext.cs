namespace BattleCards.Data
{
    using BattleCards.Models;
    using Microsoft.EntityFrameworkCore;
    using System.Reflection;

    public class ApplicationDbContext : DbContext
    { 
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
            }
            
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Card> Cards { get; set; }

        public DbSet<UserCard> UserCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
       
    }
}
