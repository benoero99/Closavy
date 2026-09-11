using Microsoft.EntityFrameworkCore;

namespace Closavy.Server.Data;

public static class EntityNamingConvention
{
    public static void Apply(ModelBuilder modelBuilder)
    {
        // Entities should end with "Entity" suffix but we map it to database without it
        foreach (var entityType in modelBuilder.Model.GetEntityTypes())
        {
            var name = entityType.ClrType.Name;

            if (name.EndsWith("Entity"))
            {
                entityType.SetTableName(name[..^"Entity".Length]);
            }
        }
    }
}