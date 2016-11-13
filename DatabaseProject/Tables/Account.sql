CREATE TABLE [dbo].[Account]
(
    [Id] INT IDENTITY(1,1) NOT NULL,  
    [Username] VARCHAR(25) NOT NULL, 
    [Password] VARCHAR(50) NOT NULL DEFAULT 'parkinspect', 
    [UserGuid] VARCHAR(50) NOT NULL, 
    [EmployeeId] INT NOT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [PK_Account] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Account_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) 
)

GO
