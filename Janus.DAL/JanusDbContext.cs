using Janus.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Janus.DAL;

public sealed class JanusDbContext : DbContext
{
    public JanusDbContext(DbContextOptions<JanusDbContext> options)
        : base(options)
    {
    }

    public DbSet<Screen> Screens { get; set; }
    public DbSet<AdSlot?> AdSlots { get; set; }
}