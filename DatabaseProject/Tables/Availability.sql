CREATE TABLE [dbo].[Availability]
(
	[EmployeeId] INT NOT NULL , 
    [Date] DATE NOT NULL, 
	[StartTime] TIME(0) NULL, 
    [EndTime] TIME(0) NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
	PRIMARY KEY ([EmployeeId], [Date]), 
    CONSTRAINT [FK_Availability_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id])
)
