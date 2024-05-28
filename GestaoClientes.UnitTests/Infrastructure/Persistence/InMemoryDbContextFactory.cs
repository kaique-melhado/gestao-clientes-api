using GestaoClientes.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace GestaoClientes.UnitTests.Infrastructure.Persistence;

public static class InMemoryDbContextFactory
{
    public static DataContext Create()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        var context = new DataContext(options);
        return context;
    }

    public static void Destroy(DataContext context)
    {
        context.Database.EnsureDeleted();
        context.Dispose();
    }
}
