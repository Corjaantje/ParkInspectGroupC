CREATE TABLE [dbo].[Coordinate]
(
	[Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY, 
    [Longitude] FLOAT NOT NULL, 
    [Latitude] FLOAT NOT NULL, 
    [Note] VARCHAR(MAX) NULL, 
    [InspectionId] INT NOT NULL, 
	[DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_Coordinate_Inspection] FOREIGN KEY ([InspectionId]) REFERENCES [Inspection]([Id])
)
