CREATE TABLE [dbo].[QuestionAnswer]
(
	[QuestionnaireId] INT NOT NULL , 
    [QuestionId] INT NOT NULL, 
    [Result] NVARCHAR(MAX) NULL, 
    PRIMARY KEY ([QuestionId], [QuestionnaireId]), 
    CONSTRAINT [FK_QuestionAnswer_Questionnaire] FOREIGN KEY ([QuestionnaireId]) REFERENCES [Questionnaire]([Id]), 
    CONSTRAINT [FK_QuestionAnswer_Question] FOREIGN KEY ([QuestionId]) REFERENCES [Question]([Id])
)
