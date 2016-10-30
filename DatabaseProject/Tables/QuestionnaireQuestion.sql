CREATE TABLE [dbo].[QuestionnaireQuestion]
(
	[QuestionnaireId] INT NOT NULL , 
    [QuestionId] INT NOT NULL, 
    [Result] VARCHAR(MAX) NULL, 
    PRIMARY KEY ([QuestionId], [QuestionnaireId]), 
    CONSTRAINT [FK_QuestionnaireQuestion_Questionnaire] FOREIGN KEY ([QuestionnaireId]) REFERENCES [Questionnaire]([Id]), 
    CONSTRAINT [FK_QuestionnaireQuestion_Question] FOREIGN KEY ([QuestionId]) REFERENCES [Question]([Id])
)
