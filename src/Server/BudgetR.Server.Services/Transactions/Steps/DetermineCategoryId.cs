using BudgetR.Core;
using BudgetR.Core.Extensions;
using BudgetR.Server.Domain.Entities.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BudgetR.Server.Services.Transactions.Steps;
public class DetermineCategoryId : TransactionStepBase
{
    public DetermineCategoryId(BudgetRDbContext context, StateContainer stateContainer) : base(context, stateContainer)
    {
    }

    public override async Task<TransactionProcessorDto> Execute(TransactionProcessorDto transactionProcessor)
    {
        var categories = await _context.TransactionCategories
            .Where(t => t.HouseholdId == transactionProcessor.HouseholdId)
            .ToListAsync();

        var categoryRules = await _context.TransactionCategoryRules
            .Where(t => t.HouseholdId == transactionProcessor.HouseholdId)
            .ToListAsync();

        if (categoryRules.IsPopulated())
        {
            foreach (var transaction in transactionProcessor.TransactionBatchDto.Transactions)
            {
                var rule = categoryRules.FirstOrDefault(r => r.TransactionType == transaction.TransactionType
                                                            && transaction.OriginalDescription.Contains(r.Rule));

                if (rule != null)
                {
                    transaction.CategoryId = rule.CategoryId;
                }
            }
        }

        if (!transactionProcessor.TransactionBatchDto.Transactions.Any(t => t.CategoryId == null || t.CategoryId == 0))
        {
            return transactionProcessor;
        }

        foreach (var transaction in transactionProcessor.TransactionBatchDto.Transactions)
        {
            if (transaction.CategoryId == null || transaction.CategoryId == 0)
            {
                long? categoryId = await _context.TransactionCategories
                            .Where(t => t.HouseholdId == transactionProcessor.HouseholdId
                                        && t.CategoryName == transaction.Category)
                            .Select(t => t.TransactionCategoryId)
                            .FirstOrDefaultAsync();

                if (categoryId.HasValue && categoryId != 0)
                {
                    transaction.CategoryId = categoryId.Value;
                }
                else
                {
                    var newCategory = new TransactionCategory
                    {
                        CategoryName = transaction.Category,
                        HouseholdId = transactionProcessor.HouseholdId
                    };

                    await _context.TransactionCategories.AddAsync(newCategory);
                    await _context.SaveChangesAsync();

                    transaction.CategoryId = newCategory.TransactionCategoryId;
                }
            }
        }

        return transactionProcessor;
    }
}
