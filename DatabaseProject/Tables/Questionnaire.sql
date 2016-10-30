CREATE TABLE [dbo].[Questionnaire]
(
	[Id] INT NOT NULL PRIMARY KEY,	
	[InspectionId] INT NOT NULL,
    CONSTRAINT [FK_Questionnaire_Inspection] FOREIGN KEY ([InspectionId]) REFERENCES [Inspection]([Id]), 
)
