CREATE TABLE [dbo].[QuestionnaireModule]
(
	[ModuleId] INT NOT NULL , 
    [QuestionnaireId] INT NOT NULL, 
    [DateCreated] DATETIME NOT NULL DEFAULT GETDATE(), 
    [DateUpdated] DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT [FK_QuestionnaireModule_Module] FOREIGN KEY ([ModuleId]) REFERENCES [Module]([Id]), 
    CONSTRAINT [FK_QuestionnaireModule_Questionnaire] FOREIGN KEY ([QuestionnaireId]) REFERENCES [Questionnaire]([Id]), 
    PRIMARY KEY ([ModuleId], [QuestionnaireId])
)
