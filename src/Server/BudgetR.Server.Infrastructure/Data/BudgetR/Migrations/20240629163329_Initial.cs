using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetR.Server.Infrastructure.Data.BudgetR.Migrations;

/// <inheritdoc />
public partial class Initial : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "AccountTypes",
            columns: table => new
            {
                AccountTypeId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                BalanceType = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_AccountTypes", x => x.AccountTypeId);
            });

        migrationBuilder.CreateTable(
            name: "Logs",
            columns: table => new
            {
                LogId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                LogType = table.Column<int>(type: "int", nullable: false),
                IsHidden = table.Column<bool>(type: "bit", nullable: false),
                HouseholdId = table.Column<long>(type: "bigint", nullable: false),
                UserId = table.Column<long>(type: "bigint", nullable: true),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true),
                Created = table.Column<DateTime>(type: "datetime2", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Logs", x => x.LogId);
            });

        migrationBuilder.CreateTable(
            name: "TransactionCategories",
            columns: table => new
            {
                TransactionCategoryId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CategoryName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                HouseholdId = table.Column<long>(type: "bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TransactionCategories", x => x.TransactionCategoryId);
            });

        migrationBuilder.CreateTable(
            name: "Accounts",
            columns: table => new
            {
                AccountId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1")
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                Name = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                LongName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                Balance = table.Column<decimal>(type: "decimal(19,2)", precision: 19, scale: 2, nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                AccountTypeId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                HouseholdId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Accounts", x => x.AccountId);
                table.CheckConstraint("CK_Account_Balance", "[Balance] >= 0");
                table.ForeignKey(
                    name: "FK_Accounts_AccountTypes_AccountTypeId",
                    column: x => x.AccountTypeId,
                    principalTable: "AccountTypes",
                    principalColumn: "AccountTypeId",
                    onDelete: ReferentialAction.Restrict);
            })
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.CreateTable(
            name: "BudgetMonths",
            columns: table => new
            {
                BudgetMonthId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1")
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "BudgetMonthHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                MonthYearId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "BudgetMonthHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                IncomeTotal = table.Column<decimal>(type: "decimal(19,2)", precision: 19, scale: 2, nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "BudgetMonthHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ExpenseTotal = table.Column<decimal>(type: "decimal(19,2)", precision: 19, scale: 2, nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "BudgetMonthHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                HouseholdId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "BudgetMonthHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "BudgetMonthHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "BudgetMonthHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "BudgetMonthHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "BudgetMonthHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BudgetMonths", x => x.BudgetMonthId);
            })
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "BudgetMonthHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.CreateTable(
            name: "BusinessTransactionActivities",
            columns: table => new
            {
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                UserId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_BusinessTransactionActivities", x => x.BusinessTransactionActivityId);
            });

        migrationBuilder.CreateTable(
            name: "Expenses",
            columns: table => new
            {
                ExpenseId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1")
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                Name = table.Column<string>(type: "nvarchar(125)", maxLength: 125, nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                Amount = table.Column<decimal>(type: "decimal(19,2)", precision: 19, scale: 2, nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                HouseholdId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Expenses", x => x.ExpenseId);
                table.ForeignKey(
                    name: "FK_Expenses_BusinessTransactionActivities_BusinessTransactionActivityId",
                    column: x => x.BusinessTransactionActivityId,
                    principalTable: "BusinessTransactionActivities",
                    principalColumn: "BusinessTransactionActivityId");
            })
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.CreateTable(
            name: "Households",
            columns: table => new
            {
                HouseholdId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1")
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "HouseholdHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "HouseholdHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "HouseholdHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "HouseholdHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "HouseholdHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "HouseholdHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Households", x => x.HouseholdId);
                table.ForeignKey(
                    name: "FK_Households_BusinessTransactionActivities_BusinessTransactionActivityId",
                    column: x => x.BusinessTransactionActivityId,
                    principalTable: "BusinessTransactionActivities",
                    principalColumn: "BusinessTransactionActivityId");
            })
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "HouseholdHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.CreateTable(
            name: "Incomes",
            columns: table => new
            {
                IncomeId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1")
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                HouseholdId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                Amount = table.Column<decimal>(type: "decimal(19,2)", precision: 19, scale: 2, nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Incomes", x => x.IncomeId);
                table.ForeignKey(
                    name: "FK_Incomes_BusinessTransactionActivities_BusinessTransactionActivityId",
                    column: x => x.BusinessTransactionActivityId,
                    principalTable: "BusinessTransactionActivities",
                    principalColumn: "BusinessTransactionActivityId");
            })
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "IncomeHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.CreateTable(
            name: "MonthYears",
            columns: table => new
            {
                MonthYearId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Month = table.Column<int>(type: "int", nullable: false),
                Year = table.Column<int>(type: "int", nullable: false),
                IsActive = table.Column<bool>(type: "bit", nullable: false),
                NumberOfDays = table.Column<int>(type: "int", nullable: false),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_MonthYears", x => x.MonthYearId);
                table.ForeignKey(
                    name: "FK_MonthYears_BusinessTransactionActivities_BusinessTransactionActivityId",
                    column: x => x.BusinessTransactionActivityId,
                    principalTable: "BusinessTransactionActivities",
                    principalColumn: "BusinessTransactionActivityId");
            });

        migrationBuilder.CreateTable(
            name: "TransactionBatches",
            columns: table => new
            {
                TransactionBatchId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                StartedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                CompletedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                RecordCount = table.Column<int>(type: "int", nullable: true),
                Source = table.Column<int>(type: "int", nullable: true),
                FileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                HouseholdId = table.Column<long>(type: "bigint", nullable: false),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TransactionBatches", x => x.TransactionBatchId);
                table.ForeignKey(
                    name: "FK_TransactionBatches_BusinessTransactionActivities_BusinessTransactionActivityId",
                    column: x => x.BusinessTransactionActivityId,
                    principalTable: "BusinessTransactionActivities",
                    principalColumn: "BusinessTransactionActivityId");
            });

        migrationBuilder.CreateTable(
            name: "TransactionCategoryRules",
            columns: table => new
            {
                TransactionCategoryRuleId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                CategoryRuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                CategoryId = table.Column<long>(type: "bigint", nullable: true),
                Rule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ComparisonType = table.Column<int>(type: "int", nullable: false),
                TransactionType = table.Column<int>(type: "int", nullable: true),
                HouseholdId = table.Column<long>(type: "bigint", nullable: false),
                TransactionCategoryId = table.Column<long>(type: "bigint", nullable: true),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TransactionCategoryRules", x => x.TransactionCategoryRuleId);
                table.ForeignKey(
                    name: "FK_TransactionCategoryRules_BusinessTransactionActivities_BusinessTransactionActivityId",
                    column: x => x.BusinessTransactionActivityId,
                    principalTable: "BusinessTransactionActivities",
                    principalColumn: "BusinessTransactionActivityId");
                table.ForeignKey(
                    name: "FK_TransactionCategoryRules_TransactionCategories_TransactionCategoryId",
                    column: x => x.TransactionCategoryId,
                    principalTable: "TransactionCategories",
                    principalColumn: "TransactionCategoryId");
            });

        migrationBuilder.CreateTable(
            name: "TransactionTypeRules",
            columns: table => new
            {
                TransactionTypeRuleId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                TransactionTypeRuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                NumericRule = table.Column<long>(type: "bigint", nullable: true),
                StringRule = table.Column<string>(type: "nvarchar(max)", nullable: true),
                ComparisonType = table.Column<int>(type: "int", nullable: false),
                RuleLevel = table.Column<int>(type: "int", nullable: false),
                AssignTransactionType = table.Column<int>(type: "int", nullable: true),
                RuleOnTransactionType = table.Column<int>(type: "int", nullable: true),
                HouseholdId = table.Column<long>(type: "bigint", nullable: false),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TransactionTypeRules", x => x.TransactionTypeRuleId);
                table.ForeignKey(
                    name: "FK_TransactionTypeRules_BusinessTransactionActivities_BusinessTransactionActivityId",
                    column: x => x.BusinessTransactionActivityId,
                    principalTable: "BusinessTransactionActivities",
                    principalColumn: "BusinessTransactionActivityId");
            });

        migrationBuilder.CreateTable(
            name: "ExpenseDetails",
            columns: table => new
            {
                ExpenseDetailId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1")
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ExpenseId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                BudgetMonthId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                AmountAllocated = table.Column<decimal>(type: "decimal(19,2)", precision: 19, scale: 2, nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ExpenseDetails", x => x.ExpenseDetailId);
                table.ForeignKey(
                    name: "FK_ExpenseDetails_BudgetMonths_BudgetMonthId",
                    column: x => x.BudgetMonthId,
                    principalTable: "BudgetMonths",
                    principalColumn: "BudgetMonthId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_ExpenseDetails_BusinessTransactionActivities_BusinessTransactionActivityId",
                    column: x => x.BusinessTransactionActivityId,
                    principalTable: "BusinessTransactionActivities",
                    principalColumn: "BusinessTransactionActivityId");
                table.ForeignKey(
                    name: "FK_ExpenseDetails_Expenses_ExpenseId",
                    column: x => x.ExpenseId,
                    principalTable: "Expenses",
                    principalColumn: "ExpenseId",
                    onDelete: ReferentialAction.Restrict);
            })
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseDetailHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.CreateTable(
            name: "FlaggedTransactions",
            columns: table => new
            {
                FlaggedTransactionId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                AccountId = table.Column<long>(type: "bigint", nullable: true),
                TransactionType = table.Column<int>(type: "int", nullable: true),
                Amount = table.Column<decimal>(type: "decimal(19,2)", precision: 19, scale: 2, nullable: false),
                TransactionDate = table.Column<DateOnly>(type: "date", nullable: true),
                TransactionMonth = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                TransactionYear = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                TransactionCategoryId = table.Column<long>(type: "bigint", nullable: true),
                TransactionBatchId = table.Column<long>(type: "bigint", nullable: true),
                HouseholdId = table.Column<long>(type: "bigint", nullable: false),
                FlagType = table.Column<int>(type: "int", nullable: false),
                Complete = table.Column<bool>(type: "bit", nullable: false),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_FlaggedTransactions", x => x.FlaggedTransactionId);
                table.ForeignKey(
                    name: "FK_FlaggedTransactions_BusinessTransactionActivities_BusinessTransactionActivityId",
                    column: x => x.BusinessTransactionActivityId,
                    principalTable: "BusinessTransactionActivities",
                    principalColumn: "BusinessTransactionActivityId");
                table.ForeignKey(
                    name: "FK_FlaggedTransactions_Households_HouseholdId",
                    column: x => x.HouseholdId,
                    principalTable: "Households",
                    principalColumn: "HouseholdId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_FlaggedTransactions_TransactionCategories_TransactionCategoryId",
                    column: x => x.TransactionCategoryId,
                    principalTable: "TransactionCategories",
                    principalColumn: "TransactionCategoryId");
            });

        migrationBuilder.CreateTable(
            name: "HouseholdParameters",
            columns: table => new
            {
                HouseholdParameterId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                HouseholdParameterType = table.Column<int>(type: "int", nullable: true),
                Value = table.Column<string>(type: "nvarchar(max)", nullable: true),
                HouseholdId = table.Column<long>(type: "bigint", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_HouseholdParameters", x => x.HouseholdParameterId);
                table.ForeignKey(
                    name: "FK_HouseholdParameters_Households_HouseholdId",
                    column: x => x.HouseholdId,
                    principalTable: "Households",
                    principalColumn: "HouseholdId",
                    onDelete: ReferentialAction.Restrict);
            });

        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                UserId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1")
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                AuthenticationId = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                LastName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                HouseholdId = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                UserType = table.Column<int>(type: "int", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.UserId);
                table.ForeignKey(
                    name: "FK_Users_Households_HouseholdId",
                    column: x => x.HouseholdId,
                    principalTable: "Households",
                    principalColumn: "HouseholdId");
            })
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.CreateTable(
            name: "IncomeDetails",
            columns: table => new
            {
                IncomeDetailId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1")
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                IncomeId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                BudgetMonthId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                IsActive = table.Column<bool>(type: "bit", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                BusinessTransactionActivityId = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt"),
                ModifiedBy = table.Column<long>(type: "bigint", nullable: true)
                    .Annotation("SqlServer:IsTemporal", true)
                    .Annotation("SqlServer:TemporalHistoryTableName", "IncomeDetailHistory")
                    .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                    .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                    .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_IncomeDetails", x => x.IncomeDetailId);
                table.ForeignKey(
                    name: "FK_IncomeDetails_BudgetMonths_BudgetMonthId",
                    column: x => x.BudgetMonthId,
                    principalTable: "BudgetMonths",
                    principalColumn: "BudgetMonthId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_IncomeDetails_BusinessTransactionActivities_BusinessTransactionActivityId",
                    column: x => x.BusinessTransactionActivityId,
                    principalTable: "BusinessTransactionActivities",
                    principalColumn: "BusinessTransactionActivityId");
                table.ForeignKey(
                    name: "FK_IncomeDetails_Incomes_IncomeId",
                    column: x => x.IncomeId,
                    principalTable: "Incomes",
                    principalColumn: "IncomeId",
                    onDelete: ReferentialAction.Restrict);
            })
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "IncomeDetailHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.CreateTable(
            name: "Transactions",
            columns: table => new
            {
                TransactionId = table.Column<long>(type: "bigint", nullable: false)
                    .Annotation("SqlServer:Identity", "1, 1"),
                Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                AccountId = table.Column<long>(type: "bigint", nullable: true),
                TransactionType = table.Column<int>(type: "int", nullable: true),
                Amount = table.Column<decimal>(type: "decimal(19,2)", precision: 19, scale: 2, nullable: false),
                TransactionDate = table.Column<DateOnly>(type: "date", nullable: true),
                TransactionMonth = table.Column<int>(type: "int", maxLength: 2, nullable: false),
                TransactionYear = table.Column<int>(type: "int", maxLength: 4, nullable: false),
                TransactionCategoryId = table.Column<long>(type: "bigint", nullable: true),
                TransactionBatchId = table.Column<long>(type: "bigint", nullable: true),
                HouseholdId = table.Column<long>(type: "bigint", nullable: false),
                PreventReprocessing = table.Column<bool>(type: "bit", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Transactions", x => x.TransactionId);
                table.ForeignKey(
                    name: "FK_Transactions_Households_HouseholdId",
                    column: x => x.HouseholdId,
                    principalTable: "Households",
                    principalColumn: "HouseholdId",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_Transactions_TransactionBatches_TransactionBatchId",
                    column: x => x.TransactionBatchId,
                    principalTable: "TransactionBatches",
                    principalColumn: "TransactionBatchId");
                table.ForeignKey(
                    name: "FK_Transactions_TransactionCategories_TransactionCategoryId",
                    column: x => x.TransactionCategoryId,
                    principalTable: "TransactionCategories",
                    principalColumn: "TransactionCategoryId");
            });

        migrationBuilder.InsertData(
            table: "AccountTypes",
            columns: new[] { "AccountTypeId", "BalanceType", "Name" },
            values: new object[,]
            {
                { 1L, 0, "Checking" },
                { 2L, 0, "Savings" },
                { 3L, 1, "Credit Card" },
                { 4L, 0, "Cash" },
                { 5L, 0, "Investment" },
                { 6L, 1, "Loan" },
                { 7L, 0, "Other" },
                { 8L, 0, "Retirement" }
            });

        migrationBuilder.InsertData(
            table: "MonthYears",
            columns: new[] { "MonthYearId", "BusinessTransactionActivityId", "IsActive", "ModifiedBy", "Month", "NumberOfDays", "Year" },
            values: new object[,]
            {
                { 1L, null, true, null, 1, 31, 2024 },
                { 2L, null, true, null, 2, 29, 2024 },
                { 3L, null, true, null, 3, 31, 2024 },
                { 4L, null, true, null, 4, 30, 2024 },
                { 5L, null, true, null, 5, 31, 2024 },
                { 6L, null, true, null, 6, 30, 2024 },
                { 7L, null, true, null, 7, 31, 2024 },
                { 8L, null, true, null, 8, 31, 2024 },
                { 9L, null, true, null, 9, 30, 2024 },
                { 10L, null, true, null, 10, 31, 2024 },
                { 11L, null, true, null, 11, 30, 2024 },
                { 12L, null, true, null, 12, 31, 2024 },
                { 13L, null, true, null, 1, 31, 2025 },
                { 14L, null, true, null, 2, 28, 2025 },
                { 15L, null, true, null, 3, 31, 2025 },
                { 16L, null, true, null, 4, 30, 2025 },
                { 17L, null, true, null, 5, 31, 2025 },
                { 18L, null, true, null, 6, 30, 2025 },
                { 19L, null, true, null, 7, 31, 2025 },
                { 20L, null, true, null, 8, 31, 2025 },
                { 21L, null, true, null, 9, 30, 2025 },
                { 22L, null, true, null, 10, 31, 2025 },
                { 23L, null, true, null, 11, 30, 2025 },
                { 24L, null, true, null, 12, 31, 2025 },
                { 25L, null, true, null, 1, 31, 2026 },
                { 26L, null, true, null, 2, 28, 2026 },
                { 27L, null, true, null, 3, 31, 2026 },
                { 28L, null, true, null, 4, 30, 2026 },
                { 29L, null, true, null, 5, 31, 2026 },
                { 30L, null, true, null, 6, 30, 2026 },
                { 31L, null, true, null, 7, 31, 2026 },
                { 32L, null, true, null, 8, 31, 2026 },
                { 33L, null, true, null, 9, 30, 2026 },
                { 34L, null, true, null, 10, 31, 2026 },
                { 35L, null, true, null, 11, 30, 2026 },
                { 36L, null, true, null, 12, 31, 2026 },
                { 37L, null, true, null, 1, 31, 2027 },
                { 38L, null, true, null, 2, 28, 2027 },
                { 39L, null, true, null, 3, 31, 2027 },
                { 40L, null, true, null, 4, 30, 2027 },
                { 41L, null, true, null, 5, 31, 2027 },
                { 42L, null, true, null, 6, 30, 2027 },
                { 43L, null, true, null, 7, 31, 2027 },
                { 44L, null, true, null, 8, 31, 2027 },
                { 45L, null, true, null, 9, 30, 2027 },
                { 46L, null, true, null, 10, 31, 2027 },
                { 47L, null, true, null, 11, 30, 2027 },
                { 48L, null, true, null, 12, 31, 2027 },
                { 49L, null, true, null, 1, 31, 2028 },
                { 50L, null, true, null, 2, 29, 2028 },
                { 51L, null, true, null, 3, 31, 2028 },
                { 52L, null, true, null, 4, 30, 2028 },
                { 53L, null, true, null, 5, 31, 2028 },
                { 54L, null, true, null, 6, 30, 2028 },
                { 55L, null, true, null, 7, 31, 2028 },
                { 56L, null, true, null, 8, 31, 2028 },
                { 57L, null, true, null, 9, 30, 2028 },
                { 58L, null, true, null, 10, 31, 2028 },
                { 59L, null, true, null, 11, 30, 2028 },
                { 60L, null, true, null, 12, 31, 2028 },
                { 61L, null, true, null, 1, 31, 2029 },
                { 62L, null, true, null, 2, 28, 2029 },
                { 63L, null, true, null, 3, 31, 2029 },
                { 64L, null, true, null, 4, 30, 2029 },
                { 65L, null, true, null, 5, 31, 2029 },
                { 66L, null, true, null, 6, 30, 2029 },
                { 67L, null, true, null, 7, 31, 2029 },
                { 68L, null, true, null, 8, 31, 2029 },
                { 69L, null, true, null, 9, 30, 2029 },
                { 70L, null, true, null, 10, 31, 2029 },
                { 71L, null, true, null, 11, 30, 2029 },
                { 72L, null, true, null, 12, 31, 2029 },
                { 73L, null, true, null, 1, 31, 2030 },
                { 74L, null, true, null, 2, 28, 2030 },
                { 75L, null, true, null, 3, 31, 2030 },
                { 76L, null, true, null, 4, 30, 2030 },
                { 77L, null, true, null, 5, 31, 2030 },
                { 78L, null, true, null, 6, 30, 2030 },
                { 79L, null, true, null, 7, 31, 2030 },
                { 80L, null, true, null, 8, 31, 2030 },
                { 81L, null, true, null, 9, 30, 2030 },
                { 82L, null, true, null, 10, 31, 2030 },
                { 83L, null, true, null, 11, 30, 2030 },
                { 84L, null, true, null, 12, 31, 2030 },
                { 85L, null, true, null, 1, 31, 2031 },
                { 86L, null, true, null, 2, 28, 2031 },
                { 87L, null, true, null, 3, 31, 2031 },
                { 88L, null, true, null, 4, 30, 2031 },
                { 89L, null, true, null, 5, 31, 2031 },
                { 90L, null, true, null, 6, 30, 2031 },
                { 91L, null, true, null, 7, 31, 2031 },
                { 92L, null, true, null, 8, 31, 2031 },
                { 93L, null, true, null, 9, 30, 2031 },
                { 94L, null, true, null, 10, 31, 2031 },
                { 95L, null, true, null, 11, 30, 2031 },
                { 96L, null, true, null, 12, 31, 2031 },
                { 97L, null, true, null, 1, 31, 2032 },
                { 98L, null, true, null, 2, 29, 2032 },
                { 99L, null, true, null, 3, 31, 2032 },
                { 100L, null, true, null, 4, 30, 2032 },
                { 101L, null, true, null, 5, 31, 2032 },
                { 102L, null, true, null, 6, 30, 2032 },
                { 103L, null, true, null, 7, 31, 2032 },
                { 104L, null, true, null, 8, 31, 2032 },
                { 105L, null, true, null, 9, 30, 2032 },
                { 106L, null, true, null, 10, 31, 2032 },
                { 107L, null, true, null, 11, 30, 2032 },
                { 108L, null, true, null, 12, 31, 2032 },
                { 109L, null, true, null, 1, 31, 2033 },
                { 110L, null, true, null, 2, 28, 2033 },
                { 111L, null, true, null, 3, 31, 2033 },
                { 112L, null, true, null, 4, 30, 2033 },
                { 113L, null, true, null, 5, 31, 2033 },
                { 114L, null, true, null, 6, 30, 2033 },
                { 115L, null, true, null, 7, 31, 2033 },
                { 116L, null, true, null, 8, 31, 2033 },
                { 117L, null, true, null, 9, 30, 2033 },
                { 118L, null, true, null, 10, 31, 2033 },
                { 119L, null, true, null, 11, 30, 2033 },
                { 120L, null, true, null, 12, 31, 2033 }
            });

        migrationBuilder.InsertData(
            table: "Users",
            columns: new[] { "UserId", "AuthenticationId", "BusinessTransactionActivityId", "FirstName", "HouseholdId", "IsActive", "LastName", "ModifiedBy", "UserType" },
            values: new object[] { 1L, "", 1L, "System", null, false, null, null, 0 });

        migrationBuilder.InsertData(
            table: "BusinessTransactionActivities",
            columns: new[] { "BusinessTransactionActivityId", "CreatedAt", "ProcessName", "UserId" },
            values: new object[] { 1L, new DateTime(2024, 6, 29, 10, 33, 29, 542, DateTimeKind.Local).AddTicks(497), "Initial Seeding", 1L });

        migrationBuilder.CreateIndex(
            name: "IX_Accounts_AccountTypeId",
            table: "Accounts",
            column: "AccountTypeId");

        migrationBuilder.CreateIndex(
            name: "IX_Accounts_BusinessTransactionActivityId",
            table: "Accounts",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_Accounts_HouseholdId",
            table: "Accounts",
            column: "HouseholdId");

        migrationBuilder.CreateIndex(
            name: "IX_BudgetMonths_BusinessTransactionActivityId",
            table: "BudgetMonths",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_BudgetMonths_HouseholdId",
            table: "BudgetMonths",
            column: "HouseholdId");

        migrationBuilder.CreateIndex(
            name: "IX_BudgetMonths_MonthYearId",
            table: "BudgetMonths",
            column: "MonthYearId");

        migrationBuilder.CreateIndex(
            name: "IX_BusinessTransactionActivities_UserId",
            table: "BusinessTransactionActivities",
            column: "UserId",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_ExpenseDetails_BudgetMonthId",
            table: "ExpenseDetails",
            column: "BudgetMonthId");

        migrationBuilder.CreateIndex(
            name: "IX_ExpenseDetails_BusinessTransactionActivityId",
            table: "ExpenseDetails",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_ExpenseDetails_ExpenseId",
            table: "ExpenseDetails",
            column: "ExpenseId");

        migrationBuilder.CreateIndex(
            name: "IX_Expenses_BusinessTransactionActivityId",
            table: "Expenses",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_FlaggedTransactions_BusinessTransactionActivityId",
            table: "FlaggedTransactions",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_FlaggedTransactions_HouseholdId",
            table: "FlaggedTransactions",
            column: "HouseholdId");

        migrationBuilder.CreateIndex(
            name: "IX_FlaggedTransactions_TransactionCategoryId",
            table: "FlaggedTransactions",
            column: "TransactionCategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_HouseholdParameters_HouseholdId",
            table: "HouseholdParameters",
            column: "HouseholdId");

        migrationBuilder.CreateIndex(
            name: "IX_Households_BusinessTransactionActivityId",
            table: "Households",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_IncomeDetails_BudgetMonthId",
            table: "IncomeDetails",
            column: "BudgetMonthId");

        migrationBuilder.CreateIndex(
            name: "IX_IncomeDetails_BusinessTransactionActivityId",
            table: "IncomeDetails",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_IncomeDetails_IncomeId",
            table: "IncomeDetails",
            column: "IncomeId");

        migrationBuilder.CreateIndex(
            name: "IX_Incomes_BusinessTransactionActivityId",
            table: "Incomes",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_MonthYears_BusinessTransactionActivityId",
            table: "MonthYears",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_TransactionBatches_BusinessTransactionActivityId",
            table: "TransactionBatches",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_TransactionCategoryRules_BusinessTransactionActivityId",
            table: "TransactionCategoryRules",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_TransactionCategoryRules_TransactionCategoryId",
            table: "TransactionCategoryRules",
            column: "TransactionCategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_HouseholdId",
            table: "Transactions",
            column: "HouseholdId");

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_TransactionBatchId",
            table: "Transactions",
            column: "TransactionBatchId");

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_TransactionCategoryId",
            table: "Transactions",
            column: "TransactionCategoryId");

        migrationBuilder.CreateIndex(
            name: "IX_Transactions_TransactionMonth_TransactionYear",
            table: "Transactions",
            columns: new[] { "TransactionMonth", "TransactionYear" });

        migrationBuilder.CreateIndex(
            name: "IX_TransactionTypeRules_BusinessTransactionActivityId",
            table: "TransactionTypeRules",
            column: "BusinessTransactionActivityId");

        migrationBuilder.CreateIndex(
            name: "IX_Users_HouseholdId",
            table: "Users",
            column: "HouseholdId");

        migrationBuilder.AddForeignKey(
            name: "FK_Accounts_BusinessTransactionActivities_BusinessTransactionActivityId",
            table: "Accounts",
            column: "BusinessTransactionActivityId",
            principalTable: "BusinessTransactionActivities",
            principalColumn: "BusinessTransactionActivityId");

        migrationBuilder.AddForeignKey(
            name: "FK_Accounts_Households_HouseholdId",
            table: "Accounts",
            column: "HouseholdId",
            principalTable: "Households",
            principalColumn: "HouseholdId",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_BudgetMonths_BusinessTransactionActivities_BusinessTransactionActivityId",
            table: "BudgetMonths",
            column: "BusinessTransactionActivityId",
            principalTable: "BusinessTransactionActivities",
            principalColumn: "BusinessTransactionActivityId");

        migrationBuilder.AddForeignKey(
            name: "FK_BudgetMonths_Households_HouseholdId",
            table: "BudgetMonths",
            column: "HouseholdId",
            principalTable: "Households",
            principalColumn: "HouseholdId",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_BudgetMonths_MonthYears_MonthYearId",
            table: "BudgetMonths",
            column: "MonthYearId",
            principalTable: "MonthYears",
            principalColumn: "MonthYearId",
            onDelete: ReferentialAction.Restrict);

        migrationBuilder.AddForeignKey(
            name: "FK_BusinessTransactionActivities_Users_UserId",
            table: "BusinessTransactionActivities",
            column: "UserId",
            principalTable: "Users",
            principalColumn: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Households_BusinessTransactionActivities_BusinessTransactionActivityId",
            table: "Households");

        migrationBuilder.DropTable(
            name: "Accounts")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "AccountHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.DropTable(
            name: "ExpenseDetails")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseDetailHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.DropTable(
            name: "FlaggedTransactions");

        migrationBuilder.DropTable(
            name: "HouseholdParameters");

        migrationBuilder.DropTable(
            name: "IncomeDetails")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "IncomeDetailHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.DropTable(
            name: "Logs");

        migrationBuilder.DropTable(
            name: "TransactionCategoryRules");

        migrationBuilder.DropTable(
            name: "Transactions");

        migrationBuilder.DropTable(
            name: "TransactionTypeRules");

        migrationBuilder.DropTable(
            name: "AccountTypes");

        migrationBuilder.DropTable(
            name: "Expenses")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "ExpenseHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.DropTable(
            name: "BudgetMonths")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "BudgetMonthHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.DropTable(
            name: "Incomes")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "IncomeHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.DropTable(
            name: "TransactionBatches");

        migrationBuilder.DropTable(
            name: "TransactionCategories");

        migrationBuilder.DropTable(
            name: "MonthYears");

        migrationBuilder.DropTable(
            name: "BusinessTransactionActivities");

        migrationBuilder.DropTable(
            name: "Users")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "UserHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

        migrationBuilder.DropTable(
            name: "Households")
            .Annotation("SqlServer:IsTemporal", true)
            .Annotation("SqlServer:TemporalHistoryTableName", "HouseholdHistory")
            .Annotation("SqlServer:TemporalHistoryTableSchema", null)
            .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
            .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");
    }
}
