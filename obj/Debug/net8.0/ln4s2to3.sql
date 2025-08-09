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
CREATE TABLE [account] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [username] nvarchar(50) NOT NULL,
    [email] nvarchar(100) NOT NULL,
    [password_hash] nvarchar(255) NOT NULL,
    [full_name] nvarchar(100) NULL,
    [last_login] datetime2 NULL,
    CONSTRAINT [PK_account] PRIMARY KEY ([Id])
);

CREATE TABLE [auth_tokens] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [account_id] uniqueidentifier NOT NULL,
    [token] nvarchar(255) NOT NULL,
    [token_type] nvarchar(20) NOT NULL,
    [expires_at] datetime2 NOT NULL,
    [created_at] datetime2 NOT NULL DEFAULT (GETDATE()),
    [last_used] datetime2 NULL,
    [device_info] nvarchar(255) NULL,
    [ip_address] nvarchar(45) NULL,
    [is_active] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_auth_tokens] PRIMARY KEY ([Id]),
    CONSTRAINT [CK_auth_tokens_token_type] CHECK (token_type IN ('login', 'refresh', 'api')),
    CONSTRAINT [FK_auth_tokens_account] FOREIGN KEY ([account_id]) REFERENCES [account] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [code_vetify] (
    [Id] uniqueidentifier NOT NULL DEFAULT (NEWID()),
    [account_id] uniqueidentifier NOT NULL,
    [code] nvarchar(255) NOT NULL,
    [expires_at] datetime2 NOT NULL DEFAULT (GETDATE()),
    [created_at] datetime2 NOT NULL DEFAULT (GETDATE()),
    [device_info] nvarchar(255) NULL,
    [ip_address] nvarchar(45) NULL,
    [is_active] bit NOT NULL DEFAULT CAST(1 AS bit),
    CONSTRAINT [PK_code_vetify] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_code_vetify_account] FOREIGN KEY ([account_id]) REFERENCES [account] ([Id]) ON DELETE CASCADE
);

CREATE UNIQUE INDEX [IX_account_email] ON [account] ([email]);

CREATE UNIQUE INDEX [IX_account_username] ON [account] ([username]);

CREATE INDEX [IX_auth_tokens_account_id] ON [auth_tokens] ([account_id]);

CREATE UNIQUE INDEX [IX_auth_tokens_token] ON [auth_tokens] ([token]);

CREATE INDEX [IX_code_vetify_account_id] ON [code_vetify] ([account_id]);

CREATE UNIQUE INDEX [IX_code_vetify_code] ON [code_vetify] ([code]);

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20250809081843_TenMigration', N'9.0.7');

COMMIT;
GO

