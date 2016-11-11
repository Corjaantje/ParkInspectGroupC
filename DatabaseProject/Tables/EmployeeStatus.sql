﻿CREATE TABLE [dbo].[EmployeeStatus]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Description] VARCHAR(25) NOT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE()
)
