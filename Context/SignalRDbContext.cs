using Microsoft.EntityFrameworkCore;
using SignalR_Asp.netCoreWebAppDemo.Model;

namespace SignalR_Asp.netCoreWebAppDemo.Context
{
    public class SignalRDbContext : DbContext
    {
        public SignalRDbContext(DbContextOptions<SignalRDbContext> options)
        : base(options)
        {
        }

        public DbSet<ClientInfo> UserConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ClientInfo>()
                .HasKey(uc => new { uc.MacAddress, uc.ConnectionId });
        }
    }
}
