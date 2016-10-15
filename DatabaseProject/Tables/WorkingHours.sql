CREATE TABLE [dbo].[WorkingHours]
(
	[EmployeeId] INT NOT NULL, 
    [Date] DATE NULL, 
    [StartTime] TIMESTAMP NULL, 
    [EndTime] TIMESTAMP NULL, 
    CONSTRAINT [FK_WorkingHours_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) 
)
