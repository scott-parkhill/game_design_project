using Chaos.Business;
using Chaos.Data;
using Chaos.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace Chaos.Services;

using Extensions;

public class ArmyUpdaterService : IHostedService
{
    IDbContextFactory<GameDbContext> _dbContextFactory;
    
    // 10 minutes in milliseconds.
    System.Timers.Timer _timer = new(600_000);
    
    public ArmyUpdaterService(IDbContextFactory<GameDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
        _timer.Elapsed += (s,e) => UpdateDatabaseValues();
        _timer.AutoReset = true;
    }

    public Task StartAsync(CancellationToken stoppingToken)
    {
        _timer.Start();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken stoppingToken)
    {
        _timer.Stop();
        return Task.CompletedTask;
    }

    async void UpdateDatabaseValues()
    {
        await using GameDbContext _context = await _dbContextFactory.CreateDbContextAsync();

        var armies = await _context.Armies.ToListAsync();

        foreach (var army in armies)
        {
            var armyScore = ArmyScore.GetArmyScore(Queryable.AsQueryable(new List<Army>() { army }).ToViewModel().First());

            int generationMultiplier = armyScore switch
            {
                < 1000 => 1,
                >= 1000 and < 10000 => 2,
                _ => 3
            };
            
            army.Recruits += army.RecruitRate * generationMultiplier;
            army.LastRecruitment = DateTime.UtcNow;

            army.Coins += army.CoinGenerationRate * generationMultiplier;
            army.LastCoinGeneration = DateTime.UtcNow;
        }

        _context.UpdateRange(armies);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException ex)
        {
            Console.WriteLine(ex);
        }
    }
}
