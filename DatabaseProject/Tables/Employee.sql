CREATE TABLE [dbo].[Employee]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [FirstName] VARCHAR(30) NOT NULL, 
	[Prefix] VARCHAR(10) NULL,
    [SurName] VARCHAR(30) NOT NULL, 
	[Gender] CHAR NULL, 
    [City] VARCHAR(20) NULL, 
    [Address] VARCHAR(50) NULL, 
    [ZipCode] VARCHAR(10) NULL, 
    [Phonenumber] VARCHAR(15) NULL, 
    [Email] VARCHAR(50) NULL,
	[RegionId] INT NULL,	 
    [EmployeeStatusId] INT NULL, 
    [Inspecter] BIT NOT NULL, 
    [Manager] BIT NOT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_Employee_Region] FOREIGN KEY ([RegionId]) REFERENCES [Region]([Id]), 
    CONSTRAINT [FK_Employee_EmployeeStatus] FOREIGN KEY ([EmployeeStatusId]) REFERENCES [EmployeeStatus]([Id]) 
)
