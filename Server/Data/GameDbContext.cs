
using Closavy.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Closavy.Server.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        EntityNamingConvention.Apply(modelBuilder);
    }

    public DbSet<CharacterEntity> Characters => Set<CharacterEntity>();
}