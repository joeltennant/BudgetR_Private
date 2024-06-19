using BudgetR.Core.Enums;
using BudgetR.Core.Extensions;
using BudgetR.Server.Domain.Entities;
using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;

namespace BudgetR.Server.Services.Transactions.Helpers;
public class FileLoaderHelper
{
    public List<TransactionBatchDto>? TransactionBatches { get; set; }
    private readonly BudgetRDbContext _context;

    public FileLoaderHelper(BudgetRDbContext context)
    {
        TransactionBatches = new();
        _context = context;
    }

    /// <summary>
    /// Load files for transactions
    /// </summary>
    /// <returns></returns>
    public FileLoaderHelper LoadFiles(long HouseholdId)
    {
        string? incomingDirectoryPath = _context.HouseholdParameters
            .Where(p => p.HouseholdId == HouseholdId && p.HouseholdParameterType == HouseholdParameterType.IncomingPath)
            .Select(p => p.Value)
            .Single();

        string? downloadDirectoryPath = _context.HouseholdParameters
            .Where(p => p.HouseholdId == HouseholdId && p.HouseholdParameterType == HouseholdParameterType.DownloadPath)
            .Select(p => p.Value)
            .Single();

        string? archiveDirectoryPath = _context.HouseholdParameters
            .Where(p => p.HouseholdId == HouseholdId && p.HouseholdParameterType == HouseholdParameterType.ArchivePath)
            .Select(p => p.Value)
            .Single();

        string? failedDirectoryPath = _context.HouseholdParameters
            .Where(p => p.HouseholdId == HouseholdId && p.HouseholdParameterType == HouseholdParameterType.FailedPath)
            .Select(p => p.Value)
            .Single();

        RetrieveAndMoveFilesFromDownloadToIncoming(downloadDirectoryPath, incomingDirectoryPath);

        List<string> incomingFiles = new DirectoryInfo(incomingDirectoryPath)
                    .GetFiles()
                    .Where(f => f.Extension == ".csv")
                    .Select(f => f.FullName)
                    .ToList();

        if (incomingFiles.IsPopulated())
        {
            foreach (var fileName in incomingFiles)
            {
                var transactionBatch = new TransactionBatchDto
                {
                    FileName = fileName,
                    HouseholdId = HouseholdId,
                    Source = BatchSource.Batch
                };

                TransactionBatches.Add(transactionBatch);
            }
        }

        foreach (var batch in TransactionBatches)
        {
            batch.Transactions = GetTransactionData(batch.FileName);
        }

        return this;
    }

    public List<TransactionData> GetTransactionData(string fileName)
    {
        var transactionData = new List<TransactionData>();
        CsvConfiguration? csvConfig = new(CultureInfo.InvariantCulture)
        {
            PrepareHeaderForMatch = args => args.Header.Replace(" ", ""),
        };

        using (var reader = new StreamReader(fileName))
        using (var csv = new CsvReader(reader, csvConfig))
        {
            var result = csv.GetRecords<TransactionData>();
            transactionData.AddRange(result);
        }

        return transactionData;
    }

    private void RetrieveAndMoveFilesFromDownloadToIncoming(string? downloadDirectoryPath, string? incomingDirectoryPath)
    {
        var downloadDirectory = new DirectoryInfo(downloadDirectoryPath);

        List<string> dowloadFiles = downloadDirectory
            .GetFiles()
            .Where(f => f.Extension == ".csv" && f.Name.ToLower().Contains("export"))
            .Select(f => f.FullName)
            .ToList();

        var today = DateTime.Now.ToString("yyyy-MM-dd");

        if (dowloadFiles.IsPopulated())
        {
            foreach (var fileName in dowloadFiles)
            {
                string newGuid = Guid.NewGuid().ToString();
                string newFileName = $"{today}.{newGuid}.csv";
                Directory.Move(fileName, Path.Combine(incomingDirectoryPath, newFileName));
            }
        }
    }
}

public class TransactionData
{
    public string Status { get; set; }
    public DateOnly? Date { get; set; }
    public string OriginalDescription { get; set; }
    public string SplitType { get; set; }
    public string Category { get; set; }
    public string Currency { get; set; }
    public decimal Amount { get; set; }
    public string UserDescription { get; set; }
    public string Memo { get; set; }
    public string Classification { get; set; }
    public string AccountName { get; set; }
    public string SimpleDescription { get; set; }
}

public class TransactionBatchDto
{
    public long TransactionBatchId { get; set; }
    public DateTime? StartedAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? RecordCount { get; set; }
    public BatchSource? Source { get; set; }
    public string? FileName { get; set; }
    public ICollection<TransactionData>? Transactions { get; set; }
    public long HouseholdId { get; set; }
}