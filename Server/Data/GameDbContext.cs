
using Closavy.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace Closavy.Server.Data;

public class GameDbContext : DbContext
{
    public GameDbContext(DbContextOptions<GameDbContext> options)
        : base(options)
    {
    }

    public DbSet<Character> Characters => Set<Character>();
}