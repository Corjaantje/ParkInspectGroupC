CREATE TABLE [dbo].[Report]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Title] VARCHAR(MAX) NOT NULL, 
    [Summary] VARCHAR(MAX) NULL, 
    [Date] DATETIME NULL, 
    [EmployeeId] INT NOT NULL, 
    CONSTRAINT [FK_Report_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]) 
)
