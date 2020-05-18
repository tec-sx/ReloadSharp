namespace Reload.DataAccess
{
    using Microsoft.EntityFrameworkCore;
    using Reload.DataAccess.Models;

    public class PersistentDb : DbContext
    {
        public DbSet<Player> Players { get; set; }
        public DbSet<InputContext> InputContexts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=reload_data.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Player>().ToTable("Players");
            modelBuilder.Entity<InputContext>().ToTable("InputContexts");
        }
    }
}
