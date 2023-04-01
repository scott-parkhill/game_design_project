using Chaos.Models.ViewModels;
using Chaos.Models.DbModels;
using Microsoft.EntityFrameworkCore;
using Chaos.Extensions;
using Chaos.Data;
using Chaos.Business;
using Chaos.Models;
using System.Text.Json;

namespace Chaos.Services.GameDbService;

public partial class GameDbService : IGameDbService
{
    public async Task<DbResult> AddArmy(string loggedUserId)
    {
        if (await _context.Armies.AnyAsync(u => u.UserId == loggedUserId))
            return new(TaskResults.Invalid, "User already has an army.");

        Army army = new()
        {
            UserId = loggedUserId,
            Recruits = 20,
            UserWeaponsJsonData = "{}"
        };
        
        var faction = await GetUserFaction(loggedUserId);

        switch (faction)
        {
            case Factions.None:
                return new(TaskResults.Invalid, "User doesn't exist in the database.");
            case Factions.Dolphins:
                army.RecruitRate = 12;
                break;
            case Factions.Pirates:
                army.CoinGenerationRate = 120;
                break;
        }

        await _context.AddAsync(army);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return new(TaskResults.Failure, "Failed to add army to the database.");
        }

        return new(TaskResults.Success, "Army successfully added to database.");
    }

    public async Task<ArmyViewModel?> GetArmyViewModel(string userId)
    {
        return await _context.Armies.Where(u => u.UserId == userId).ToViewModel().SingleOrDefaultAsync();
    }

    // TODO This is hacktastic, fix.
    public async Task<DbResult> UpdateArmies()
    {
        var armies = await _context.Armies.ToListAsync();

        foreach (var army in armies)
        {
            if (army.LastRecruitment.Date < DateTime.UtcNow.Date)
            {
                army.Recruits += army.RecruitRate;
                army.LastRecruitment = DateTime.UtcNow;
            }

            if (army.LastCoinGeneration.Date < DateTime.UtcNow.Date)
            {
                army.Coins += army.CoinGenerationRate;
                army.LastCoinGeneration = DateTime.UtcNow;
            }
        }

        _context.UpdateRange(armies);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return new(TaskResults.Failure, "Failed to add recruits and coins.");
        }

        return new(TaskResults.Success, "Successfully added recruits and coins.");
    }

    public async Task<DbResult> UpdateSoldierCount(string loggedUserId, int recruits, int attackers, int defenders, int sentries, int sappers)
    {
        var army = await _context.Armies.Where(u => u.UserId == loggedUserId).FirstOrDefaultAsync();

        if (army is null)
            return new(TaskResults.Invalid, "Army doesn't exist for that user.");

        army.Recruits = recruits;
        army.Attackers = attackers;
        army.Defenders = defenders;
        army.Sentries = sentries;
        army.Sappers = sappers;

        _context.Update(army);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return new(TaskResults.Failure, "Failed to update soldier count.");
        }

        return new(TaskResults.Success, "Successfully updated soldier count.");
    }

    public async Task<DbResult> UpdateCoins(string loggedUserId, int coins)
    {
        var army = await _context.Armies.Where(u => u.UserId == loggedUserId).FirstOrDefaultAsync();

        if (army is null)
            return new(TaskResults.Invalid, "No army exists for that user.");

        army.Coins = coins;

        _context.Update(army);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return new(TaskResults.Failure, "Failed to update coins for the army.");
        }

        return new(TaskResults.Success, "Successfully updated coins for the army.");
    }

    public async Task<DbResult> TrainRecruits(string loggedUserId, int newAttackers, int newDefenders, int newSentries, int newSappers)
    {
        if (newAttackers < 0 || newDefenders < 0 || newSentries < 0 || newSappers < 0)
            return new(TaskResults.Invalid, "Cannot have any trainee values that are less than zero.");

        var newTrainees = newAttackers + newDefenders + newSentries + newSappers;

        if (newTrainees == 0)
            return new(TaskResults.Success, "No trainees added, as requested lol.");

        var army = await _context.Armies.Where(u => u.UserId == loggedUserId).SingleOrDefaultAsync();

        if (army is null)
            return new(TaskResults.Invalid, "No army exists for that user.");

        if (army.Recruits - newTrainees < 0)
            return new(TaskResults.Invalid, "Not enough recruits exist to train.");

        army.Recruits -= newTrainees;
        army.Attackers += newAttackers;
        army.Defenders += newDefenders;
        army.Sentries += newSentries;
        army.Sappers += newSappers;

        _context.Update(army);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return new(TaskResults.Failure, "Failed to update army.");
        }

        return new(TaskResults.Success, "Army successfully updated.");
    }

    public async Task<DbResult> UpdateUserWeapons(string loggedUserId, params (int Delta, bool IsPurchasing, WeaponTypes WeaponType)[] weapons)
    {
        var army = await _context.Armies.Where(u => u.UserId == loggedUserId).SingleOrDefaultAsync();

        if (army is null)
            return new(TaskResults.Invalid, "Army doesn't exist.");
        
        var costTotal = weapons.Where(u => u.IsPurchasing).Sum(u => u.Delta * Weapon.Weapons[u.WeaponType].Cost);

        if (army.Coins - costTotal < 0)
            return new(TaskResults.Failure, "Not enough money to purchase all the weapons.");

        var currentWeapons = JsonSerializer.Deserialize<UserWeaponsData>(army.UserWeaponsJsonData) ?? new();

        foreach (var (delta, _, weaponType) in weapons)
        {
            var weapon = Weapon.Weapons[weaponType];
            
            var userWeapon = currentWeapons.UserWeapons.Where(u => u.WeaponType == weaponType).FirstOrDefault();

            if (userWeapon is null)
            {
                if (delta < 0)
                    return new(TaskResults.Invalid, $"Cannot have a negative amount of weapons of type {weaponType}.");

                currentWeapons.UserWeapons.Add(new(delta, weaponType));
            }
            else
            {
                userWeapon.Count += delta;

                if (userWeapon.Count < 0)
                    return new(TaskResults.Invalid, $"Cannot have a negative amount of weapons of type {weaponType}.");
            }
        }

        army.UserWeaponsJsonData = JsonSerializer.Serialize(currentWeapons);
        army.Coins -= costTotal;

        _context.Update(army);

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            return new(TaskResults.Failure, "Failed to update the database with the new weapon counts/purchases.");
        }

        return new(TaskResults.Success, "Successfully saved weapon changes.");
    }
}
