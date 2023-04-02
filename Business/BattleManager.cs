using Chaos.Models.ViewModels;
using Chaos.Models;

namespace Chaos.Business;

public class BattleManager
{
    const int RecruitAttritionRate = 2;
    const int TrainedAttritionRate = 1;

    readonly IGameDbService _dbService;
    readonly BattleCalculator _battleCalculator;

    public BattleManager(IGameDbService dbService, BattleCalculator battleCalculator) => (_dbService, _battleCalculator) = (dbService, battleCalculator);

    /// <summary> Attack another user. </summary>
    public async Task<(string Message, AfterActionReportViewModel ViewModel)> AttackUser(string aggressorId, string defenderId)
    {
        #region Setup
        TaskResults result = TaskResults.Invalid;
        string message = "";

        var aggressorArmy = await _dbService.GetArmyViewModel(aggressorId);
        var defenderArmy = await _dbService.GetArmyViewModel(defenderId);

        if (aggressorArmy is null || defenderArmy is null)
            throw new ArgumentException("Either the aggressor or defender does not have an army in the database.");
        
        var aggressorOriginalRecruits = aggressorArmy.Recruits;
        var aggressorOriginalAttackers = aggressorArmy.Attackers;
        var defenderOriginalRecruits = defenderArmy.Recruits;
        var defenderOriginalDefenders = defenderArmy.Defenders;
        
        var aggressorScore = ArmyScore.GetOffensiveScore(aggressorArmy, await _dbService.GetUserFaction(aggressorId));
        var defenderScore = ArmyScore.GetDefensiveScore(defenderArmy, await _dbService.GetUserFaction(defenderId));

        var outcome = _battleCalculator.CalculateBattleOutcome(aggressorScore, defenderScore);
        #endregion

        #region Equipment Losses

        double aggressorLossChance = 0;
        double defenderLossChance = 0;

        if (outcome.Outcome < 1)
        {
            aggressorLossChance = 0.1;
            defenderLossChance = 0.05;
        }
        else
        {
            aggressorLossChance = 0.05;
            defenderLossChance = 0.1;
        }

        List<UserWeapon> newAggressorWeapons = aggressorArmy.UserWeapons.UserWeapons
                                                        .Where(u => Weapon.Weapons[u.WeaponType].ActionType  == ActionTypes.Offence)
                                                        .Select(u => new UserWeapon() { Count = u.Count, WeaponType = u.WeaponType }).ToList();

        var aggressorWeapons = aggressorArmy.UserWeapons.UserWeapons.Where(u => Weapon.Weapons[u.WeaponType].ActionType  == ActionTypes.Offence).ToList();

        // Attacker equipment losses.
        foreach (var userWeapon in aggressorWeapons)
        {
            var currentWeapon = newAggressorWeapons.Where(u => u.WeaponType == userWeapon.WeaponType).First();
            for (int i = 0; i < userWeapon.Count; ++i)
            {
                if (Utility.Rng.NextDouble() <= aggressorLossChance)
                    --currentWeapon.Count;
            }
        }

        (result, message) = await _dbService.UpdateUserWeapons(aggressorId, newAggressorWeapons.Select(u => (aggressorWeapons.Where(v => v.WeaponType == u.WeaponType).Select(w => w.Count).First() - u.Count, false, u.WeaponType)).ToArray());

        if (result != TaskResults.Success)
            throw new InvalidOperationException(message);

        List<UserWeapon> newDefenderWeapons = defenderArmy.UserWeapons.UserWeapons
                                        .Where(u => Weapon.Weapons[u.WeaponType].ActionType  == ActionTypes.Defence)
                                        .Select(u => new UserWeapon() { Count = u.Count, WeaponType = u.WeaponType }).ToList();

        var defenderWeapons = defenderArmy.UserWeapons.UserWeapons.Where(u => Weapon.Weapons[u.WeaponType].ActionType  == ActionTypes.Defence).ToList();

        // Defender equipment losses.
        foreach (var userWeapon in defenderWeapons)
        {
            var currentWeapon = newDefenderWeapons.Where(u => u.WeaponType == userWeapon.WeaponType).First();
            for (int i = 0; i < userWeapon.Count; ++i)
            {
                if (Utility.Rng.NextDouble() <= defenderLossChance)
                    --currentWeapon.Count;
            }
        }

        (result, message) = await _dbService.UpdateUserWeapons(defenderId, newDefenderWeapons.Select(u => (defenderWeapons.Where(v => v.WeaponType == u.WeaponType).Select(u => u.Count).First() - u.Count, false, u.WeaponType)).ToArray());

        // TODO This should really be done within a database transaction.
        if (result is not TaskResults.Success)
            throw new InvalidOperationException(message);

        #endregion

        #region Unit Losses
        // Attacker recruit losses.
        for (int i = 0; i < aggressorArmy.Recruits; ++i)
        {
            if (Utility.Rng.NextDouble() <= aggressorLossChance * RecruitAttritionRate)
                --aggressorArmy.Recruits;
        }

        // Attacker trained losses.
        for (int i = 0; i < aggressorArmy.Attackers; ++i)
        {
            if (Utility.Rng.NextDouble() <= aggressorLossChance * TrainedAttritionRate)
                --aggressorArmy.Attackers;
        }

        (result, message) = await _dbService.UpdateSoldierCount(aggressorId, aggressorArmy.Recruits, aggressorArmy.Attackers, aggressorArmy.Defenders, aggressorArmy.Sentries, aggressorArmy.Sappers);

        if (result != TaskResults.Success)
            throw new InvalidOperationException(message);


        // Defender recruit losses.
        for (int i = 0; i < defenderArmy.Recruits; ++i)
        {
            if (Utility.Rng.NextDouble() <= defenderLossChance * RecruitAttritionRate)
                --defenderArmy.Recruits;
        }

        // Defender trained losses.
        for (int i = 0; i < defenderArmy.Defenders; ++i)
        {
            if (Utility.Rng.NextDouble() <= defenderLossChance * TrainedAttritionRate)
                --defenderArmy.Defenders;
        }

        (result, message) = await _dbService.UpdateSoldierCount(defenderId, defenderArmy.Recruits, defenderArmy.Attackers, defenderArmy.Defenders, defenderArmy.Sentries, defenderArmy.Sappers);

        if (result != TaskResults.Success)
            throw new InvalidOperationException(message);

        #endregion

        #region Coin Calculations
        int coins;
        if (outcome.Outcome > 1)
        {
            coins = defenderArmy.Coins / 2;

            // TODO Actually catch the db result.
            (_, _) = await _dbService.UpdateCoins(aggressorId, aggressorArmy.Coins + coins);
            (_, _) = await _dbService.UpdateCoins(defenderId, defenderArmy.Coins - coins);
        }
        else
        {
            coins = defenderArmy.Coins / 10;
            
            // TODO Actually catch the db result.
            (_, _) = await _dbService.UpdateCoins(aggressorId, aggressorArmy.Coins + coins);
            (_, _) = await _dbService.UpdateCoins(defenderId, defenderArmy.Coins - coins);
        }
        #endregion

        #region Generate View Model
        aggressorArmy = (await _dbService.GetArmyViewModel(aggressorId))!;
        defenderArmy = (await _dbService.GetArmyViewModel(defenderId))!;

        UserWeaponsData aggressorWeaponsData = new();

        foreach (var item in aggressorWeapons)
        {
            var newData = newAggressorWeapons.Where(u => u.WeaponType == item.WeaponType).First();
            UserWeapon newWeapon = new() { Count = item.Count - newData.Count, WeaponType = item.WeaponType };
            if (newWeapon.Count > 0)
                aggressorWeaponsData.UserWeapons.Add(newWeapon);
        }

        UserWeaponsData defenderWeaponsData = new();

        foreach (var item in defenderWeapons)
        {
            var newData = newDefenderWeapons.Where(u => u.WeaponType == item.WeaponType).First();
            UserWeapon newWeapon = new() { Count = item.Count - newData.Count, WeaponType = item.WeaponType };
            if (newWeapon.Count > 0)
                defenderWeaponsData.UserWeapons.Add(newWeapon);
        }

        AfterActionReportViewModel viewModel = new()
        {
            AggressorId = aggressorId,
            DefenderId = defenderId,
            BattleTime = DateTime.UtcNow,
            AggressorRecruitLosses = aggressorOriginalRecruits - aggressorArmy.Recruits,
            AggressorAttackerLosses = aggressorOriginalAttackers - aggressorArmy.Attackers,
            AggressorToolsLost = aggressorWeaponsData,
            DefenderCoinLosses = coins,
            DefenderRecruitLosses = defenderOriginalRecruits - defenderArmy.Recruits,
            DefenderDefenderLosses = defenderOriginalDefenders - defenderArmy.Defenders,
            DefenderToolsLost = defenderWeaponsData
        };
        #endregion

        // TODO Consume result.
        (_, _) = await _dbService.CreateAfterActionReport(viewModel);

        return (outcome.Message, viewModel);
    }

    public async Task<(string Message, SpyReportViewModel ViewModel)> SapUser(string aggressorId, string defenderId)
    {
        #region Setup
        TaskResults result = TaskResults.Invalid;
        string message = "";

        var aggressorArmy = await _dbService.GetArmyViewModel(aggressorId);
        var defenderArmy = await _dbService.GetArmyViewModel(defenderId);
        
        if (aggressorArmy is null || defenderArmy is null)
            throw new ArgumentException("Either the aggressor or defender does not have an army in the database.");
        
        var aggressorOriginalSappers = aggressorArmy.Sappers;
        var aggressorOriginalRecruits = aggressorArmy.Recruits;
        var defenderOriginalSentries = defenderArmy.Sentries;
        var defenderOriginalRecruits = defenderArmy.Recruits;

        var aggressorScore = ArmyScore.GetSapperScore(aggressorArmy, await _dbService.GetUserFaction(aggressorId));
        var defenderScore = ArmyScore.GetSentryScore(defenderArmy, await _dbService.GetUserFaction(defenderId));
        var defenderDefenceScore = ArmyScore.GetDefensiveScore(defenderArmy, await _dbService.GetUserFaction(defenderId));

        var outcome = _battleCalculator.CalculateSapperOutcome(aggressorScore, defenderScore, defenderDefenceScore);
        #endregion
        
        #region Equipment Losses

        double aggressorLossChance = 0;
        double defenderLossChance = 0;

        // Aggressor loss.
        if (outcome.Outcome < 1)
        {
            aggressorLossChance = 0.1;
            defenderLossChance = 0.025;
        }
        
        // Aggressor success.
        else
        {
            aggressorLossChance = 0.05;
            defenderLossChance = 0.05;
        }

        List<UserWeapon> newAggressorWeapons = aggressorArmy.UserWeapons.UserWeapons
                                                    .Where(u => Weapon.Weapons[u.WeaponType].ActionType  == ActionTypes.Sapping)
                                                    .Select(u => new UserWeapon() { Count = u.Count, WeaponType = u.WeaponType }).ToList();

        var aggressorWeapons = aggressorArmy.UserWeapons.UserWeapons.Where(u => Weapon.Weapons[u.WeaponType].ActionType  == ActionTypes.Sapping).ToList();

        // Attacker equipment losses.
        foreach (var userWeapon in aggressorWeapons)
        {
            var currentWeapon = newAggressorWeapons.Where(u => u.WeaponType == userWeapon.WeaponType).First();
            for (int i = 0; i < userWeapon.Count; ++i)
            {
                if (Utility.Rng.NextDouble() <= aggressorLossChance)
                    --currentWeapon.Count;
            }
        }

        (result, message) = await _dbService.UpdateUserWeapons(aggressorId, newAggressorWeapons.Select(u => (aggressorWeapons.Where(v => v.WeaponType == u.WeaponType).Select(w => w.Count).First() - u.Count, false, u.WeaponType)).ToArray());

        if (result != TaskResults.Success)
            throw new InvalidOperationException(message);

        List<UserWeapon> newDefenderWeapons = defenderArmy.UserWeapons.UserWeapons
                                                .Select(u => new UserWeapon() { Count = u.Count, WeaponType = u.WeaponType }).ToList();

        var defenderWeapons = defenderArmy.UserWeapons.UserWeapons.ToList();

        // Defender equipment losses.
        foreach (var userWeapon in defenderWeapons)
        {
            var currentWeapon = newDefenderWeapons.Where(u => u.WeaponType == userWeapon.WeaponType).First();
            for (int i = 0; i < userWeapon.Count; ++i)
            {
                if (Utility.Rng.NextDouble() <= defenderLossChance)
                    --currentWeapon.Count;
            }
        }

        (result, message) = await _dbService.UpdateUserWeapons(defenderId, newDefenderWeapons.Select(u => (defenderWeapons.Where(v => v.WeaponType == u.WeaponType).Select(u => u.Count).First() - u.Count, false, u.WeaponType)).ToArray());

        // TODO This should really be done within a database transaction.
        if (result is not TaskResults.Success)
            throw new InvalidOperationException(message);

        #endregion
        
        #region Unit Losses
        // Attacker recruit losses.
        for (int i = 0; i < aggressorArmy.Recruits; ++i)
        {
            if (Utility.Rng.NextDouble() <= aggressorLossChance * RecruitAttritionRate)
                --aggressorArmy.Recruits;
        }

        // Attacker trained losses.
        for (int i = 0; i < aggressorArmy.Sappers; ++i)
        {
            if (Utility.Rng.NextDouble() <= aggressorLossChance * TrainedAttritionRate)
                --aggressorArmy.Sappers;
        }

        (result, message) = await _dbService.UpdateSoldierCount(aggressorId, aggressorArmy.Recruits, aggressorArmy.Attackers, aggressorArmy.Defenders, aggressorArmy.Sentries, aggressorArmy.Sappers);

        if (result != TaskResults.Success)
            throw new InvalidOperationException(message);
        
        // Defender recruit losses.
        for (int i = 0; i < defenderArmy.Recruits; ++i)
        {
            if (Utility.Rng.NextDouble() <= defenderLossChance * RecruitAttritionRate)
                --defenderArmy.Recruits;
        }

        // Defender trained losses.
        for (int i = 0; i < defenderArmy.Sentries; ++i)
        {
            if (Utility.Rng.NextDouble() <= defenderLossChance * TrainedAttritionRate)
                --defenderArmy.Sentries;
        }

        (result, message) = await _dbService.UpdateSoldierCount(defenderId, defenderArmy.Recruits, defenderArmy.Attackers, defenderArmy.Defenders, defenderArmy.Sentries, defenderArmy.Sappers);

        if (result != TaskResults.Success)
            throw new InvalidOperationException(message);

        #endregion
        
        #region Generate View Model
        aggressorArmy = (await _dbService.GetArmyViewModel(aggressorId))!;
        defenderArmy = (await _dbService.GetArmyViewModel(defenderId))!;

        UserWeaponsData aggressorWeaponsData = new();

        foreach (var item in aggressorWeapons)
        {
            var newData = newAggressorWeapons.Where(u => u.WeaponType == item.WeaponType).First();
            UserWeapon newWeapon = new() { Count = item.Count - newData.Count, WeaponType = item.WeaponType };
            if (newWeapon.Count > 0)
                aggressorWeaponsData.UserWeapons.Add(newWeapon);
        }

        UserWeaponsData defenderWeaponsData = new();

        foreach (var item in defenderWeapons)
        {
            var newData = newDefenderWeapons.Where(u => u.WeaponType == item.WeaponType).First();
            UserWeapon newWeapon = new() { Count = item.Count - newData.Count, WeaponType = item.WeaponType };
            if (newWeapon.Count > 0)
                defenderWeaponsData.UserWeapons.Add(newWeapon);
        }

        aggressorArmy = await _dbService.GetArmyViewModel(aggressorId);
        defenderArmy = await _dbService.GetArmyViewModel(defenderId);
        
        if (aggressorArmy is null || defenderArmy is null)
            throw new ArgumentException("Either the aggressor or defender does not have an army in the database.");
        
        aggressorScore = ArmyScore.GetSapperScore(aggressorArmy, await _dbService.GetUserFaction(aggressorId));
        defenderScore = ArmyScore.GetSentryScore(defenderArmy, await _dbService.GetUserFaction(defenderId));
        defenderDefenceScore = ArmyScore.GetDefensiveScore(defenderArmy, await _dbService.GetUserFaction(defenderId));
        
        outcome = _battleCalculator.CalculateSapperOutcome(aggressorScore, defenderScore, defenderDefenceScore);

        SpyReportViewModel viewModel = new()
        {
            SapperId = aggressorId,
            SentryId = defenderId,
            SpyTime = DateTime.UtcNow,
            SapperRecruitLosses = aggressorOriginalRecruits - aggressorArmy.Recruits,
            SapperSapperLosses = aggressorOriginalSappers - aggressorArmy.Sappers,
            SapperToolsLost = aggressorWeaponsData,
            SentryRecruitLosses = defenderOriginalRecruits - defenderArmy.Recruits,
            SentrySentryLosses = defenderOriginalSentries - defenderArmy.Sentries,
            SentryToolsLost = defenderWeaponsData,
            SentryMaximumDefence = outcome.DefenceRange.Max,
            SentryMinimumDefence = outcome.DefenceRange.Min,
            SapperStrength = aggressorScore
        };
        #endregion

        // TODO Consume result.
        (_, _) = await _dbService.CreateSpyReport(viewModel);

        return (outcome.Message, viewModel);
    }
}
