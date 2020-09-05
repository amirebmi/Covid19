IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

CREATE TABLE [Cities] (
    [Id] int NOT NULL IDENTITY,
    [CityName] nvarchar(max) NULL,
    [Population] bigint NOT NULL,
    CONSTRAINT [PK_Cities] PRIMARY KEY ([Id])
);

GO

CREATE TABLE [Covid19s] (
    [Id] int NOT NULL IDENTITY,
    [Date] datetime2 NOT NULL,
    [Cases] bigint NOT NULL,
    [Deaths] bigint NOT NULL,
    [Tested] bigint NOT NULL,
    [CityId] int NULL,
    CONSTRAINT [PK_Covid19s] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Covid19s_Cities_CityId] FOREIGN KEY ([CityId]) REFERENCES [Cities] ([Id]) ON DELETE NO ACTION
);

GO

CREATE INDEX [IX_Covid19s_CityId] ON [Covid19s] ([CityId]);

GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20200721043948_InitialSchema', N'3.1.6');

GO

