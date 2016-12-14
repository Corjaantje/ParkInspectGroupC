CREATE TABLE [dbo].[InspectionImage]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [File] VARCHAR(MAX) NOT NULL, 
    [InspectionId] INT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_InspectionImages_Inspection] FOREIGN KEY ([InspectionId]) REFERENCES [Inspection]([Id])
)
