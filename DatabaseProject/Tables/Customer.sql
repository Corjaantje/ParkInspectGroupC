CREATE TABLE [dbo].[Customer]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Name] VARCHAR(15) NOT NULL, 
    [Address] VARCHAR(50) NULL, 
	[Location] VARCHAR(50) NULL,
    [Phonenumber] VARCHAR(15) NULL, 
    [Email] VARCHAR(50) NULL 
)
