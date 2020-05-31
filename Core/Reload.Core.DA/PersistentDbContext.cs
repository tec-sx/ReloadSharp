namespace Reload.Core.DA
{
    using Microsoft.EntityFrameworkCore;

    public class PersistentDbContext : DbContext
    {
        public DbSet<Reload.Core.DA.Models.Player> Players { get; set; }
        public DbSet<Reload.Core.DA.Models.InputContext> InputContexts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=reload_data.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Reload.Core.DA.Models.Player>().ToTable("Players");
            modelBuilder.Entity<Reload.Core.DA.Models.InputContext>().ToTable("InputContexts");
        }
    }
}
