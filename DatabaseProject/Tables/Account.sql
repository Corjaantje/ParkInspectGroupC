CREATE TABLE [dbo].[Account]
(
    [Id] INT IDENTITY(1,1) NOT NULL,  
    [Username] VARCHAR(25) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL, 
    [UserGuid] VARCHAR(50) NOT NULL, 
    [EmployeeId] INT NOT NULL, 
    CONSTRAINT [PK_Account] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Account_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) 
)

GO
