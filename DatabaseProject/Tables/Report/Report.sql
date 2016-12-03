CREATE TABLE [dbo].[Report]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(MAX) NOT NULL, 
    [Summary] VARCHAR(MAX) NULL, 
    [Date] DATETIME NULL, 
    [EmployeeId] INT NOT NULL, 
    [AssignmentId] INT NOT NULL, 
    CONSTRAINT [FK_Report_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]), 
    CONSTRAINT [FK_Report_Assignment] FOREIGN KEY ([AssignmentId]) REFERENCES [Assignment]([Id]) 
)
