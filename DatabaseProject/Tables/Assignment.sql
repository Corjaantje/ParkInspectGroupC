CREATE TABLE [dbo].[Assignment]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
	[CustomerId] INT NOT NULL, 
	[ManagerId] INT NOT NULL, 
    [Description] VARCHAR(MAX) NULL, 
    [StartDate] DATE NULL, 
    [EndDate] DATE NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_Assignment_Manager] FOREIGN KEY ([ManagerId]) REFERENCES [Employee]([Id]), 
    CONSTRAINT [FK_Assignment_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Customer]([Id]),
)
