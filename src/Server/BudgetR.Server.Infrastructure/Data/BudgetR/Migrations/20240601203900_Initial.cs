using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace BudgetR.Server.Infrastructure.Data.BudgetR.Migrations
{
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
                    ProcessName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true),
                    UserId1 = table.Column<long>(type: "bigint", nullable: true)
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
                table: "BusinessTransactionActivities",
                columns: new[] { "BusinessTransactionActivityId", "CreatedAt", "ProcessName", "UserId", "UserId1" },
                values: new object[] { 1L, new DateTime(2024, 6, 1, 14, 38, 59, 626, DateTimeKind.Local).AddTicks(4398), "Initial Seeding", 1L, null });

            migrationBuilder.InsertData(
                table: "MonthYears",
                columns: new[] { "MonthYearId", "BusinessTransactionActivityId", "IsActive", "ModifiedBy", "Month", "NumberOfDays", "Year" },
                values: new object[,]
                {
                    { 1L, null, true, null, 1, 31, 2023 },
                    { 2L, null, true, null, 2, 28, 2023 },
                    { 3L, null, true, null, 3, 31, 2023 },
                    { 4L, null, true, null, 4, 30, 2023 },
                    { 5L, null, true, null, 5, 31, 2023 },
                    { 6L, null, true, null, 6, 30, 2023 },
                    { 7L, null, true, null, 7, 31, 2023 },
                    { 8L, null, true, null, 8, 31, 2023 },
                    { 9L, null, true, null, 9, 30, 2023 },
                    { 10L, null, true, null, 10, 31, 2023 },
                    { 11L, null, true, null, 11, 30, 2023 },
                    { 12L, null, true, null, 12, 31, 2023 },
                    { 13L, null, true, null, 1, 31, 2024 },
                    { 14L, null, true, null, 2, 29, 2024 },
                    { 15L, null, true, null, 3, 31, 2024 },
                    { 16L, null, true, null, 4, 30, 2024 },
                    { 17L, null, true, null, 5, 31, 2024 },
                    { 18L, null, true, null, 6, 30, 2024 },
                    { 19L, null, true, null, 7, 31, 2024 },
                    { 20L, null, true, null, 8, 31, 2024 },
                    { 21L, null, true, null, 9, 30, 2024 },
                    { 22L, null, true, null, 10, 31, 2024 },
                    { 23L, null, true, null, 11, 30, 2024 },
                    { 24L, null, true, null, 12, 31, 2024 },
                    { 25L, null, true, null, 1, 31, 2025 },
                    { 26L, null, true, null, 2, 28, 2025 },
                    { 27L, null, true, null, 3, 31, 2025 },
                    { 28L, null, true, null, 4, 30, 2025 },
                    { 29L, null, true, null, 5, 31, 2025 },
                    { 30L, null, true, null, 6, 30, 2025 },
                    { 31L, null, true, null, 7, 31, 2025 },
                    { 32L, null, true, null, 8, 31, 2025 },
                    { 33L, null, true, null, 9, 30, 2025 },
                    { 34L, null, true, null, 10, 31, 2025 },
                    { 35L, null, true, null, 11, 30, 2025 },
                    { 36L, null, true, null, 12, 31, 2025 },
                    { 37L, null, true, null, 1, 31, 2026 },
                    { 38L, null, true, null, 2, 28, 2026 },
                    { 39L, null, true, null, 3, 31, 2026 },
                    { 40L, null, true, null, 4, 30, 2026 },
                    { 41L, null, true, null, 5, 31, 2026 },
                    { 42L, null, true, null, 6, 30, 2026 },
                    { 43L, null, true, null, 7, 31, 2026 },
                    { 44L, null, true, null, 8, 31, 2026 },
                    { 45L, null, true, null, 9, 30, 2026 },
                    { 46L, null, true, null, 10, 31, 2026 },
                    { 47L, null, true, null, 11, 30, 2026 },
                    { 48L, null, true, null, 12, 31, 2026 },
                    { 49L, null, true, null, 1, 31, 2027 },
                    { 50L, null, true, null, 2, 28, 2027 },
                    { 51L, null, true, null, 3, 31, 2027 },
                    { 52L, null, true, null, 4, 30, 2027 },
                    { 53L, null, true, null, 5, 31, 2027 },
                    { 54L, null, true, null, 6, 30, 2027 },
                    { 55L, null, true, null, 7, 31, 2027 },
                    { 56L, null, true, null, 8, 31, 2027 },
                    { 57L, null, true, null, 9, 30, 2027 },
                    { 58L, null, true, null, 10, 31, 2027 },
                    { 59L, null, true, null, 11, 30, 2027 },
                    { 60L, null, true, null, 12, 31, 2027 },
                    { 61L, null, true, null, 1, 31, 2028 },
                    { 62L, null, true, null, 2, 29, 2028 },
                    { 63L, null, true, null, 3, 31, 2028 },
                    { 64L, null, true, null, 4, 30, 2028 },
                    { 65L, null, true, null, 5, 31, 2028 },
                    { 66L, null, true, null, 6, 30, 2028 },
                    { 67L, null, true, null, 7, 31, 2028 },
                    { 68L, null, true, null, 8, 31, 2028 },
                    { 69L, null, true, null, 9, 30, 2028 },
                    { 70L, null, true, null, 10, 31, 2028 },
                    { 71L, null, true, null, 11, 30, 2028 },
                    { 72L, null, true, null, 12, 31, 2028 },
                    { 73L, null, true, null, 1, 31, 2029 },
                    { 74L, null, true, null, 2, 28, 2029 },
                    { 75L, null, true, null, 3, 31, 2029 },
                    { 76L, null, true, null, 4, 30, 2029 },
                    { 77L, null, true, null, 5, 31, 2029 },
                    { 78L, null, true, null, 6, 30, 2029 },
                    { 79L, null, true, null, 7, 31, 2029 },
                    { 80L, null, true, null, 8, 31, 2029 },
                    { 81L, null, true, null, 9, 30, 2029 },
                    { 82L, null, true, null, 10, 31, 2029 },
                    { 83L, null, true, null, 11, 30, 2029 },
                    { 84L, null, true, null, 12, 31, 2029 },
                    { 85L, null, true, null, 1, 31, 2030 },
                    { 86L, null, true, null, 2, 28, 2030 },
                    { 87L, null, true, null, 3, 31, 2030 },
                    { 88L, null, true, null, 4, 30, 2030 },
                    { 89L, null, true, null, 5, 31, 2030 },
                    { 90L, null, true, null, 6, 30, 2030 },
                    { 91L, null, true, null, 7, 31, 2030 },
                    { 92L, null, true, null, 8, 31, 2030 },
                    { 93L, null, true, null, 9, 30, 2030 },
                    { 94L, null, true, null, 10, 31, 2030 },
                    { 95L, null, true, null, 11, 30, 2030 },
                    { 96L, null, true, null, 12, 31, 2030 },
                    { 97L, null, true, null, 1, 31, 2031 },
                    { 98L, null, true, null, 2, 28, 2031 },
                    { 99L, null, true, null, 3, 31, 2031 },
                    { 100L, null, true, null, 4, 30, 2031 },
                    { 101L, null, true, null, 5, 31, 2031 },
                    { 102L, null, true, null, 6, 30, 2031 },
                    { 103L, null, true, null, 7, 31, 2031 },
                    { 104L, null, true, null, 8, 31, 2031 },
                    { 105L, null, true, null, 9, 30, 2031 },
                    { 106L, null, true, null, 10, 31, 2031 },
                    { 107L, null, true, null, 11, 30, 2031 },
                    { 108L, null, true, null, 12, 31, 2031 },
                    { 109L, null, true, null, 1, 31, 2032 },
                    { 110L, null, true, null, 2, 29, 2032 },
                    { 111L, null, true, null, 3, 31, 2032 },
                    { 112L, null, true, null, 4, 30, 2032 },
                    { 113L, null, true, null, 5, 31, 2032 },
                    { 114L, null, true, null, 6, 30, 2032 },
                    { 115L, null, true, null, 7, 31, 2032 },
                    { 116L, null, true, null, 8, 31, 2032 },
                    { 117L, null, true, null, 9, 30, 2032 },
                    { 118L, null, true, null, 10, 31, 2032 },
                    { 119L, null, true, null, 11, 30, 2032 },
                    { 120L, null, true, null, 12, 31, 2032 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "AuthenticationId", "BusinessTransactionActivityId", "FirstName", "HouseholdId", "IsActive", "LastName", "ModifiedBy", "UserType" },
                values: new object[] { 1L, "", 1L, "System", null, false, null, null, 0 });

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
                name: "IX_BusinessTransactionActivities_UserId1",
                table: "BusinessTransactionActivities",
                column: "UserId1");

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
                name: "FK_BusinessTransactionActivities_Users_UserId1",
                table: "BusinessTransactionActivities",
                column: "UserId1",
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
                name: "IncomeDetails")
                .Annotation("SqlServer:IsTemporal", true)
                .Annotation("SqlServer:TemporalHistoryTableName", "IncomeDetailHistory")
                .Annotation("SqlServer:TemporalHistoryTableSchema", null)
                .Annotation("SqlServer:TemporalPeriodEndColumnName", "ModifiedAt")
                .Annotation("SqlServer:TemporalPeriodStartColumnName", "CreatedAt");

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
}
