CREATE TABLE [dbo].[Questionnaire]
(
	[Id] INT NOT NULL PRIMARY KEY,	
	[InspectionId] INT NOT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_Questionnaire_Inspection] FOREIGN KEY ([InspectionId]) REFERENCES [Inspection]([Id])
)
