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
CREATE TABLE [AspNetRoles] (
    [Id] nvarchar(450) NOT NULL,
    [Name] nvarchar(256) NULL,
    [NormalizedName] nvarchar(256) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
);

CREATE TABLE [AspNetUsers] (
    [Id] nvarchar(450) NOT NULL,
    [FullName] nvarchar(max) NOT NULL,
    [ImageUrl] nvarchar(max) NULL,
    [UserName] nvarchar(256) NULL,
    [NormalizedUserName] nvarchar(256) NULL,
    [Email] nvarchar(256) NULL,
    [NormalizedEmail] nvarchar(256) NULL,
    [EmailConfirmed] bit NOT NULL,
    [PasswordHash] nvarchar(max) NULL,
    [SecurityStamp] nvarchar(max) NULL,
    [ConcurrencyStamp] nvarchar(max) NULL,
    [PhoneNumber] nvarchar(max) NULL,
    [PhoneNumberConfirmed] bit NOT NULL,
    [TwoFactorEnabled] bit NOT NULL,
    [LockoutEnd] datetimeoffset NULL,
    [LockoutEnabled] bit NOT NULL,
    [AccessFailedCount] int NOT NULL,
    CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
);

CREATE TABLE [FishTypes] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [UnitOfMeasure] nvarchar(max) NULL,
    CONSTRAINT [PK_FishTypes] PRIMARY KEY ([Id])
);

CREATE TABLE [ProcessingUnits] (
    [UnitId] int NOT NULL IDENTITY,
    [UnitName] nvarchar(max) NOT NULL,
    [Contact] nvarchar(max) NULL,
    CONSTRAINT [PK_ProcessingUnits] PRIMARY KEY ([UnitId])
);

CREATE TABLE [Receipts] (
    [ReceiptId] int NOT NULL IDENTITY,
    [PaymentId] int NOT NULL,
    [FishTypeId] int NOT NULL,
    [UnitId] int NOT NULL,
    [Notes] nvarchar(max) NULL,
    CONSTRAINT [PK_Receipts] PRIMARY KEY ([ReceiptId])
);

CREATE TABLE [AspNetRoleClaims] (
    [Id] int NOT NULL IDENTITY,
    [RoleId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserClaims] (
    [Id] int NOT NULL IDENTITY,
    [UserId] nvarchar(450) NOT NULL,
    [ClaimType] nvarchar(max) NULL,
    [ClaimValue] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserLogins] (
    [LoginProvider] nvarchar(450) NOT NULL,
    [ProviderKey] nvarchar(450) NOT NULL,
    [ProviderDisplayName] nvarchar(max) NULL,
    [UserId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
    CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserRoles] (
    [UserId] nvarchar(450) NOT NULL,
    [RoleId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [AspNetUserTokens] (
    [UserId] nvarchar(450) NOT NULL,
    [LoginProvider] nvarchar(450) NOT NULL,
    [Name] nvarchar(450) NOT NULL,
    [Value] nvarchar(max) NULL,
    CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
    CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [FishPrices] (
    [PriceId] int NOT NULL IDENTITY,
    [PricePerUnitOfMeasure] decimal(18,2) NOT NULL,
    [FishTypeId] int NOT NULL,
    [EffectiveDate] datetime2 NOT NULL,
    CONSTRAINT [PK_FishPrices] PRIMARY KEY ([PriceId]),
    CONSTRAINT [FK_FishPrices_FishTypes_FishTypeId] FOREIGN KEY ([FishTypeId]) REFERENCES [FishTypes] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [SalaryPayments] (
    [PaymentId] int NOT NULL IDENTITY,
    [PaymentDate] datetime2 NOT NULL,
    [FishId] int NOT NULL,
    [UnitId] int NOT NULL,
    [TotalQuantityProcessed] decimal(18,2) NOT NULL,
    [Notes] nvarchar(max) NULL,
    [FishTypeId] int NOT NULL,
    [ProcessingUnitUnitId] int NOT NULL,
    CONSTRAINT [PK_SalaryPayments] PRIMARY KEY ([PaymentId]),
    CONSTRAINT [FK_SalaryPayments_FishTypes_FishTypeId] FOREIGN KEY ([FishTypeId]) REFERENCES [FishTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SalaryPayments_ProcessingUnits_ProcessingUnitUnitId] FOREIGN KEY ([ProcessingUnitUnitId]) REFERENCES [ProcessingUnits] ([UnitId]) ON DELETE CASCADE
);

CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);

CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;

CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);

CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);

CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);

CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);

CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;

CREATE UNIQUE INDEX [IX_FishPrices_FishTypeId] ON [FishPrices] ([FishTypeId]);

CREATE INDEX [IX_SalaryPayments_FishTypeId] ON [SalaryPayments] ([FishTypeId]);

CREATE INDEX [IX_SalaryPayments_ProcessingUnitUnitId] ON [SalaryPayments] ([ProcessingUnitUnitId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251128102524_InitialCreate', N'9.0.0');

DROP TABLE [SalaryPayments];

EXEC sp_rename N'[Receipts].[PaymentId]', N'SalaryProcesseId', 'COLUMN';

ALTER TABLE [FishPrices] ADD [ImgFishUrl] nvarchar(max) NULL;

CREATE TABLE [SalaryProcesses] (
    [SalaryProcesseId] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [FishId] int NOT NULL,
    [UnitId] int NOT NULL,
    [TotalQuantityProcessed] decimal(18,2) NOT NULL,
    [Notes] nvarchar(max) NULL,
    [FishTypeId] int NOT NULL,
    [ProcessingUnitUnitId] int NOT NULL,
    CONSTRAINT [PK_SalaryProcesses] PRIMARY KEY ([SalaryProcesseId]),
    CONSTRAINT [FK_SalaryProcesses_FishTypes_FishTypeId] FOREIGN KEY ([FishTypeId]) REFERENCES [FishTypes] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_SalaryProcesses_ProcessingUnits_ProcessingUnitUnitId] FOREIGN KEY ([ProcessingUnitUnitId]) REFERENCES [ProcessingUnits] ([UnitId]) ON DELETE CASCADE
);

CREATE INDEX [IX_SalaryProcesses_FishTypeId] ON [SalaryProcesses] ([FishTypeId]);

CREATE INDEX [IX_SalaryProcesses_ProcessingUnitUnitId] ON [SalaryProcesses] ([ProcessingUnitUnitId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251201064129_AddImgFishUrlToFishAndRenameSalaryPaymentToSalaryProcess', N'9.0.0');

EXEC sp_rename N'[SalaryProcesses].[SalaryProcesseId]', N'SalaryProcessId', 'COLUMN';

EXEC sp_rename N'[Receipts].[SalaryProcesseId]', N'SalaryProcessId', 'COLUMN';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251201064939_RenameSalaryProcesseToSalaryProcess', N'9.0.0');

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FishPrices]') AND [c].[name] = N'ImgFishUrl');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [FishPrices] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [FishPrices] DROP COLUMN [ImgFishUrl];

ALTER TABLE [FishTypes] ADD [ImgFishUrl] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251202073306_ChangePropertyImgUrlComeToFishType', N'9.0.0');

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FishPrices]') AND [c].[name] = N'EffectiveDate');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [FishPrices] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [FishPrices] DROP COLUMN [EffectiveDate];

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251202073626_DeletePropertyEffectiveDate', N'9.0.0');

ALTER TABLE [SalaryProcesses] DROP CONSTRAINT [FK_SalaryProcesses_ProcessingUnits_ProcessingUnitUnitId];

DROP INDEX [IX_SalaryProcesses_ProcessingUnitUnitId] ON [SalaryProcesses];

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalaryProcesses]') AND [c].[name] = N'FishId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [SalaryProcesses] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [SalaryProcesses] DROP COLUMN [FishId];

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalaryProcesses]') AND [c].[name] = N'ProcessingUnitUnitId');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [SalaryProcesses] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [SalaryProcesses] DROP COLUMN [ProcessingUnitUnitId];

EXEC sp_rename N'[SalaryProcesses].[UnitId]', N'ProcessingUnitId', 'COLUMN';

CREATE INDEX [IX_SalaryProcesses_ProcessingUnitId] ON [SalaryProcesses] ([ProcessingUnitId]);

ALTER TABLE [SalaryProcesses] ADD CONSTRAINT [FK_SalaryProcesses_ProcessingUnits_ProcessingUnitId] FOREIGN KEY ([ProcessingUnitId]) REFERENCES [ProcessingUnits] ([UnitId]) ON DELETE CASCADE;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251203081447_UpdatePropertySalaryProcessing', N'9.0.0');

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalaryProcesses]') AND [c].[name] = N'Date');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [SalaryProcesses] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [SalaryProcesses] ALTER COLUMN [Date] date NOT NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251203090406_ChangeDateTimeToDateOnly', N'9.0.0');

DROP TABLE [Receipts];

ALTER TABLE [SalaryProcesses] ADD [SalaryPayment] decimal(18,2) NOT NULL DEFAULT 0.0;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251207100924_RemoveReciptAndAddPropertySalaryPaymentToSalaryProcess', N'9.0.0');

ALTER TABLE [SalaryProcesses] ADD [PricePerKg] decimal(18,2) NOT NULL DEFAULT 0.0;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251213110752_AddPropertyPricePerKgToSalaryProcess', N'9.0.0');

ALTER TABLE [SalaryProcesses] ADD [Status] nvarchar(max) NOT NULL DEFAULT N'';

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251213163103_AddPropertyStatusToSalaryProcess', N'9.0.0');

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[SalaryProcesses]') AND [c].[name] = N'Status');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [SalaryProcesses] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [SalaryProcesses] ALTER COLUMN [Status] int NOT NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251213164918_ChangeSalaryStatusToEnum', N'9.0.0');

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[FishTypes]') AND [c].[name] = N'UnitOfMeasure');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [FishTypes] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [FishTypes] DROP COLUMN [UnitOfMeasure];

EXEC sp_rename N'[SalaryProcesses].[PricePerKg]', N'PricePerUnit', 'COLUMN';

EXEC sp_rename N'[FishPrices].[PricePerUnitOfMeasure]', N'PricePerUnit', 'COLUMN';

ALTER TABLE [FishPrices] ADD [EffectiveDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';

ALTER TABLE [FishPrices] ADD [UnitOfMeasure] nvarchar(max) NULL;

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251215155014_UpdateTableFishPrice', N'9.0.0');

DROP INDEX [IX_FishPrices_FishTypeId] ON [FishPrices];

CREATE INDEX [IX_FishPrices_FishTypeId] ON [FishPrices] ([FishTypeId]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20251216143524_Update1toManyRelationshipInFishType', N'9.0.0');

COMMIT;
GO

