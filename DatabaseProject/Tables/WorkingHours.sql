﻿CREATE TABLE [dbo].[WorkingHours]
(
	[EmployeeId] INT NOT NULL, 
    [Date] DATE NOT NULL, 
    [StartTime] TIME(0) NULL, 
    [EndTime] TIME(0) NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_WorkingHours_Employee] FOREIGN KEY ([EmployeeId]) REFERENCES [Employee]([Id]), 
    CONSTRAINT [PK_WorkingHours] PRIMARY KEY ([EmployeeId], [Date]) 
)
