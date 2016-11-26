CREATE TABLE [dbo].[Questionnaire]
(
	[Id] INT IDENTITY(1,1) NOT NULL, 
	[InspectionId] INT NOT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
	CONSTRAINT [PK_Questionnaire] PRIMARY KEY ([Id]), 
    CONSTRAINT [FK_Questionnaire_Inspection] FOREIGN KEY ([InspectionId]) REFERENCES [Inspection]([Id])
)
