--Date: 2024-05-30
--Always check that this script is up to date with the latest migration script
--When you update, be sure to change the date above
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [AccountTypes] (
    [AccountTypeId] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [BalanceType] int NOT NULL,
    CONSTRAINT [PK_AccountTypes] PRIMARY KEY ([AccountTypeId])
);
GO

CREATE TABLE [BusinessTransactionActivities] (
    [BusinessTransactionActivityId] bigint NOT NULL IDENTITY,
    [ProcessName] nvarchar(max) NOT NULL,
    [CreatedAt] datetime2 NOT NULL,
    [UserId] bigint NULL,
    CONSTRAINT [PK_BusinessTransactionActivities] PRIMARY KEY ([BusinessTransactionActivityId])
);
GO

DECLARE @historyTableSchema sysname = SCHEMA_NAME()
EXEC(N'CREATE TABLE [Expenses] (
    [ExpenseId] bigint NOT NULL IDENTITY,
    [Name] nvarchar(125) NULL,
    [Amount] decimal(19,2) NOT NULL,
    [IsActive] bit NOT NULL,
    [HouseholdId] bigint NOT NULL,
    [CreatedAt] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [ModifiedAt] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    [BusinessTransactionActivityId] bigint NULL,
    [ModifiedBy] bigint NULL,
    CONSTRAINT [PK_Expenses] PRIMARY KEY ([ExpenseId]),
    CONSTRAINT [FK_Expenses_BusinessTransactionActivities_BusinessTransactionActivityId] FOREIGN KEY ([BusinessTransactionActivityId]) REFERENCES [BusinessTransactionActivities] ([BusinessTransactionActivityId]),
    PERIOD FOR SYSTEM_TIME([CreatedAt], [ModifiedAt])
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [' + @historyTableSchema + N'].[ExpenseHistory]))');
GO

DECLARE @historyTableSchema sysname = SCHEMA_NAME()
EXEC(N'CREATE TABLE [Households] (
    [HouseholdId] bigint NOT NULL IDENTITY,
    [Name] nvarchar(max) NULL,
    [CreatedAt] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [ModifiedAt] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    [BusinessTransactionActivityId] bigint NULL,
    [ModifiedBy] bigint NULL,
    CONSTRAINT [PK_Households] PRIMARY KEY ([HouseholdId]),
    CONSTRAINT [FK_Households_BusinessTransactionActivities_BusinessTransactionActivityId] FOREIGN KEY ([BusinessTransactionActivityId]) REFERENCES [BusinessTransactionActivities] ([BusinessTransactionActivityId]),
    PERIOD FOR SYSTEM_TIME([CreatedAt], [ModifiedAt])
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [' + @historyTableSchema + N'].[HouseholdHistory]))');
GO

DECLARE @historyTableSchema sysname = SCHEMA_NAME()
EXEC(N'CREATE TABLE [Incomes] (
    [IncomeId] bigint NOT NULL IDENTITY,
    [HouseholdId] bigint NOT NULL,
    [Name] nvarchar(max) NULL,
    [Amount] decimal(19,2) NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [ModifiedAt] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    [BusinessTransactionActivityId] bigint NULL,
    [ModifiedBy] bigint NULL,
    CONSTRAINT [PK_Incomes] PRIMARY KEY ([IncomeId]),
    CONSTRAINT [FK_Incomes_BusinessTransactionActivities_BusinessTransactionActivityId] FOREIGN KEY ([BusinessTransactionActivityId]) REFERENCES [BusinessTransactionActivities] ([BusinessTransactionActivityId]),
    PERIOD FOR SYSTEM_TIME([CreatedAt], [ModifiedAt])
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [' + @historyTableSchema + N'].[IncomeHistory]))');
GO

CREATE TABLE [MonthYears] (
    [MonthYearId] bigint NOT NULL IDENTITY,
    [Month] int NOT NULL,
    [Year] int NOT NULL,
    [IsActive] bit NOT NULL,
    [NumberOfDays] int NOT NULL,
    [BusinessTransactionActivityId] bigint NULL,
    [ModifiedBy] bigint NULL,
    CONSTRAINT [PK_MonthYears] PRIMARY KEY ([MonthYearId]),
    CONSTRAINT [FK_MonthYears_BusinessTransactionActivities_BusinessTransactionActivityId] FOREIGN KEY ([BusinessTransactionActivityId]) REFERENCES [BusinessTransactionActivities] ([BusinessTransactionActivityId])
);
GO

DECLARE @historyTableSchema sysname = SCHEMA_NAME()
EXEC(N'CREATE TABLE [Accounts] (
    [AccountId] bigint NOT NULL IDENTITY,
    [Name] nvarchar(125) NULL,
    [LongName] nvarchar(max) NULL,
    [Balance] decimal(19,2) NOT NULL,
    [AccountTypeId] bigint NOT NULL,
    [HouseholdId] bigint NOT NULL,
    [CreatedAt] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [ModifiedAt] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    [BusinessTransactionActivityId] bigint NULL,
    [ModifiedBy] bigint NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([AccountId]),
    CONSTRAINT [CK_Account_Balance] CHECK ([Balance] >= 0),
    CONSTRAINT [FK_Accounts_AccountTypes_AccountTypeId] FOREIGN KEY ([AccountTypeId]) REFERENCES [AccountTypes] ([AccountTypeId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Accounts_BusinessTransactionActivities_BusinessTransactionActivityId] FOREIGN KEY ([BusinessTransactionActivityId]) REFERENCES [BusinessTransactionActivities] ([BusinessTransactionActivityId]),
    CONSTRAINT [FK_Accounts_Households_HouseholdId] FOREIGN KEY ([HouseholdId]) REFERENCES [Households] ([HouseholdId]) ON DELETE NO ACTION,
    PERIOD FOR SYSTEM_TIME([CreatedAt], [ModifiedAt])
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [' + @historyTableSchema + N'].[AccountHistory]))');
GO

DECLARE @historyTableSchema sysname = SCHEMA_NAME()
EXEC(N'CREATE TABLE [Users] (
    [UserId] bigint NOT NULL IDENTITY,
    [AuthenticationId] nvarchar(max) NULL,
    [FirstName] nvarchar(max) NULL,
    [LastName] nvarchar(max) NULL,
    [HouseholdId] bigint NULL,
    [UserType] int NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [ModifiedAt] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    [BusinessTransactionActivityId] bigint NULL,
    [ModifiedBy] bigint NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_BusinessTransactionActivities_BusinessTransactionActivityId] FOREIGN KEY ([BusinessTransactionActivityId]) REFERENCES [BusinessTransactionActivities] ([BusinessTransactionActivityId]),
    CONSTRAINT [FK_Users_Households_HouseholdId] FOREIGN KEY ([HouseholdId]) REFERENCES [Households] ([HouseholdId]),
    PERIOD FOR SYSTEM_TIME([CreatedAt], [ModifiedAt])
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [' + @historyTableSchema + N'].[UserHistory]))');
GO

DECLARE @historyTableSchema sysname = SCHEMA_NAME()
EXEC(N'CREATE TABLE [BudgetMonths] (
    [BudgetMonthId] bigint NOT NULL IDENTITY,
    [MonthYearId] bigint NOT NULL,
    [IncomeTotal] decimal(19,2) NOT NULL,
    [ExpenseTotal] decimal(19,2) NOT NULL,
    [HouseholdId] bigint NOT NULL,
    [CreatedAt] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [ModifiedAt] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    [BusinessTransactionActivityId] bigint NULL,
    [ModifiedBy] bigint NULL,
    CONSTRAINT [PK_BudgetMonths] PRIMARY KEY ([BudgetMonthId]),
    CONSTRAINT [FK_BudgetMonths_BusinessTransactionActivities_BusinessTransactionActivityId] FOREIGN KEY ([BusinessTransactionActivityId]) REFERENCES [BusinessTransactionActivities] ([BusinessTransactionActivityId]),
    CONSTRAINT [FK_BudgetMonths_Households_HouseholdId] FOREIGN KEY ([HouseholdId]) REFERENCES [Households] ([HouseholdId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_BudgetMonths_MonthYears_MonthYearId] FOREIGN KEY ([MonthYearId]) REFERENCES [MonthYears] ([MonthYearId]) ON DELETE NO ACTION,
    PERIOD FOR SYSTEM_TIME([CreatedAt], [ModifiedAt])
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [' + @historyTableSchema + N'].[BudgetMonthHistory]))');
GO

DECLARE @historyTableSchema sysname = SCHEMA_NAME()
EXEC(N'CREATE TABLE [ExpenseDetails] (
    [ExpenseDetailId] bigint NOT NULL IDENTITY,
    [ExpenseId] bigint NOT NULL,
    [BudgetMonthId] bigint NOT NULL,
    [IsActive] bit NOT NULL,
    [AmountAllocated] decimal(19,2) NOT NULL,
    [CreatedAt] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [ModifiedAt] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    [BusinessTransactionActivityId] bigint NULL,
    [ModifiedBy] bigint NULL,
    CONSTRAINT [PK_ExpenseDetails] PRIMARY KEY ([ExpenseDetailId]),
    CONSTRAINT [FK_ExpenseDetails_BudgetMonths_BudgetMonthId] FOREIGN KEY ([BudgetMonthId]) REFERENCES [BudgetMonths] ([BudgetMonthId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_ExpenseDetails_BusinessTransactionActivities_BusinessTransactionActivityId] FOREIGN KEY ([BusinessTransactionActivityId]) REFERENCES [BusinessTransactionActivities] ([BusinessTransactionActivityId]),
    CONSTRAINT [FK_ExpenseDetails_Expenses_ExpenseId] FOREIGN KEY ([ExpenseId]) REFERENCES [Expenses] ([ExpenseId]) ON DELETE NO ACTION,
    PERIOD FOR SYSTEM_TIME([CreatedAt], [ModifiedAt])
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [' + @historyTableSchema + N'].[ExpenseDetailHistory]))');
GO

DECLARE @historyTableSchema sysname = SCHEMA_NAME()
EXEC(N'CREATE TABLE [IncomeDetails] (
    [IncomeDetailId] bigint NOT NULL IDENTITY,
    [IncomeId] bigint NOT NULL,
    [BudgetMonthId] bigint NOT NULL,
    [IsActive] bit NOT NULL,
    [CreatedAt] datetime2 GENERATED ALWAYS AS ROW START HIDDEN NOT NULL,
    [ModifiedAt] datetime2 GENERATED ALWAYS AS ROW END HIDDEN NOT NULL,
    [BusinessTransactionActivityId] bigint NULL,
    [ModifiedBy] bigint NULL,
    CONSTRAINT [PK_IncomeDetails] PRIMARY KEY ([IncomeDetailId]),
    CONSTRAINT [FK_IncomeDetails_BudgetMonths_BudgetMonthId] FOREIGN KEY ([BudgetMonthId]) REFERENCES [BudgetMonths] ([BudgetMonthId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_IncomeDetails_BusinessTransactionActivities_BusinessTransactionActivityId] FOREIGN KEY ([BusinessTransactionActivityId]) REFERENCES [BusinessTransactionActivities] ([BusinessTransactionActivityId]),
    CONSTRAINT [FK_IncomeDetails_Incomes_IncomeId] FOREIGN KEY ([IncomeId]) REFERENCES [Incomes] ([IncomeId]) ON DELETE NO ACTION,
    PERIOD FOR SYSTEM_TIME([CreatedAt], [ModifiedAt])
) WITH (SYSTEM_VERSIONING = ON (HISTORY_TABLE = [' + @historyTableSchema + N'].[IncomeDetailHistory]))');
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AccountTypeId', N'BalanceType', N'Name') AND [object_id] = OBJECT_ID(N'[AccountTypes]'))
    SET IDENTITY_INSERT [AccountTypes] ON;
INSERT INTO [AccountTypes] ([AccountTypeId], [BalanceType], [Name])
VALUES (CAST(1 AS bigint), 0, N'Checking'),
(CAST(2 AS bigint), 0, N'Savings'),
(CAST(3 AS bigint), 1, N'Credit Card'),
(CAST(4 AS bigint), 0, N'Cash'),
(CAST(5 AS bigint), 0, N'Investment'),
(CAST(6 AS bigint), 1, N'Loan'),
(CAST(7 AS bigint), 0, N'Other'),
(CAST(8 AS bigint), 0, N'Retirement');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'AccountTypeId', N'BalanceType', N'Name') AND [object_id] = OBJECT_ID(N'[AccountTypes]'))
    SET IDENTITY_INSERT [AccountTypes] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BusinessTransactionActivityId', N'CreatedAt', N'ProcessName', N'UserId') AND [object_id] = OBJECT_ID(N'[BusinessTransactionActivities]'))
    SET IDENTITY_INSERT [BusinessTransactionActivities] ON;
INSERT INTO [BusinessTransactionActivities] ([BusinessTransactionActivityId], [CreatedAt], [ProcessName], [UserId])
VALUES (CAST(1 AS bigint), '2024-05-30T06:36:14.6765261-06:00', N'Initial Seeding', CAST(1 AS bigint));
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'BusinessTransactionActivityId', N'CreatedAt', N'ProcessName', N'UserId') AND [object_id] = OBJECT_ID(N'[BusinessTransactionActivities]'))
    SET IDENTITY_INSERT [BusinessTransactionActivities] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MonthYearId', N'BusinessTransactionActivityId', N'IsActive', N'ModifiedBy', N'Month', N'NumberOfDays', N'Year') AND [object_id] = OBJECT_ID(N'[MonthYears]'))
    SET IDENTITY_INSERT [MonthYears] ON;
INSERT INTO [MonthYears] ([MonthYearId], [BusinessTransactionActivityId], [IsActive], [ModifiedBy], [Month], [NumberOfDays], [Year])
VALUES (CAST(1 AS bigint), NULL, CAST(1 AS bit), NULL, 1, 31, 2023),
(CAST(2 AS bigint), NULL, CAST(1 AS bit), NULL, 2, 28, 2023),
(CAST(3 AS bigint), NULL, CAST(1 AS bit), NULL, 3, 31, 2023),
(CAST(4 AS bigint), NULL, CAST(1 AS bit), NULL, 4, 30, 2023),
(CAST(5 AS bigint), NULL, CAST(1 AS bit), NULL, 5, 31, 2023),
(CAST(6 AS bigint), NULL, CAST(1 AS bit), NULL, 6, 30, 2023),
(CAST(7 AS bigint), NULL, CAST(1 AS bit), NULL, 7, 31, 2023),
(CAST(8 AS bigint), NULL, CAST(1 AS bit), NULL, 8, 31, 2023),
(CAST(9 AS bigint), NULL, CAST(1 AS bit), NULL, 9, 30, 2023),
(CAST(10 AS bigint), NULL, CAST(1 AS bit), NULL, 10, 31, 2023),
(CAST(11 AS bigint), NULL, CAST(1 AS bit), NULL, 11, 30, 2023),
(CAST(12 AS bigint), NULL, CAST(1 AS bit), NULL, 12, 31, 2023),
(CAST(13 AS bigint), NULL, CAST(1 AS bit), NULL, 1, 31, 2024),
(CAST(14 AS bigint), NULL, CAST(1 AS bit), NULL, 2, 29, 2024),
(CAST(15 AS bigint), NULL, CAST(1 AS bit), NULL, 3, 31, 2024),
(CAST(16 AS bigint), NULL, CAST(1 AS bit), NULL, 4, 30, 2024),
(CAST(17 AS bigint), NULL, CAST(1 AS bit), NULL, 5, 31, 2024),
(CAST(18 AS bigint), NULL, CAST(1 AS bit), NULL, 6, 30, 2024),
(CAST(19 AS bigint), NULL, CAST(1 AS bit), NULL, 7, 31, 2024),
(CAST(20 AS bigint), NULL, CAST(1 AS bit), NULL, 8, 31, 2024),
(CAST(21 AS bigint), NULL, CAST(1 AS bit), NULL, 9, 30, 2024),
(CAST(22 AS bigint), NULL, CAST(1 AS bit), NULL, 10, 31, 2024),
(CAST(23 AS bigint), NULL, CAST(1 AS bit), NULL, 11, 30, 2024),
(CAST(24 AS bigint), NULL, CAST(1 AS bit), NULL, 12, 31, 2024),
(CAST(25 AS bigint), NULL, CAST(1 AS bit), NULL, 1, 31, 2025),
(CAST(26 AS bigint), NULL, CAST(1 AS bit), NULL, 2, 28, 2025),
(CAST(27 AS bigint), NULL, CAST(1 AS bit), NULL, 3, 31, 2025),
(CAST(28 AS bigint), NULL, CAST(1 AS bit), NULL, 4, 30, 2025),
(CAST(29 AS bigint), NULL, CAST(1 AS bit), NULL, 5, 31, 2025),
(CAST(30 AS bigint), NULL, CAST(1 AS bit), NULL, 6, 30, 2025),
(CAST(31 AS bigint), NULL, CAST(1 AS bit), NULL, 7, 31, 2025),
(CAST(32 AS bigint), NULL, CAST(1 AS bit), NULL, 8, 31, 2025),
(CAST(33 AS bigint), NULL, CAST(1 AS bit), NULL, 9, 30, 2025),
(CAST(34 AS bigint), NULL, CAST(1 AS bit), NULL, 10, 31, 2025),
(CAST(35 AS bigint), NULL, CAST(1 AS bit), NULL, 11, 30, 2025),
(CAST(36 AS bigint), NULL, CAST(1 AS bit), NULL, 12, 31, 2025),
(CAST(37 AS bigint), NULL, CAST(1 AS bit), NULL, 1, 31, 2026),
(CAST(38 AS bigint), NULL, CAST(1 AS bit), NULL, 2, 28, 2026),
(CAST(39 AS bigint), NULL, CAST(1 AS bit), NULL, 3, 31, 2026),
(CAST(40 AS bigint), NULL, CAST(1 AS bit), NULL, 4, 30, 2026),
(CAST(41 AS bigint), NULL, CAST(1 AS bit), NULL, 5, 31, 2026),
(CAST(42 AS bigint), NULL, CAST(1 AS bit), NULL, 6, 30, 2026);
INSERT INTO [MonthYears] ([MonthYearId], [BusinessTransactionActivityId], [IsActive], [ModifiedBy], [Month], [NumberOfDays], [Year])
VALUES (CAST(43 AS bigint), NULL, CAST(1 AS bit), NULL, 7, 31, 2026),
(CAST(44 AS bigint), NULL, CAST(1 AS bit), NULL, 8, 31, 2026),
(CAST(45 AS bigint), NULL, CAST(1 AS bit), NULL, 9, 30, 2026),
(CAST(46 AS bigint), NULL, CAST(1 AS bit), NULL, 10, 31, 2026),
(CAST(47 AS bigint), NULL, CAST(1 AS bit), NULL, 11, 30, 2026),
(CAST(48 AS bigint), NULL, CAST(1 AS bit), NULL, 12, 31, 2026),
(CAST(49 AS bigint), NULL, CAST(1 AS bit), NULL, 1, 31, 2027),
(CAST(50 AS bigint), NULL, CAST(1 AS bit), NULL, 2, 28, 2027),
(CAST(51 AS bigint), NULL, CAST(1 AS bit), NULL, 3, 31, 2027),
(CAST(52 AS bigint), NULL, CAST(1 AS bit), NULL, 4, 30, 2027),
(CAST(53 AS bigint), NULL, CAST(1 AS bit), NULL, 5, 31, 2027),
(CAST(54 AS bigint), NULL, CAST(1 AS bit), NULL, 6, 30, 2027),
(CAST(55 AS bigint), NULL, CAST(1 AS bit), NULL, 7, 31, 2027),
(CAST(56 AS bigint), NULL, CAST(1 AS bit), NULL, 8, 31, 2027),
(CAST(57 AS bigint), NULL, CAST(1 AS bit), NULL, 9, 30, 2027),
(CAST(58 AS bigint), NULL, CAST(1 AS bit), NULL, 10, 31, 2027),
(CAST(59 AS bigint), NULL, CAST(1 AS bit), NULL, 11, 30, 2027),
(CAST(60 AS bigint), NULL, CAST(1 AS bit), NULL, 12, 31, 2027),
(CAST(61 AS bigint), NULL, CAST(1 AS bit), NULL, 1, 31, 2028),
(CAST(62 AS bigint), NULL, CAST(1 AS bit), NULL, 2, 29, 2028),
(CAST(63 AS bigint), NULL, CAST(1 AS bit), NULL, 3, 31, 2028),
(CAST(64 AS bigint), NULL, CAST(1 AS bit), NULL, 4, 30, 2028),
(CAST(65 AS bigint), NULL, CAST(1 AS bit), NULL, 5, 31, 2028),
(CAST(66 AS bigint), NULL, CAST(1 AS bit), NULL, 6, 30, 2028),
(CAST(67 AS bigint), NULL, CAST(1 AS bit), NULL, 7, 31, 2028),
(CAST(68 AS bigint), NULL, CAST(1 AS bit), NULL, 8, 31, 2028),
(CAST(69 AS bigint), NULL, CAST(1 AS bit), NULL, 9, 30, 2028),
(CAST(70 AS bigint), NULL, CAST(1 AS bit), NULL, 10, 31, 2028),
(CAST(71 AS bigint), NULL, CAST(1 AS bit), NULL, 11, 30, 2028),
(CAST(72 AS bigint), NULL, CAST(1 AS bit), NULL, 12, 31, 2028),
(CAST(73 AS bigint), NULL, CAST(1 AS bit), NULL, 1, 31, 2029),
(CAST(74 AS bigint), NULL, CAST(1 AS bit), NULL, 2, 28, 2029),
(CAST(75 AS bigint), NULL, CAST(1 AS bit), NULL, 3, 31, 2029),
(CAST(76 AS bigint), NULL, CAST(1 AS bit), NULL, 4, 30, 2029),
(CAST(77 AS bigint), NULL, CAST(1 AS bit), NULL, 5, 31, 2029),
(CAST(78 AS bigint), NULL, CAST(1 AS bit), NULL, 6, 30, 2029),
(CAST(79 AS bigint), NULL, CAST(1 AS bit), NULL, 7, 31, 2029),
(CAST(80 AS bigint), NULL, CAST(1 AS bit), NULL, 8, 31, 2029),
(CAST(81 AS bigint), NULL, CAST(1 AS bit), NULL, 9, 30, 2029),
(CAST(82 AS bigint), NULL, CAST(1 AS bit), NULL, 10, 31, 2029),
(CAST(83 AS bigint), NULL, CAST(1 AS bit), NULL, 11, 30, 2029),
(CAST(84 AS bigint), NULL, CAST(1 AS bit), NULL, 12, 31, 2029);
INSERT INTO [MonthYears] ([MonthYearId], [BusinessTransactionActivityId], [IsActive], [ModifiedBy], [Month], [NumberOfDays], [Year])
VALUES (CAST(85 AS bigint), NULL, CAST(1 AS bit), NULL, 1, 31, 2030),
(CAST(86 AS bigint), NULL, CAST(1 AS bit), NULL, 2, 28, 2030),
(CAST(87 AS bigint), NULL, CAST(1 AS bit), NULL, 3, 31, 2030),
(CAST(88 AS bigint), NULL, CAST(1 AS bit), NULL, 4, 30, 2030),
(CAST(89 AS bigint), NULL, CAST(1 AS bit), NULL, 5, 31, 2030),
(CAST(90 AS bigint), NULL, CAST(1 AS bit), NULL, 6, 30, 2030),
(CAST(91 AS bigint), NULL, CAST(1 AS bit), NULL, 7, 31, 2030),
(CAST(92 AS bigint), NULL, CAST(1 AS bit), NULL, 8, 31, 2030),
(CAST(93 AS bigint), NULL, CAST(1 AS bit), NULL, 9, 30, 2030),
(CAST(94 AS bigint), NULL, CAST(1 AS bit), NULL, 10, 31, 2030),
(CAST(95 AS bigint), NULL, CAST(1 AS bit), NULL, 11, 30, 2030),
(CAST(96 AS bigint), NULL, CAST(1 AS bit), NULL, 12, 31, 2030),
(CAST(97 AS bigint), NULL, CAST(1 AS bit), NULL, 1, 31, 2031),
(CAST(98 AS bigint), NULL, CAST(1 AS bit), NULL, 2, 28, 2031),
(CAST(99 AS bigint), NULL, CAST(1 AS bit), NULL, 3, 31, 2031),
(CAST(100 AS bigint), NULL, CAST(1 AS bit), NULL, 4, 30, 2031),
(CAST(101 AS bigint), NULL, CAST(1 AS bit), NULL, 5, 31, 2031),
(CAST(102 AS bigint), NULL, CAST(1 AS bit), NULL, 6, 30, 2031),
(CAST(103 AS bigint), NULL, CAST(1 AS bit), NULL, 7, 31, 2031),
(CAST(104 AS bigint), NULL, CAST(1 AS bit), NULL, 8, 31, 2031),
(CAST(105 AS bigint), NULL, CAST(1 AS bit), NULL, 9, 30, 2031),
(CAST(106 AS bigint), NULL, CAST(1 AS bit), NULL, 10, 31, 2031),
(CAST(107 AS bigint), NULL, CAST(1 AS bit), NULL, 11, 30, 2031),
(CAST(108 AS bigint), NULL, CAST(1 AS bit), NULL, 12, 31, 2031),
(CAST(109 AS bigint), NULL, CAST(1 AS bit), NULL, 1, 31, 2032),
(CAST(110 AS bigint), NULL, CAST(1 AS bit), NULL, 2, 29, 2032),
(CAST(111 AS bigint), NULL, CAST(1 AS bit), NULL, 3, 31, 2032),
(CAST(112 AS bigint), NULL, CAST(1 AS bit), NULL, 4, 30, 2032),
(CAST(113 AS bigint), NULL, CAST(1 AS bit), NULL, 5, 31, 2032),
(CAST(114 AS bigint), NULL, CAST(1 AS bit), NULL, 6, 30, 2032),
(CAST(115 AS bigint), NULL, CAST(1 AS bit), NULL, 7, 31, 2032),
(CAST(116 AS bigint), NULL, CAST(1 AS bit), NULL, 8, 31, 2032),
(CAST(117 AS bigint), NULL, CAST(1 AS bit), NULL, 9, 30, 2032),
(CAST(118 AS bigint), NULL, CAST(1 AS bit), NULL, 10, 31, 2032),
(CAST(119 AS bigint), NULL, CAST(1 AS bit), NULL, 11, 30, 2032),
(CAST(120 AS bigint), NULL, CAST(1 AS bit), NULL, 12, 31, 2032);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'MonthYearId', N'BusinessTransactionActivityId', N'IsActive', N'ModifiedBy', N'Month', N'NumberOfDays', N'Year') AND [object_id] = OBJECT_ID(N'[MonthYears]'))
    SET IDENTITY_INSERT [MonthYears] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'AuthenticationId', N'BusinessTransactionActivityId', N'FirstName', N'HouseholdId', N'IsActive', N'LastName', N'ModifiedBy', N'UserType') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] ON;
INSERT INTO [Users] ([UserId], [AuthenticationId], [BusinessTransactionActivityId], [FirstName], [HouseholdId], [IsActive], [LastName], [ModifiedBy], [UserType])
VALUES (CAST(1 AS bigint), N'', CAST(1 AS bigint), N'System', NULL, CAST(0 AS bit), NULL, NULL, 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'UserId', N'AuthenticationId', N'BusinessTransactionActivityId', N'FirstName', N'HouseholdId', N'IsActive', N'LastName', N'ModifiedBy', N'UserType') AND [object_id] = OBJECT_ID(N'[Users]'))
    SET IDENTITY_INSERT [Users] OFF;
GO

CREATE INDEX [IX_Accounts_AccountTypeId] ON [Accounts] ([AccountTypeId]);
GO

CREATE INDEX [IX_Accounts_BusinessTransactionActivityId] ON [Accounts] ([BusinessTransactionActivityId]);
GO

CREATE INDEX [IX_Accounts_HouseholdId] ON [Accounts] ([HouseholdId]);
GO

CREATE INDEX [IX_BudgetMonths_BusinessTransactionActivityId] ON [BudgetMonths] ([BusinessTransactionActivityId]);
GO

CREATE INDEX [IX_BudgetMonths_HouseholdId] ON [BudgetMonths] ([HouseholdId]);
GO

CREATE INDEX [IX_BudgetMonths_MonthYearId] ON [BudgetMonths] ([MonthYearId]);
GO

CREATE INDEX [IX_ExpenseDetails_BudgetMonthId] ON [ExpenseDetails] ([BudgetMonthId]);
GO

CREATE INDEX [IX_ExpenseDetails_BusinessTransactionActivityId] ON [ExpenseDetails] ([BusinessTransactionActivityId]);
GO

CREATE INDEX [IX_ExpenseDetails_ExpenseId] ON [ExpenseDetails] ([ExpenseId]);
GO

CREATE INDEX [IX_Expenses_BusinessTransactionActivityId] ON [Expenses] ([BusinessTransactionActivityId]);
GO

CREATE INDEX [IX_Households_BusinessTransactionActivityId] ON [Households] ([BusinessTransactionActivityId]);
GO

CREATE INDEX [IX_IncomeDetails_BudgetMonthId] ON [IncomeDetails] ([BudgetMonthId]);
GO

CREATE INDEX [IX_IncomeDetails_BusinessTransactionActivityId] ON [IncomeDetails] ([BusinessTransactionActivityId]);
GO

CREATE INDEX [IX_IncomeDetails_IncomeId] ON [IncomeDetails] ([IncomeId]);
GO

CREATE INDEX [IX_Incomes_BusinessTransactionActivityId] ON [Incomes] ([BusinessTransactionActivityId]);
GO

CREATE INDEX [IX_MonthYears_BusinessTransactionActivityId] ON [MonthYears] ([BusinessTransactionActivityId]);
GO

CREATE INDEX [IX_Users_BusinessTransactionActivityId] ON [Users] ([BusinessTransactionActivityId]);
GO

CREATE INDEX [IX_Users_HouseholdId] ON [Users] ([HouseholdId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240530123615_Initial', N'8.0.6');
GO

COMMIT;
GO