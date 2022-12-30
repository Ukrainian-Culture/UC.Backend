using Entities;
using Entities.Configurations;
using Entities.Models;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Ukrainian_Culture.Tests.DbModels;

namespace Ukrainian_Culture.Tests;

public class ConnectionFactory : IDisposable
{

    private bool _disposedValue = false; // To detect redundant calls  

    public RepositoryContext CreateContextForInMemory(ITestableModel modelBuilder)
    {
        var option
            = new DbContextOptionsBuilder<RepositoryContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .UseModel(modelBuilder.GetModel())
                .Options;

        var context = new RepositoryContext(option);

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return context;
    }


    protected virtual void Dispose(bool disposing)
    {
        if (_disposedValue) return;
        if (disposing)
        {
        }
        _disposedValue = true;
    }

    public void Dispose()
    {
        Dispose(true);
    }
}