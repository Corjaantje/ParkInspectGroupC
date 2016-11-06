CREATE TABLE [dbo].[Availability]
(
	[EmployeeId] INT NOT NULL , 
    [Date] DATE NULL, 
    [EndTime] TIME(0) NULL, 
    [StartTime] TIME(0) NULL, 
    CONSTRAINT [FK_Availability_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id])
)
