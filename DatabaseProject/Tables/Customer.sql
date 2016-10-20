CREATE TABLE [dbo].[Customer]
(
	[Id] INT NOT NULL PRIMARY KEY, 
	[AccountId] INT NOT NULL, 
    [Name] VARCHAR(15) NOT NULL, 
    [Address] VARCHAR(50) NULL, 
	[Location] VARCHAR(50) NULL,
    [Phonenumber] VARCHAR(15) NULL, 
    [Email] VARCHAR(50) NULL, 
    CONSTRAINT [FK_Customer_Account] FOREIGN KEY ([AccountId]) REFERENCES [Account]([Id]), 
)
