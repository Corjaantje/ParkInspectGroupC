CREATE TABLE [dbo].[WorkingHours]
(
	[EmployeeId] INT NOT NULL, 
    [Date] DATE NULL, 
    [StartTime] TIME NULL, 
    [EndTime] TIME NULL, 
    CONSTRAINT [FK_WorkingHours_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) 
)
