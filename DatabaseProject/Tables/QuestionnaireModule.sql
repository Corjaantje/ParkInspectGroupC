CREATE TABLE [dbo].[QuestionnaireModule]
(
	[ModuleId] INT NOT NULL , 
    [QuestionnaireId] INT NOT NULL, 
    CONSTRAINT [FK_QuestionnaireModule_Module] FOREIGN KEY ([ModuleId]) REFERENCES [Module]([Id]), 
    CONSTRAINT [FK_QuestionnaireModule_Questionnaire] FOREIGN KEY ([QuestionnaireId]) REFERENCES [Questionnaire]([Id]), 
    PRIMARY KEY ([ModuleId], [QuestionnaireId])
)
