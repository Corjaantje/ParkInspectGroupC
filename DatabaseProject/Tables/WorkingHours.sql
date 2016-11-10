CREATE TABLE [dbo].[WorkingHours]
(
	[EmployeeId] INT NOT NULL, 
    [Date] DATE NULL, 
    [StartTime] TIME NULL, 
    [EndTime] TIME NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_WorkingHours_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) 
)
