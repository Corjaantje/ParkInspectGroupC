CREATE TABLE [dbo].[InspectionImage]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [File] VARCHAR(MAX) NOT NULL, 
    [InspectionId] INT NULL, 
    CONSTRAINT [FK_InspectionImages_Inspection] FOREIGN KEY ([InspectionId]) REFERENCES [Inspection]([Id])
)
