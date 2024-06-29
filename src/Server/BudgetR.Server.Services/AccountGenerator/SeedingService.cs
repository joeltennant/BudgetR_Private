using BudgetR.Core.Enums;
using BudgetR.Core.Models;
using BudgetR.Server.Domain.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.EntityFrameworkCore;
using System.Globalization;

namespace BudgetR.Server.Services.AccountGenerator;
public class SeedingService
{
    private readonly BudgetRDbContext _context;
    public SeedingService(BudgetRDbContext context)
    {
        _context = context;
    }

    public async Task Execute()
    {
        var transaction = await _context.BeginTransactionContext();
        try
        {
            long btaId = await CreateBta();
            SeedConfigurationDto config = GetConfigurationData();

            var userHouseholdIds = await CreateNewUserAndHousehold(config.FirstName, config.LastName, config.AuthId, btaId, config.HouseholdName);

            await AddHouseHoldParameters(config, userHouseholdIds.HouseholdId);

            var buildCategories = new BuildCategoriesFromTransactions(_context);
            await buildCategories.Build(userHouseholdIds.HouseholdId);

            var buildAccounts = new BuildAccountsFromTransactions(_context);
            await buildAccounts.Build(userHouseholdIds.HouseholdId);

            await _context.CommitTransactionContext(transaction);
        }
        catch (Exception ex)
        {
            await transaction.DisposeAsync();
            var transactionStatus = transaction;
            throw;
        }
    }

    private async Task AddHouseHoldParameters(SeedConfigurationDto config, long householdId)
    {
        var hasIncomePath = await _context.HouseholdParameters
                .AnyAsync(h => h.HouseholdId == householdId
                            && h.HouseholdParameterType == HouseholdParameterType.IncomingPath);

        if (!hasIncomePath)
        {
            await _context.AddAsync(new HouseholdParameter
            {
                HouseholdId = householdId,
                HouseholdParameterType = HouseholdParameterType.IncomingPath,
                Value = config.IncomingPath
            });
            await _context.SaveChangesAsync();
        }

        var hasDownloadPath = await _context.HouseholdParameters
                .AnyAsync(h => h.HouseholdId == householdId
                            && h.HouseholdParameterType == HouseholdParameterType.DownloadPath);
        if (!hasDownloadPath)
        {
            await _context.AddAsync(new HouseholdParameter
            {
                HouseholdId = householdId,
                HouseholdParameterType = HouseholdParameterType.DownloadPath,
                Value = config.DownloadPath
            });
            await _context.SaveChangesAsync();
        }

        var hasArchivePath = await _context.HouseholdParameters
                .AnyAsync(h => h.HouseholdId == householdId
                            && h.HouseholdParameterType == HouseholdParameterType.ArchivePath);

        if (!hasArchivePath)
        {
            await _context.AddAsync(new HouseholdParameter
            {
                HouseholdId = householdId,
                HouseholdParameterType = HouseholdParameterType.ArchivePath,
                Value = config.Archivepath
            });
            await _context.SaveChangesAsync();
        }

        var hasFailedPath = await _context.HouseholdParameters
        .AnyAsync(h => h.HouseholdId == householdId
                    && h.HouseholdParameterType == HouseholdParameterType.FailedPath);

        if (!hasFailedPath)
        {
            await _context.AddAsync(new HouseholdParameter
            {
                HouseholdId = householdId,
                HouseholdParameterType = HouseholdParameterType.FailedPath,
                Value = config.Archivepath
            });
            await _context.SaveChangesAsync();
        }
    }

    private async Task<(long UserId, long HouseholdId)> CreateNewUserAndHousehold(string? firstName, string? lastName, string? authId, long btaId, string HouseholdName)
    {
        var user = await _context.Users
            .Where(u => u.AuthenticationId == authId)
            .Select(u => new User { UserId = u.UserId, HouseholdId = u.HouseholdId })
            .FirstOrDefaultAsync();

        if (user != null)
        {
            return new(user.UserId, user.HouseholdId.Value);
        }

        Household household = new()
        {
            Name = HouseholdName,
            BusinessTransactionActivityId = btaId,
        };

        await _context.Households.AddAsync(household);
        await _context.SaveChangesAsync();

        await _context.AddRangeAsync(BuildMonthBudgetList(household.HouseholdId));
        await _context.SaveChangesAsync();

        var newUser = new User
        {
            FirstName = firstName,
            LastName = lastName,
            AuthenticationId = authId,
            IsActive = true,
            UserType = UserType.User,
            BusinessTransactionActivityId = btaId,
            HouseholdId = household.HouseholdId
        };

        await _context.AddAsync(newUser);
        await _context.SaveChangesAsync();

        return new(newUser.UserId, household.HouseholdId);
    }

    protected async Task<long> CreateBta()
    {
        long userId = 1;

        var bta = new BusinessTransactionActivity
        {
            ProcessName = "Seeding from Console",
            UserId = userId,
            CreatedAt = DateTime.UtcNow
        };

        await _context.BusinessTransactionActivities.AddAsync(bta);
        await _context.SaveChangesAsync();

        return bta.BusinessTransactionActivityId;
    }

    private SeedConfigurationDto GetConfigurationData()
    {
        SeedConfigurationDto seedConfiguration;
        CsvConfiguration? csvConfig = new(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.Replace(" ", ""),
        };

        using (var reader = new StreamReader("Configuration.csv"))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            var result = csv.GetRecords<SeedConfigurationDto>();
            seedConfiguration = result.First();
        }

        return seedConfiguration;
    }

    private List<BudgetMonth> BuildMonthBudgetList(long HouseholdId)
    {
        var today = DateOnly.FromDateTime(DateTime.Now);

        List<MonthYear> monthYears = _context.MonthYears
            .Where(m => m.IsActive)
            .OrderBy(m => m.MonthYearId)
            .ToList();

        List<BudgetMonth> monthBudgets = new();

        foreach (var monthYear in monthYears)
        {
            monthBudgets.Add(new BudgetMonth
            {
                MonthYearId = monthYear.MonthYearId,
                HouseholdId = HouseholdId,
            });
        }

        return monthBudgets;
    }
}
