﻿using BudgetR.Server.Services.Transactions.Helpers;

namespace BudgetR.Server.Services.Transactions;
public class TransactionService
{
    private readonly BudgetRDbContext _context;
    public TransactionService(BudgetRDbContext context)
    {
        _context = context;
    }

    public void LoadAndProcessTransactions()
    {
        var _fileLoader = new FileLoaderHelper(_context);
    }

    public void ReProcessTransactions()
    {

    }
}
