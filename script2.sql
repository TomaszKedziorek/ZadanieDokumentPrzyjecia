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

CREATE TABLE [Addresses] (
    [Id] int NOT NULL IDENTITY,
    [Country] nvarchar(max) NOT NULL,
    [Street] nvarchar(max) NOT NULL,
    [City] nvarchar(max) NOT NULL,
    [State] nvarchar(max) NOT NULL,
    [ZipCode] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Labels] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Labels] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Warehouses] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Symbol] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Warehouses] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Suppliers] (
    [Id] int NOT NULL IDENTITY,
    [CompanyName] nvarchar(max) NOT NULL,
    [AddressId] int NOT NULL,
    CONSTRAINT [PK_Suppliers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Suppliers_Addresses_AddressId] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AdmissionDocuments] (
    [Id] int NOT NULL IDENTITY,
    [TargetWarehouseId] int NOT NULL,
    [SupplierId] int NOT NULL,
    [Canceled] bit NOT NULL,
    [Approved] bit NOT NULL,
    CONSTRAINT [PK_AdmissionDocuments] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_AdmissionDocuments_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AdmissionDocuments_Warehouses_TargetWarehouseId] FOREIGN KEY ([TargetWarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [AdmissionDocumentLabel] (
    [AdmissionDocumentsId] int NOT NULL,
    [LablesId] int NOT NULL,
    CONSTRAINT [PK_AdmissionDocumentLabel] PRIMARY KEY ([AdmissionDocumentsId], [LablesId]),
    CONSTRAINT [FK_AdmissionDocumentLabel_AdmissionDocuments_AdmissionDocumentsId] FOREIGN KEY ([AdmissionDocumentsId]) REFERENCES [AdmissionDocuments] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_AdmissionDocumentLabel_Labels_LablesId] FOREIGN KEY ([LablesId]) REFERENCES [Labels] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Commodities] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(max) NOT NULL,
    [Code] nvarchar(max) NOT NULL,
    [Price] float NOT NULL,
    [Quantity] int NOT NULL,
    [AdmissionDocumentId] int NOT NULL,
    CONSTRAINT [PK_Commodities] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Commodities_AdmissionDocuments_AdmissionDocumentId] FOREIGN KEY ([AdmissionDocumentId]) REFERENCES [AdmissionDocuments] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_AdmissionDocumentLabel_LablesId] ON [AdmissionDocumentLabel] ([LablesId]);
GO

CREATE INDEX [IX_AdmissionDocuments_SupplierId] ON [AdmissionDocuments] ([SupplierId]);
GO

CREATE INDEX [IX_AdmissionDocuments_TargetWarehouseId] ON [AdmissionDocuments] ([TargetWarehouseId]);
GO

CREATE INDEX [IX_Commodities_AdmissionDocumentId] ON [Commodities] ([AdmissionDocumentId]);
GO

CREATE INDEX [IX_Suppliers_AddressId] ON [Suppliers] ([AddressId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240311152814_Init', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AdmissionDocuments] DROP CONSTRAINT [FK_AdmissionDocuments_Suppliers_SupplierId];
GO

ALTER TABLE [AdmissionDocuments] DROP CONSTRAINT [FK_AdmissionDocuments_Warehouses_TargetWarehouseId];
GO

ALTER TABLE [Suppliers] DROP CONSTRAINT [FK_Suppliers_Addresses_AddressId];
GO

DROP INDEX [IX_Suppliers_AddressId] ON [Suppliers];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AdmissionDocuments]') AND [c].[name] = N'TargetWarehouseId');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [AdmissionDocuments] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [AdmissionDocuments] ALTER COLUMN [TargetWarehouseId] int NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[AdmissionDocuments]') AND [c].[name] = N'SupplierId');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [AdmissionDocuments] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [AdmissionDocuments] ALTER COLUMN [SupplierId] int NULL;
GO

ALTER TABLE [Addresses] ADD [SupplierId] int NOT NULL DEFAULT 0;
GO

CREATE UNIQUE INDEX [IX_Addresses_SupplierId] ON [Addresses] ([SupplierId]);
GO

ALTER TABLE [Addresses] ADD CONSTRAINT [FK_Addresses_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [AdmissionDocuments] ADD CONSTRAINT [FK_AdmissionDocuments_Suppliers_SupplierId] FOREIGN KEY ([SupplierId]) REFERENCES [Suppliers] ([Id]) ON DELETE SET NULL;
GO

ALTER TABLE [AdmissionDocuments] ADD CONSTRAINT [FK_AdmissionDocuments_Warehouses_TargetWarehouseId] FOREIGN KEY ([TargetWarehouseId]) REFERENCES [Warehouses] ([Id]) ON DELETE SET NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240312155744_fixRelations', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Commodities]') AND [c].[name] = N'AdmissionDocumentId');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Commodities] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Commodities] ALTER COLUMN [AdmissionDocumentId] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240313103515_ComodityNullAdmissionId', N'7.0.16');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [AdmissionDocumentLabel] DROP CONSTRAINT [FK_AdmissionDocumentLabel_Labels_LablesId];
GO

EXEC sp_rename N'[AdmissionDocumentLabel].[LablesId]', N'LabelsId', N'COLUMN';
GO

EXEC sp_rename N'[AdmissionDocumentLabel].[IX_AdmissionDocumentLabel_LablesId]', N'IX_AdmissionDocumentLabel_LabelsId', N'INDEX';
GO

ALTER TABLE [AdmissionDocumentLabel] ADD CONSTRAINT [FK_AdmissionDocumentLabel_Labels_LabelsId] FOREIGN KEY ([LabelsId]) REFERENCES [Labels] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240316141004_fixLabelTypo', N'7.0.16');
GO

COMMIT;
GO

