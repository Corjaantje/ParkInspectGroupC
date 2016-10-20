CREATE TABLE [dbo].[Availability]
(
	[EmployeeId] INT NOT NULL , 
    [Date] DATE NULL, 
    [EndTime] TIME NULL, 
    [StartTime] TIME NULL, 
    CONSTRAINT [FK_Availability_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id])
)
