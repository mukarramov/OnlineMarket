using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class SaveChangeInterceptor : SaveChangesInterceptor
{
    public override InterceptionResult<int>
        SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        if (eventData.Context is null)
        {
            return base.SavingChanges(eventData, result);
        }

        AddEntity(eventData.Context);
        UpdateEntity(eventData.Context);
        DeleteEntity(eventData.Context);

        return base.SavingChanges(eventData, result);
    }

    private static void DeleteEntity(DbContext context)
    {
        var entityEntries = context.ChangeTracker.Entries<BaseEntity>().ToList();

        foreach (var entry in entityEntries.Where(x => x.State == EntityState.Deleted))
        {
            entry.State = EntityState.Modified;
            entry.Entity.IsDeleted = true;
        }
    }

    private static void UpdateEntity(DbContext context)
    {
        var entityEntries = context.ChangeTracker.Entries<BaseEntity>().ToList();

        foreach (var entry in entityEntries.Where(x => x.State == EntityState.Modified))
        {
            entry.Entity.UpdateAt = DateTime.UtcNow;
        }
    }

    private static void AddEntity(DbContext context)
    {
        var entityEntries = context.ChangeTracker.Entries<BaseEntity>().ToList();

        foreach (var entry in entityEntries.Where(x => x.State == EntityState.Added))
        {
            entry.Entity.CreateAt = DateTime.UtcNow;
        }
    }
}