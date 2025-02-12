CREATE TABLE [dbo].[account] (
    [Id] UNIQUEIDENTIFIER NOT NULL,
    [Name] VARCHAR(100) NOT NULL,
    [Balance] DECIMAL(18,2) NOT NULL,
    [AccountType] INT NOT NULL,
    [UserID] UNIQUEIDENTIFIER NOT NULL,
    
    CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Account_User] FOREIGN KEY ([UserID]) REFERENCES [dbo].[user]([Id]) ON DELETE CASCADE
);
GO