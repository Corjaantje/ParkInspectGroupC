﻿CREATE TABLE [dbo].[Region]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Region] VARCHAR(50) NOT NULL,
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE()
)
