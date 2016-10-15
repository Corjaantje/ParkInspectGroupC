CREATE TABLE [dbo].[Account]
(
	[EmployeeId] INT NOT NULL, 
    [Username] VARCHAR(25) NOT NULL, 
    [Password] VARCHAR(50) NULL, 
    CONSTRAINT [FK_Account_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) 
)
