CREATE TABLE [dbo].[QuestionnaireModule]
(
	[QuestionnaireId] INT NOT NULL, 
    [ModuleId] INT NOT NULL, 
    CONSTRAINT [PK_QuestionnaireModule] PRIMARY KEY ([ModuleId], [QuestionnaireId]), 
    CONSTRAINT [FK_QuestionnaireModule_Questionnaire] FOREIGN KEY ([QuestionnaireId]) REFERENCES [Questionnaire]([Id]), 
    CONSTRAINT [FK_QuestionnaireModule_ToTable_1] FOREIGN KEY ([ModuleId]) REFERENCES [ToTable]([ToTableColumn]) 
)
