using ClientManager.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace ClientManager.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<ClientAdditionalField> ClientAdditionalFields { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>()
                .HasMany(c => c.AdditionalFields)
                .WithOne(af => af.Client)
                .HasForeignKey(af => af.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
