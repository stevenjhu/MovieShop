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

CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(24) NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Movies] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(512) NOT NULL,
    [Overview] nvarchar(max) NOT NULL,
    [Tagline] nvarchar(512) NOT NULL,
    [Budget] decimal(18,4) NULL DEFAULT 9.9,
    [Revenue] decimal(18,4) NULL DEFAULT 9.9,
    [ImdbUrl] nvarchar(2084) NOT NULL,
    [TmdbUrl] nvarchar(2084) NOT NULL,
    [PosterUrl] nvarchar(2084) NOT NULL,
    [BackdropUrl] nvarchar(2084) NOT NULL,
    [OriginalLanguage] nvarchar(64) NOT NULL,
    [ReleaseDate] datetime2 NULL,
    [RunTime] int NULL,
    [Price] decimal(5,2) NULL DEFAULT 9.9,
    [CreatedDate] datetime2 NULL DEFAULT (getdate()),
    [UpdatedDate] datetime2 NULL,
    [UpdatedBy] nvarchar(512) NULL,
    [CreatedBy] nvarchar(512) NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MovieGenres] (
    [MovieId] int NOT NULL,
    [GenreId] int NOT NULL,
    CONSTRAINT [PK_MovieGenres] PRIMARY KEY ([MovieId], [GenreId]),
    CONSTRAINT [FK_MovieGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieGenres_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE TABLE [Trailers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(2084) NOT NULL,
    [TrailerUrl] nvarchar(2084) NOT NULL,
    [MovieId] int NOT NULL,
    CONSTRAINT [PK_Trailers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Trailers_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieGenres_GenreId] ON [MovieGenres] ([GenreId]);
GO

CREATE INDEX [IX_Movies_Budget] ON [Movies] ([Budget]);
GO

CREATE INDEX [IX_Movies_Price] ON [Movies] ([Price]);
GO

CREATE INDEX [IX_Movies_Revenue] ON [Movies] ([Revenue]);
GO

CREATE INDEX [IX_Movies_Title] ON [Movies] ([Title]);
GO

CREATE INDEX [IX_Trailers_MovieId] ON [Trailers] ([MovieId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824212940_CreateMultipleTables', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'UpdatedBy');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Movies] ALTER COLUMN [UpdatedBy] nvarchar(max) NULL;
GO

DROP INDEX [IX_Movies_Title] ON [Movies];
DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Title');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Title] nvarchar(256) NOT NULL;
CREATE INDEX [IX_Movies_Title] ON [Movies] ([Title]);
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'CreatedBy');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Movies] ALTER COLUMN [CreatedBy] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825165833_ReConfigureMoviesTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Casts] (
    [Id] int NOT NULL IDENTITY,
    [Gender] nvarchar(max) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [ProfilePath] nvarchar(2084) NOT NULL,
    [TmdbUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Casts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MovieCasts] (
    [CastId] int NOT NULL,
    [MovieId] int NOT NULL,
    [Character] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_MovieCasts] PRIMARY KEY ([CastId], [MovieId], [Character]),
    CONSTRAINT [FK_MovieCasts_Casts_CastId] FOREIGN KEY ([CastId]) REFERENCES [Casts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieCasts_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieCasts_MovieId] ON [MovieCasts] ([MovieId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825172840_AddCastAndMovieCastTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Favorites] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Favorites] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [FK_Favorites_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825175148_AddFavoritesTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [FirstName] nvarchar(128) NOT NULL,
    [LastName] nvarchar(128) NOT NULL,
    [Email] nvarchar(256) NOT NULL,
    [HashedPassword] nvarchar(1024) NOT NULL,
    [DateOfBirth] datetime2 NULL,
    [PhoneNumber] nvarchar(16) NULL,
    [ProfilePictureUrl] nvarchar(max) NULL,
    [Salt] nvarchar(1024) NOT NULL,
    [IsLocked] bit NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Favorites_UserId] ON [Favorites] ([UserId]);
GO

ALTER TABLE [Favorites] ADD CONSTRAINT [FK_Favorites_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825181954_AddUsersTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Reviews] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    [CreateDate] datetime2 NOT NULL DEFAULT (getdate()),
    [Rating] decimal(3,2) NOT NULL DEFAULT 9.9,
    [ReviewText] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [FK_Reviews_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Reviews_UserId] ON [Reviews] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825184707_AddReviewsTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Purchases] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    [PurchaseDateTime] datetime2 NOT NULL DEFAULT (getdate()),
    [PurchaseNumber] nvarchar(450) NOT NULL,
    [TotalPrice] decimal(5,2) NOT NULL DEFAULT 9.9,
    CONSTRAINT [PK_Purchases] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [AK_Purchases_PurchaseNumber] UNIQUE ([PurchaseNumber]),
    CONSTRAINT [FK_Purchases_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Purchases_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Purchases_UserId] ON [Purchases] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825191511_AddPurchasesTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825192732_ReviseTableNav', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserRoles] (
    [RoleId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([RoleId], [UserId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_UserRoles_UserId] ON [UserRoles] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825193815_AddUserRolesAndRolesTable', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Purchases] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    [PurchaseDateTime] datetime2 NOT NULL DEFAULT (getdate()),
    [PurchaseNumber] nvarchar(450) NOT NULL,
    [TotalPrice] decimal(5,2) NOT NULL DEFAULT 9.9,
    CONSTRAINT [PK_Purchases] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [AK_Purchases_PurchaseNumber] UNIQUE ([PurchaseNumber]),
    CONSTRAINT [FK_Purchases_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Purchases_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Purchases_UserId] ON [Purchases] ([UserId]);
GO

ALTER TABLE [Purchases] DROP CONSTRAINT [AK_Purchases_PurchaseNumber];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Purchases]') AND [c].[name] = N'PurchaseNumber');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Purchases] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Purchases] ALTER COLUMN [PurchaseNumber] uniqueidentifier NOT NULL;
ALTER TABLE [Purchases] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [PurchaseNumber];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220829164908_PurchaseTableUpdate', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MovieCardModel] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [PosterUrl] nvarchar(max) NOT NULL,
    [CastId] int NULL,
    CONSTRAINT [PK_MovieCardModel] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_MovieCardModel_Casts_CastId] FOREIGN KEY ([CastId]) REFERENCES [Casts] ([Id])
);
GO

CREATE INDEX [IX_MovieCardModel_CastId] ON [MovieCardModel] ([CastId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220830183040_UpdatedCastEntity', N'6.0.8');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP TABLE [MovieCardModel];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220830205206_RestoreCastTableRelationship', N'6.0.8');
GO

COMMIT;
GO

