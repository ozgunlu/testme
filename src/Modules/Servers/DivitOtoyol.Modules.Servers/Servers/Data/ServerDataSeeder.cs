using Bogus;
using Bogus.Extensions.UnitedKingdom;
using BuildingBlocks.Abstractions.Persistence;
using DivitOtoyol.Modules.Servers.Servers.Models;
using DivitOtoyol.Modules.Servers.Servers.ValueObjects;
using DivitOtoyol.Modules.Servers.Shared.Contracts;
using Microsoft.EntityFrameworkCore;

namespace DivitOtoyol.Modules.Servers.Servers.Data;

public class ServerDataSeeder : IDataSeeder
{
    private readonly IServerDbContext _dbContext;

    public ServerDataSeeder(IServerDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task SeedAllAsync()
    {
        if (await _dbContext.Servers.AnyAsync())
            return;
    }
}
