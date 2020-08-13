namespace Reload.Core.DA
{
    using Microsoft.EntityFrameworkCore;
    using Reload.Core.DA.Entities;

    /// <summary>
    /// The database context.
    /// </summary>
    public class ReloadDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }

        /// <inheritdoc/>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(@"Data Source=reload_data.db");
        }

        /// <inheritdoc/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>().ToTable("User");
        }
    }
}
