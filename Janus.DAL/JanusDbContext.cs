using Janus.Domain.Entites;
using Microsoft.EntityFrameworkCore;

namespace Janus.DAL;

public class JanusDbContext : DbContext
{
    public DbSet<Screen> Screens { get; set; }
    public DbSet<AdSlot> AdSlots{ get; set; }

    public string DbPath { get; }

    public JanusDbContext()
    {
        const Environment.SpecialFolder folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "janus.db");
    }

    // The following configures EF to create a Sqlite database file in the
    // special "local" folder for your platform.
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}