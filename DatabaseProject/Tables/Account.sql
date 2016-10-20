CREATE TABLE [dbo].[Account]
(
    [Id] INT NOT NULL,  
    [Username] VARCHAR(25) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    CONSTRAINT [PK_Account] PRIMARY KEY ([Id]) 
)
